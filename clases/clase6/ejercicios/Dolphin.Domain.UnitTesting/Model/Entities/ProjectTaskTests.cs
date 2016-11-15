using System;
using System.Collections.Generic;
using System.Linq;
using Dolphin.Domain.Model.Entities.Calendars;
using Dolphin.Domain.Model.Entities.Projects;
using Dolphin.Domain.Model.Exceptions;
using Dolphin.Domain.Model.Factories;
using NUnit.Framework;
using Moq;

namespace Dolphin.Domain.Model.Entities
{
    [TestFixture]
    public class ProjectTaskTests
    {
        private const String VALID_PROJECT_NAME = "Project Name";
        private const String VALID_PROJECTTASK_NAME = "Project Task Name";

        private static ProjectFactory projectFactory;
        private static ProjectCalendarFactory projectCalendarFactory;

        [TestFixtureSetUp]
        public static void TestFixtureSetUp()
        {
            projectCalendarFactory = new ProjectCalendarFactory();
            projectFactory = new ProjectFactory(projectCalendarFactory);
        }

        private Project CreateProject()
        {
            return projectFactory.CreateProject(VALID_PROJECT_NAME);
        }

        private ProjectTask AddProjectTask(Project project)
        {
            return project.AddTask(VALID_PROJECTTASK_NAME);
        }

        private ProjectTask AddProjectTask(ProjectTask task)
        {
            return task.AddTask(VALID_PROJECTTASK_NAME);
        }

        private ProjectTask CreateTask()
        {
            return AddProjectTask(CreateProject());
        }

        [Test]
        public void AProjectTaskCannotHaveEmptyName()
        {
            Project project = CreateProject();

            Exception caughtException = Assert.Catch(() => project.AddTask(""));

            Assert.That(caughtException, Is.InstanceOf<InvalidActivityNameException>());
        }

        [Test]
        public void AProjectTaskCannotHaveNullName()
        {
            Project project = CreateProject();

            Exception caughtException = Assert.Catch(() => project.AddTask(null));

            Assert.That(caughtException, Is.InstanceOf<InvalidActivityNameException>());
        }

        [Test]
        public void AProjectTaskHasName()
        {
            Project project = CreateProject();
            ProjectTask task = project.AddTask(VALID_PROJECTTASK_NAME);

            String name = task.Name;

            Assert.That(name, Is.EqualTo(VALID_PROJECTTASK_NAME));
        }

        [Test]
        public void ANewTaskDoesNotHaveChildrenTasks()
        {
            ProjectTask task = CreateTask();

            IEnumerable<ProjectTask> children = task.Tasks;

            Assert.That(children.Any(), Is.False);
        }

        [Test]
        public void WhenAddAChildTaskToATaskThenItHasOneChildTask()
        {
            ProjectTask task = CreateTask();
            ProjectTask child = AddProjectTask(task);

            IEnumerable<ProjectTask> children = task.Tasks;
            ProjectTask firstChild = children.First();

            Assert.That(children.Count(), Is.EqualTo(1));
            Assert.That(firstChild, Is.SameAs(child));
        }

        [Test]
        public void WhenAddThreeChildTasksToATaskItHasThreeChildTasks()
        {
            ProjectTask task = CreateTask();

            ProjectTask child1 = AddProjectTask(task);
            ProjectTask child2 = AddProjectTask(task);
            ProjectTask child3 = AddProjectTask(task);

            IEnumerable<ProjectTask> children = task.Tasks;
            ProjectTask firstChild = children.ElementAt(0);
            ProjectTask secondChild = children.ElementAt(1);
            ProjectTask thirdChild = children.ElementAt(2);

            Assert.That(children.Count(), Is.EqualTo(3));
            Assert.That(firstChild, Is.SameAs(child1));
            Assert.That(secondChild, Is.SameAs(child2));
            Assert.That(thirdChild, Is.SameAs(child3));
        }

        [Test]
        public void WhenAddAChildTaskToATaskItIsPossibleRemoveIt()
        {
            ProjectTask task = CreateTask();

            ProjectTask child = AddProjectTask(task);
            task.RemoveTask(child);

            IEnumerable<ProjectTask> children = task.Tasks;

            Assert.That(children.Any(), Is.False);
        }

        [Test]
        public void ItIsNotPossibleRemoveAChildTaskWichIsNotAddedBefore()
        {
            ProjectTask task = CreateTask();
            ProjectTask foreignTask = CreateTask();

            Exception caughtException = Assert.Catch(() => task.RemoveTask(foreignTask));

            Assert.That(caughtException, Is.InstanceOf<ProjectTaskDoesNotExistException>());
        }

        [Test]
        public void ItIsNotPossibleRemoveAChildTaskWithNullReference()
        {
            ProjectTask task = CreateTask();

            Exception caughtException = Assert.Catch(() => task.RemoveTask(null));

            Assert.That(caughtException, Is.InstanceOf<ProjectTaskNullException>());
        }

        [Test]
        public void WhenAddThreeChildTasksToATaskItIsPossibleRemoveThem()
        {
            ProjectTask task = CreateTask();

            ProjectTask child1 = AddProjectTask(task);
            ProjectTask child2 = AddProjectTask(task);
            ProjectTask child3 = AddProjectTask(task);

            task.RemoveTask(child1);
            task.RemoveTask(child2);
            task.RemoveTask(child3);

            IEnumerable<ProjectTask> children = task.Tasks;

            Assert.That(children.Any(), Is.False);
        }

        [Test]
        public void ItIsPossibleRemoveAllChildTasksInATask()
        {
            ProjectTask task = CreateTask();
            AddProjectTask(task);
            AddProjectTask(task);
            AddProjectTask(task);

            task.RemoveAllTasks();

            IEnumerable<ProjectTask> tasks = task.Tasks;

            Assert.That(tasks.Any(), Is.False);
        }

        [Test]
        public void ChildTasksAddedToATaskAreTheSameOnes()
        {
            Project task = CreateProject();

            List<ProjectTask> children = new List<ProjectTask>();
            ProjectTask child1 = AddProjectTask(task);
            children.Add(child1);
            ProjectTask child2 = AddProjectTask(task);
            children.Add(child2);
            ProjectTask child3 = AddProjectTask(task);
            children.Add(child3);

            IEnumerable<ProjectTask> childrenTasks = task.Tasks;

            CollectionAssert.AreEquivalent(childrenTasks, children);
        }

        [Test]
        public void ATaskCanHaveNullExpectedStartDateAndNullExpectedFinishDate()
        {
            ProjectTask task = CreateTask();

            task.ExpectedStartDate = null;
            task.ExpectedFinishDate = null;

            task.ExpectedFinishDate = null;
            task.ExpectedStartDate = null;

            Assert.True(true);
        }

        [Test]
        public void ATaskCanHaveAnyExpectedStartDateAndNullExpectedFinishDate()
        {
            ProjectTask task = CreateTask();
            DateTime today = DateTime.Today;

            task.ExpectedStartDate = today;
            task.ExpectedFinishDate = null;

            task.ExpectedFinishDate = null;
            task.ExpectedStartDate = today;

            Assert.True(true);
        }

        [Test]
        public void ATaskCanHaveNullExpectedStartDateAndAnyExpectedFinishDate()
        {
            ProjectTask task = CreateTask();
            DateTime today = DateTime.Today;

            task.ExpectedStartDate = null;
            task.ExpectedFinishDate = today;

            task.ExpectedFinishDate = today;
            task.ExpectedStartDate = null;

            Assert.True(true);
        }

        [Test]
        public void ATaskCanHaveAnyExpectedStartDateAndAnyExpectedFinishDateButStartOneIsLowerThanFinishOne()
        {
            ProjectTask task = CreateTask();
            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);

            task.ExpectedStartDate = today;

            Exception caughtException = Assert.Catch(() => task.ExpectedFinishDate = yesterday);

            Assert.That(caughtException, Is.InstanceOf<InvalidFinishDateException>());
        }

        [Test]
        public void ATaskCanHaveAnyFinishExpectedDateAndAnyExpectedStartDateButStartOneIsLowerThanFinishOne()
        {
            ProjectTask task = CreateTask();
            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);

            task.ExpectedFinishDate = yesterday;

            Exception caughtException = Assert.Catch(() => task.ExpectedStartDate = today);

            Assert.That(caughtException, Is.InstanceOf<InvalidStartDateException>());
        }

        [Test]
        public void ATaskCanHaveSameStartExpectedStartDateThatExpectedFinishDate()
        {
            ProjectTask task = CreateTask();
            DateTime today = DateTime.Today;

            task.ExpectedStartDate = today;
            task.ExpectedFinishDate = today;

            Assert.True(true);
        }

        [Test]
        public void ATaskHasExpectedStartDateNormalized()
        {
            ProjectTask task = CreateTask();
            DateTime now = DateTime.Now;

            task.ExpectedStartDate = now;

            Assert.That(now.Date, Is.EqualTo(task.ExpectedStartDate));
        }

        [Test]
        public void ATaskHasExpectedFinishDateNormalized()
        {
            ProjectTask task = CreateTask();
            DateTime now = DateTime.Now;

            task.ExpectedFinishDate = now;

            Assert.That(now.Date, Is.EqualTo(task.ExpectedFinishDate));
        }

        [Test]
        public void ATaskCanHaveNullActualStartDateAndNullActualFinishDate()
        {
            ProjectTask task = CreateTask();

            task.ActualStartDate = null;
            task.ActualFinishDate = null;

            task.ActualFinishDate = null;
            task.ActualStartDate = null;

            Assert.True(true);
        }

        [Test]
        public void ATaskCanHaveAnyActualStartDateAndNullActualFinishDate()
        {
            ProjectTask task = CreateTask();
            DateTime today = DateTime.Today;

            task.ActualStartDate = today;
            task.ActualFinishDate = null;

            task.ActualFinishDate = null;
            task.ActualStartDate = today;

            Assert.True(true);
        }

        [Test]
        public void ATaskCanHaveNullActualStartDateAndAnyActualFinishDate()
        {
            ProjectTask task = CreateTask();
            DateTime today = DateTime.Today;

            task.ActualStartDate = null;
            task.ActualFinishDate = today;

            task.ActualFinishDate = today;
            task.ActualStartDate = null;

            Assert.True(true);
        }

        [Test]
        public void ATaskCanHaveAnyActualStartDateAndAnyActualFinishDateButStartOneIsLowerThanFinishOne()
        {
            ProjectTask task = CreateTask();
            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);

            task.ActualStartDate = today;

            Exception caughtException = Assert.Catch(() => task.ActualFinishDate = yesterday);

            Assert.That(caughtException, Is.InstanceOf<InvalidFinishDateException>());
        }

        [Test]
        public void ATaskCanHaveAnyFinishActualDateAndAnyActualStartDateButStartOneIsLowerThanFinishOne()
        {
            ProjectTask task = CreateTask();
            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);

            task.ActualFinishDate = yesterday;

            Exception caughtException = Assert.Catch(() => task.ActualStartDate = today);

            Assert.That(caughtException, Is.InstanceOf<InvalidStartDateException>());
        }

        [Test]
        public void ATaskCanHaveSameStartActualStartDateThatActualFinishDate()
        {
            ProjectTask task = CreateTask();
            DateTime today = DateTime.Today;

            task.ActualStartDate = today;
            task.ActualFinishDate = today;

            Assert.True(true);
        }

        [Test]
        public void ATaskHasActualStartDateNormalized()
        {
            ProjectTask task = CreateTask();
            DateTime now = DateTime.Now;

            task.ActualStartDate = now;

            Assert.That(now.Date, Is.EqualTo(task.ActualStartDate));
        }

        [Test]
        public void ATaskHasActualFinishDateNormalized()
        {
            ProjectTask task = CreateTask();
            DateTime now = DateTime.Now;

            task.ActualFinishDate = now;

            Assert.That(now.Date, Is.EqualTo(task.ActualFinishDate));
        }

        [Test]
        public void WhenChangeExpectedDatesOfATaskItChangesExpectedDatesInProject()
        {
            Project project = CreateProject();
            ProjectTask task = AddProjectTask(project);
            DateTime today = DateTime.Now.Date;
            DateTime yesterday = today.AddDays(-1);
            DateTime tomorrow = today.AddDays(1);

            task.ExpectedStartDate = yesterday;
            task.ExpectedFinishDate = tomorrow;

            Assert.That(project.ExpectedStartDate, Is.EqualTo(yesterday));
            Assert.That(project.ExpectedFinishDate, Is.EqualTo(tomorrow));
        }

        [Test]
        public void WhenChangeExpectedDatesOfSeveralTasksItChangesExpectedDatesInProject()
        {
            Project project = CreateProject();
            ProjectTask task1 = AddProjectTask(project);
            ProjectTask task2 = AddProjectTask(project);

            DateTime today = DateTime.Now.Date;
            DateTime yesterday = today.AddDays(-1);
            DateTime tomorrow = today.AddDays(1);
            DateTime previuosMonth = today.AddMonths(-1);
            DateTime nextMonth = today.AddMonths(1);

            task1.ExpectedStartDate = tomorrow;
            task1.ExpectedFinishDate = nextMonth;

            task2.ExpectedStartDate = previuosMonth;
            task2.ExpectedFinishDate = yesterday;

            Assert.That(project.ExpectedStartDate, Is.EqualTo(previuosMonth));
            Assert.That(project.ExpectedFinishDate, Is.EqualTo(nextMonth));
        }

        [Test]
        public void WhenChangeExpectedDatesOfSeveralTasksAndChildrenTasksItChangesExpectedDatesInProject()
        {
            Project project = CreateProject();
            ProjectTask task1 = AddProjectTask(project);
            ProjectTask task2 = AddProjectTask(project);
            ProjectTask childTask11 = AddProjectTask(task1);
            ProjectTask childTask12 = AddProjectTask(task1);
            ProjectTask childTask21 = AddProjectTask(task2);
            ProjectTask childTask22 = AddProjectTask(task2);

            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);
            DateTime tomorrow = today.AddDays(1);
            DateTime previuosMonth = today.AddMonths(-1);
            DateTime nextMonth = today.AddMonths(1);
            DateTime previuosYear = today.AddYears(-1);
            DateTime nextYear = today.AddYears(1);

            childTask11.ExpectedStartDate = tomorrow;
            childTask11.ExpectedFinishDate = nextMonth;

            childTask12.ExpectedStartDate = previuosYear;
            childTask12.ExpectedFinishDate = yesterday;

            childTask21.ExpectedStartDate = tomorrow;
            childTask21.ExpectedFinishDate = nextYear;

            childTask22.ExpectedStartDate = previuosMonth;
            childTask22.ExpectedFinishDate = yesterday;

            Assert.That(task1.ExpectedStartDate, Is.EqualTo(previuosYear));
            Assert.That(task1.ExpectedFinishDate, Is.EqualTo(nextMonth));
            Assert.That(task2.ExpectedStartDate, Is.EqualTo(previuosMonth));
            Assert.That(task2.ExpectedFinishDate, Is.EqualTo(nextYear));
            Assert.That(project.ExpectedStartDate, Is.EqualTo(previuosYear));
            Assert.That(project.ExpectedFinishDate, Is.EqualTo(nextYear));
        }

        private DateTime CalulateNext(DateTime date, DayOfWeek dayOfWeek)
        {
            while (date.DayOfWeek != dayOfWeek)
            {
                date = date.AddDays(1);
            }
            return date;
        }

        [Test]
        public void ATaskWithoutChildTaskHasItsOwnExpectedEffort()
        {
            DateTime today = DateTime.Today;
            DateTime monday = CalulateNext(today, DayOfWeek.Monday);
            DateTime tuesday = CalulateNext(monday, DayOfWeek.Tuesday);
            DateTime wednesday = CalulateNext(monday, DayOfWeek.Wednesday);
            DateTime thursday = CalulateNext(monday, DayOfWeek.Thursday);
            DateTime friday = CalulateNext(monday, DayOfWeek.Friday);

            Mock<IProjectCalendar> calendarMock = new Mock<IProjectCalendar>();
            calendarMock.Setup(x => x.GetWorkingHours(monday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(tuesday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(wednesday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(thursday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(friday)).Returns(8);

            Mock<IProjectCalendarFactory> calendarFactoryMock = new Mock<IProjectCalendarFactory>();
            calendarFactoryMock.Setup(x => x.CreateDefaultWorkingCalendar()).Returns(calendarMock.Object);

            ProjectFactory projectFactory = new ProjectFactory(calendarFactoryMock.Object);
            Project project = projectFactory.CreateProject(VALID_PROJECT_NAME);

            ProjectTask task = project.AddTask(VALID_PROJECTTASK_NAME);

            task.ExpectedStartDate = monday;
            task.ExpectedFinishDate = friday;

            float effort = task.CalculateExpectedEffort();

            Assert.That(effort, Is.EqualTo(40f));
        }

        [Test]
        public void ATaskWithChildTaskHasExpectedEffortOfItsChildren()
        {
            DateTime today = DateTime.Today;
            DateTime monday = CalulateNext(today, DayOfWeek.Monday);
            DateTime tuesday = CalulateNext(monday, DayOfWeek.Tuesday);
            DateTime wednesday = CalulateNext(monday, DayOfWeek.Wednesday);
            DateTime thursday = CalulateNext(monday, DayOfWeek.Thursday);
            DateTime friday = CalulateNext(monday, DayOfWeek.Friday);

            Mock<IProjectCalendar> calendarMock = new Mock<IProjectCalendar>();
            calendarMock.Setup(x => x.GetWorkingHours(monday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(tuesday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(wednesday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(thursday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(friday)).Returns(8);

            Mock<IProjectCalendarFactory> calendarFactoryMock = new Mock<IProjectCalendarFactory>();
            calendarFactoryMock.Setup(x => x.CreateDefaultWorkingCalendar()).Returns(calendarMock.Object);

            ProjectFactory projectFactory = new ProjectFactory(calendarFactoryMock.Object);
            Project project = projectFactory.CreateProject(VALID_PROJECT_NAME);

            ProjectTask parentTask = project.AddTask(VALID_PROJECTTASK_NAME);
            ProjectTask childTask1 = parentTask.AddTask(VALID_PROJECTTASK_NAME);
            ProjectTask childTask2 = parentTask.AddTask(VALID_PROJECTTASK_NAME);

            childTask1.ExpectedStartDate = monday;
            childTask1.ExpectedFinishDate = friday;

            childTask2.ExpectedStartDate = tuesday;
            childTask2.ExpectedFinishDate = friday;

            float effort = parentTask.CalculateExpectedEffort();

            Assert.That(effort, Is.EqualTo(72f));
        }

        [Test]
        public void TaskProgressCannotBeGraterThan100()
        {
            ProjectTask task = CreateTask();

            Exception caughtException = Assert.Catch(() => task.SetProgress(101));

            Assert.That(caughtException, Is.InstanceOf<InvalidActivityProgressException>());
        }

        [Test]
        public void TaskProgressCannotBeLowerThan0()
        {
            ProjectTask task = CreateTask();

            Exception caughtException = Assert.Catch(() => task.SetProgress(-1));

            Assert.That(caughtException, Is.InstanceOf<InvalidActivityProgressException>());
        }

        [Test]
        public void ATaskWithChildTaskDoesnotHaveItsOwnProgress()
        {
            ProjectTask task = CreateTask();
            ProjectTask child = AddProjectTask(task);

            Exception caughtException = Assert.Catch(() => task.SetProgress(50));

            Assert.That(caughtException, Is.InstanceOf<InvalidActivityProgressException>());
        }

        [Test]
        public void ATaskWithoutChildTaskHasItsOwnProgress()
        {
            ProjectTask task = CreateTask();
            task.SetProgress(50);

            int progress = task.CalculateProgress();

            Assert.That(progress, Is.EqualTo(50));
        }

        [Test]
        public void ATaskWithAChildTasksHasProgressBasedOnItsChild()
        {
            DateTime today = DateTime.Today;
            DateTime monday = CalulateNext(today, DayOfWeek.Monday);
            
            ProjectTask task = CreateTask();
            ProjectTask child = AddProjectTask(task);
            child.ExpectedStartDate = monday;
            child.ExpectedFinishDate = monday;
            child.SetProgress(22);

            int progress = task.CalculateProgress();

            Assert.That(progress, Is.EqualTo(22));
        }

        [Test]
        public void ATaskWithChildrenTasksHasProgressBasedOnItsChildren()
        {
            DateTime today = DateTime.Today;
            DateTime monday = CalulateNext(today, DayOfWeek.Monday);
            DateTime tuesday = CalulateNext(monday, DayOfWeek.Tuesday);
            DateTime wednesday = CalulateNext(monday, DayOfWeek.Wednesday);
            DateTime thursday = CalulateNext(monday, DayOfWeek.Thursday);
            DateTime friday = CalulateNext(monday, DayOfWeek.Friday);

            Mock<IProjectCalendar> calendarMock = new Mock<IProjectCalendar>();
            calendarMock.Setup(x => x.GetWorkingHours(monday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(tuesday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(wednesday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(thursday)).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(friday)).Returns(8);

            Mock<IProjectCalendarFactory> calendarFactoryMock = new Mock<IProjectCalendarFactory>();
            calendarFactoryMock.Setup(x => x.CreateDefaultWorkingCalendar()).Returns(calendarMock.Object);

            ProjectFactory projectFactory = new ProjectFactory(calendarFactoryMock.Object);
            Project project = projectFactory.CreateProject(VALID_PROJECT_NAME);

            ProjectTask parentTask = project.AddTask(VALID_PROJECTTASK_NAME);
            ProjectTask childTask1 = parentTask.AddTask(VALID_PROJECTTASK_NAME);
            ProjectTask childTask2 = parentTask.AddTask(VALID_PROJECTTASK_NAME);
            ProjectTask childTask3 = parentTask.AddTask(VALID_PROJECTTASK_NAME);

            // 40 hours effort, 10% progress
            childTask1.ExpectedStartDate = monday;
            childTask1.ExpectedFinishDate = friday;
            childTask1.SetProgress(10);

            // 40 hours effort, 25% progress
            childTask2.ExpectedStartDate = monday;
            childTask2.ExpectedFinishDate = friday;
            childTask2.SetProgress(25);

            // 24 hours effort, 50% progress
            childTask3.ExpectedStartDate = monday;
            childTask3.ExpectedFinishDate = wednesday;
            childTask3.SetProgress(50);

            int progress = parentTask.CalculateProgress();

            Assert.That(progress, Is.EqualTo(25));
        }
    }
}
