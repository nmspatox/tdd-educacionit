using System;
using System.Collections.Generic;
using System.Linq;
using Dolphin.Domain.Model.Entities.Calendars;
using Dolphin.Domain.Model.Exceptions;

namespace Dolphin.Domain.Model.Entities.Projects
{
    public class ProjectTask : Activity
    {
        private ProjectTask parent;
        protected IList<ProjectTask> tasks;

        public ProjectTask(ProjectTask parent, IProjectCalendar calendar, string name)
            : base(calendar, name)
        {
            this.parent = parent;
            InitializeTasks();
        }

        protected override void OnSetExpectedStartDate()
        {
            base.OnSetExpectedStartDate();
            if (expectedStartDate != null)
                UpdateParentExpectedStartDate();
        }

        private void UpdateParentExpectedStartDate()
        {
            if (parent != null)
            {
                parent.ExpectedStartDate = GetMinimumExpectedStartDateInSiblingActivities();
            }
        }

        private DateTime? GetMinimumExpectedStartDateInSiblingActivities()
        {
            return GetMinimumDateInSiblingActivities(task => task.ExpectedStartDate);
        }

        protected override void OnSetExpectedFinishDate()
        {
            base.OnSetExpectedFinishDate();
            if (expectedFinishDate != null)
                UpdateParentExpectedFinishDate();
        }

        private void UpdateParentExpectedFinishDate()
        {
            if (parent != null)
            {
                parent.ExpectedFinishDate = GetMaximumExpectedStartDateInSiblingActivities();
            }
        }

        private DateTime? GetMaximumExpectedStartDateInSiblingActivities()
        {
            return GetMaximumDateInSiblingActivities(task => task.ExpectedFinishDate);
        }

        protected override void OnSetActualStartDate()
        {
            base.OnSetActualStartDate();
            if (actualStartDate != null)
                UpdateParentActualStartDate();
        }

        private void UpdateParentActualStartDate()
        {
            if (parent != null)
            {
                parent.ActualStartDate = GetMinimumActualStartDateInSiblingActivities();
            }
        }

        private DateTime? GetMinimumActualStartDateInSiblingActivities()
        {
            return GetMinimumDateInSiblingActivities(task => task.ActualStartDate);
        }

        protected override void OnSetActualFinishDate()
        {
            base.OnSetActualFinishDate();
            if (actualFinishDate != null)
                UpdateParentActualFinishDate();
        }

        private void UpdateParentActualFinishDate()
        {
            if (parent != null)
            {
                parent.ActualFinishDate = GetMaximumActualStartDateInSiblingActivities();
            }
        }

        private DateTime? GetMaximumActualStartDateInSiblingActivities()
        {
            return GetMaximumDateInSiblingActivities(task => task.ActualFinishDate);
        }

        protected void InitializeTasks()
        {
            tasks = new List<ProjectTask>();
        }

        public ProjectTask AddTask(string name)
        {
            ProjectTask task = new ProjectTask(this, calendar, name);
            tasks.Add(task);
            return task;
        }

        public IEnumerable<ProjectTask> Tasks
        {
            get { return tasks; }
        }

        public void RemoveTask(ProjectTask task)
        {
            ValidateTaskIsNotNull(task);
            ValidateTaskBelongsToProject(task);

            tasks.Remove(task);
        }

        public void RemoveAllTasks()
        {
            InitializeTasks();
        }

        private void ValidateTaskIsNotNull(ProjectTask task)
        {
            if (task == null)
                throw new ProjectTaskNullException();
        }

        private void ValidateTaskBelongsToProject(ProjectTask task)
        {
            if (!tasks.Contains(task))
                throw new ProjectTaskDoesNotExistException();
        }

        private DateTime? GetMinimumDateInSiblingActivities(Func<ProjectTask, DateTime?> dateToEvaluate)
        {
            return GetDateInSiblingActivities(dateToEvaluate, (dateWorkingWith, partialResult) => dateWorkingWith < partialResult);
        }

        private DateTime? GetMaximumDateInSiblingActivities(Func<ProjectTask, DateTime?> dateToEvaluate)
        {
            return GetDateInSiblingActivities(dateToEvaluate, (dateWorkingWith, partialResult) => dateWorkingWith > partialResult);
        }

        private DateTime? GetDateInSiblingActivities(Func<ProjectTask, DateTime?> dateToEvaluate, Func<DateTime, DateTime, bool> compareExpression)
        {
            DateTime? result = null;
            foreach (ProjectTask childTask in parent.Tasks)
            {
                if (dateToEvaluate(childTask) != null)
                {
                    if (result == null)
                        result = dateToEvaluate(childTask);
                    if (compareExpression(dateToEvaluate(childTask).Value, result.Value))
                        result = dateToEvaluate(childTask);
                }
            }
            return result;
        }

        public float CalculateExpectedEffort()
        {
            float effort = 0;

            if (tasks.Any())
            {
                foreach (ProjectTask task in tasks)
                {
                    effort += task.CalculateExpectedEffort();
                }
            }
            else if (expectedStartDate != null && expectedFinishDate != null)
            {
                DateTime date = expectedStartDate.Value;
                DateTime finishDate = expectedFinishDate.Value;

                effort = calendar.GetWorkingHours(date);

                while (date < finishDate)
                {
                    date = date.AddDays(1);
                    effort += calendar.GetWorkingHours(date);
                }
            }

            return effort;
        }
    }
}
