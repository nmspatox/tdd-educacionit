using System;
using System.Collections.Generic;
using System.Linq;
using Dolphin.Domain.Model.Entities.Calendars;
using Dolphin.Domain.Model.Entities.Projects;
using Dolphin.Domain.Model.Exceptions;
using Dolphin.Domain.Model.Factories;
using Moq;
using NUnit.Framework;

namespace Dolphin.Domain.Model.Entities
{
    [TestFixture]
    public class ProjectTests
    {
        private const string VALID_PROJECT_NAME = "Project Name";
        private const string VALID_PROJECTTASK_NAME = "Project Task Name";
        private readonly ProjectCalendarFactory calendarFactory = new ProjectCalendarFactory();

        [Test]
        public void CannotCreateAProjectWithoutAWorkingCalendar()
        {
            ProjectFactory projectFactory = new ProjectFactory(calendarFactory);

            Exception caughtException = Assert.Catch(() => projectFactory.CreateProject(null, VALID_PROJECT_NAME));

            Assert.That(caughtException, Is.InstanceOf<InvalidActivityCalendarException>());
        }

        [Test]
        public void CannotCreateAProjectWithNullName()
        {
            ProjectFactory projectFactory = new ProjectFactory(calendarFactory);
            ProjectCalendarFactory workingCalendarFactory = new ProjectCalendarFactory();
            IProjectCalendar calendar = workingCalendarFactory.CreateDefaultWorkingCalendar();

            Exception caughtException = Assert.Catch(() => projectFactory.CreateProject(calendar, null));

            Assert.That(caughtException, Is.InstanceOf<InvalidActivityNameException>());
        }

        [Test]
        public void CannotCreateAProjectWithEmptyName()
        {
            ProjectFactory projectFactory = new ProjectFactory(calendarFactory);
            ProjectCalendarFactory workingCalendarFactory = new ProjectCalendarFactory();
            IProjectCalendar calendar = workingCalendarFactory.CreateDefaultWorkingCalendar();

            Exception caughtException = Assert.Catch(() => projectFactory.CreateProject(calendar, ""));

            Assert.That(caughtException, Is.InstanceOf<InvalidActivityNameException>());
        }

        [Test]
        public void AProjectWithoutWorkingCalendarIsCreatedWithDefaultWorkingCalendar()
        {
            DateTime monday = new DateTime(2013, 7, 1);
            DateTime tuesday = new DateTime(2013, 7, 2);
            DateTime wednesday = new DateTime(2013, 7, 3);
            DateTime thursday = new DateTime(2013, 7, 4);
            DateTime friday = new DateTime(2013, 7, 5);
            DateTime saturday = new DateTime(2013, 7, 6);
            DateTime sunday = new DateTime(2013, 7, 7);

            Mock<IProjectCalendar> calendarMock = new Mock<IProjectCalendar>();
            calendarMock.Setup(x => x.GetWorkingHours(It.IsAny<DateTime>())).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(saturday)).Returns(0);
            calendarMock.Setup(x => x.GetWorkingHours(sunday)).Returns(0);
            calendarMock.Setup(x => x.IsWorkingDay(It.IsAny<DateTime>())).Returns(true);
            calendarMock.Setup(x => x.IsWorkingDay(saturday)).Returns(false);
            calendarMock.Setup(x => x.IsWorkingDay(sunday)).Returns(false);

            Project project = new Project(calendarMock.Object, VALID_PROJECT_NAME);

            IProjectCalendar calendar = project.Calendar;

            Assert.That(calendar.IsWorkingDay(monday), Is.True);
            Assert.That(calendar.IsWorkingDay(tuesday), Is.True);
            Assert.That(calendar.IsWorkingDay(wednesday), Is.True);
            Assert.That(calendar.IsWorkingDay(thursday), Is.True);
            Assert.That(calendar.IsWorkingDay(friday), Is.True);
            Assert.That(calendar.IsWorkingDay(saturday), Is.False);
            Assert.That(calendar.IsWorkingDay(sunday), Is.False);

            Assert.That(calendar.GetWorkingHours(monday), Is.EqualTo(8));
            Assert.That(calendar.GetWorkingHours(tuesday), Is.EqualTo(8));
            Assert.That(calendar.GetWorkingHours(wednesday), Is.EqualTo(8));
            Assert.That(calendar.GetWorkingHours(thursday), Is.EqualTo(8));
            Assert.That(calendar.GetWorkingHours(friday), Is.EqualTo(8));
            Assert.That(calendar.GetWorkingHours(saturday), Is.EqualTo(0));
            Assert.That(calendar.GetWorkingHours(sunday), Is.EqualTo(0));
        }

        [Test]
        public void AProjectHasName()
        {
            ProjectFactory projectFactory = new ProjectFactory(calendarFactory);
            Project project = projectFactory.CreateProject(VALID_PROJECT_NAME);

            string projectName = project.Name;

            Assert.That(projectName, Is.EqualTo(VALID_PROJECT_NAME));
        }

        private Project CreateProjectWithDefaultNameAndDefaultCalendar() {
            ProjectFactory projectFactory = new ProjectFactory(calendarFactory);
            return projectFactory.CreateProject(VALID_PROJECT_NAME);
        }

        private Project CreateProjectWithDefaultNameAndDefaultCalendar(IProjectCalendar calendar)
        {
            ProjectFactory projectFactory = new ProjectFactory(calendarFactory);
            return projectFactory.CreateProject(calendar, VALID_PROJECT_NAME);
        }

        [Test]
        public void AProjectCanHaveNullExpectedStartDateAndNullExpectedFinishDate()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();

            project.ExpectedStartDate = null;
            project.ExpectedFinishDate = null;

            project.ExpectedFinishDate = null;
            project.ExpectedStartDate = null;

            Assert.True(true);
        }

        [Test]
        public void AProjectCanHaveAnyExpectedStartDateAndNullExpectedFinishDate()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime today = DateTime.Today;

            project.ExpectedStartDate = today;
            project.ExpectedFinishDate = null;

            project.ExpectedFinishDate = null;
            project.ExpectedStartDate = today;

            Assert.True(true);
        }

        [Test]
        public void AProjectCanHaveNullExpectedStartDateAndAnyExpectedFinishDate()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime today = DateTime.Today;

            project.ExpectedStartDate = null;
            project.ExpectedFinishDate = today;

            project.ExpectedFinishDate = today;
            project.ExpectedStartDate = null;

            Assert.True(true);
        }

        [Test]
        public void AProjectCanHaveAnyExpectedStartDateAndAnyExpectedFinishDateButStartOneIsLowerThanFinishOne()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);

            project.ExpectedStartDate = today;

            Exception caughtException = Assert.Catch(() => project.ExpectedFinishDate = yesterday);

            Assert.That(caughtException, Is.InstanceOf<InvalidFinishDateException>());
        }

        [Test]
        public void AProjectCanHaveAnyFinishExpectedDateAndAnyExpectedStartDateButStartOneIsLowerThanFinishOne()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);

            project.ExpectedFinishDate = yesterday;

            Exception caughtException = Assert.Catch(() => project.ExpectedStartDate = today);

            Assert.That(caughtException, Is.InstanceOf<InvalidStartDateException>());
        }

        [Test]
        public void AProjectCanHaveSameStartExpectedStartDateThatExpectedFinishDate()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime today = DateTime.Today;

            project.ExpectedStartDate = today;
            project.ExpectedFinishDate = today;

            Assert.True(true);
        }

        [Test]
        public void AProjectHasExpectedStartDateNormalized()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime now = DateTime.Now;

            project.ExpectedStartDate = now;

            Assert.That(now.Date, Is.EqualTo(project.ExpectedStartDate));
        }

        [Test]
        public void AProjectHasExpectedFinishDateNormalized()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime now = DateTime.Now;

            project.ExpectedFinishDate = now;

            Assert.That(now.Date, Is.EqualTo(project.ExpectedFinishDate));
        }

        [Test]
        public void AProjectCanHaveNullActualStartDateAndNullActualFinishDate()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();

            project.ActualStartDate = null;
            project.ActualFinishDate = null;

            project.ActualFinishDate = null;
            project.ActualStartDate = null;

            Assert.True(true);
        }

        [Test]
        public void AProjectCanHaveAnyActualStartDateAndNullActualFinishDate()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime today = DateTime.Today;

            project.ActualStartDate = today;
            project.ActualFinishDate = null;

            project.ActualFinishDate = null;
            project.ActualStartDate = today;

            Assert.True(true);
        }

        [Test]
        public void AProjectCanHaveNullActualStartDateAndAnyActualFinishDate()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime today = DateTime.Today;

            project.ActualStartDate = null;
            project.ActualFinishDate = today;

            project.ActualFinishDate = today;
            project.ActualStartDate = null;

            Assert.True(true);
        }

        [Test]
        public void AProjectCanHaveAnyActualStartDateAndAnyActualFinishDateButStartOneIsLowerThanFinishOne()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);

            project.ActualStartDate = today;

            Exception caughtException = Assert.Catch(() => project.ActualFinishDate = yesterday);

            Assert.That(caughtException, Is.InstanceOf<InvalidFinishDateException>());
        }

        [Test]
        public void AProjectCanHaveAnyFinishActualDateAndAnyActualStartDateButStartOneIsLowerThanFinishOne()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime today = DateTime.Today;
            DateTime yesterday = today.AddDays(-1);

            project.ActualFinishDate = yesterday;

            Exception caughtException = Assert.Catch(() => project.ActualStartDate = today);

            Assert.That(caughtException, Is.InstanceOf<InvalidStartDateException>());
        }

        [Test]
        public void AProjectCanHaveSameStartActualStartDateThatActualFinishDate()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime today = DateTime.Today;

            project.ActualStartDate = today;
            project.ActualFinishDate = today;

            Assert.True(true);
        }

        [Test]
        public void AProjectHasActualStartDateNormalized()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime now = DateTime.Now;

            project.ActualStartDate = now;

            Assert.That(now.Date, Is.EqualTo(project.ActualStartDate));
        }

        [Test]
        public void AProjectHasActualFinishDateNormalized()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            DateTime now = DateTime.Now;

            project.ActualFinishDate = now;

            Assert.That(now.Date, Is.EqualTo(project.ActualFinishDate));
        }

        private ProjectTask AddProjectTask(Project project)
        {
            return project.AddTask(VALID_PROJECTTASK_NAME);
        }

        [Test]
        public void ANewProjectDoesNotHaveTasks()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();

            IEnumerable<ProjectTask> tasks = project.Tasks;

            Assert.That(tasks.Any(), Is.False);
        }

        [Test]
        public void WhenAddATaskThenTheProjectHasOneTask()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();

            ProjectTask task = AddProjectTask(project);
            IEnumerable<ProjectTask> tasks = project.Tasks;
            ProjectTask firstTask = tasks.First();

            Assert.That(tasks.Count(), Is.EqualTo(1));
            Assert.That(firstTask, Is.SameAs(task));
        }

        [Test]
        public void WhenAddThreeTasksProjectHasThreeTasks()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();

            ProjectTask task1 = AddProjectTask(project);
            ProjectTask task2 = AddProjectTask(project);
            ProjectTask task3 = AddProjectTask(project);

            IEnumerable<ProjectTask> tasks = project.Tasks;
            ProjectTask firstTask = tasks.ElementAt(0);
            ProjectTask secondTask = tasks.ElementAt(1);
            ProjectTask thirdTask = tasks.ElementAt(2);

            Assert.That(tasks.Count(), Is.EqualTo(3));
            Assert.That(firstTask, Is.SameAs(task1));
            Assert.That(secondTask, Is.SameAs(task2));
            Assert.That(thirdTask, Is.SameAs(task3));
        }

        [Test]
        public void WhenAddATaskItIsPossibleRemoveIt()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();

            ProjectTask task = AddProjectTask(project);
            project.RemoveTask(task);

            IEnumerable<ProjectTask> tasks = project.Tasks;

            Assert.That(tasks.Any(), Is.False);
        }

        [Test]
        public void ItIsNotPossibleRemoveATaskWichIsNotAddedBefore()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            ProjectTask foreignTask = AddProjectTask(CreateProjectWithDefaultNameAndDefaultCalendar());

            Exception caughtException = Assert.Catch(() => project.RemoveTask(foreignTask));

            Assert.That(caughtException, Is.InstanceOf<ProjectTaskDoesNotExistException>());
        }

        [Test]
        public void ItIsNotPossibleRemoveATaskWithNullReference()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();

            Exception caughtException = Assert.Catch(() => project.RemoveTask(null));

            Assert.That(caughtException, Is.InstanceOf<ProjectTaskNullException>());
        }

        [Test]
        public void WhenAddThreeTasksToAProjectItIsPossibleRemoveThem()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();

            ProjectTask task1 = AddProjectTask(project);
            ProjectTask task2 = AddProjectTask(project);
            ProjectTask task3 = AddProjectTask(project);

            project.RemoveTask(task1);
            project.RemoveTask(task2);
            project.RemoveTask(task3);

            IEnumerable<ProjectTask> tasks = project.Tasks;

            Assert.That(tasks.Any(), Is.False);
        }

        [Test]
        public void ItIsPossibleRemoveAllTasksInAProject()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            AddProjectTask(project);
            AddProjectTask(project);
            AddProjectTask(project);

            project.RemoveAllTasks();

            IEnumerable<ProjectTask> tasks = project.Tasks;

            Assert.That(tasks.Any(), Is.False);
        }

        [Test]
        public void TasksAddedToAProjectAreTheSameOnes()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();

            List<ProjectTask> tasks = new List<ProjectTask>();
            ProjectTask task1 = AddProjectTask(project);
            tasks.Add(task1);
            ProjectTask task2 = AddProjectTask(project);
            tasks.Add(task2);
            ProjectTask task3 = AddProjectTask(project);
            tasks.Add(task3);

            IEnumerable<ProjectTask> projectTasks = project.Tasks;

            CollectionAssert.AreEquivalent(projectTasks, tasks);
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
        public void AProjectWhichLastAWeekHas40WorkingHoursExpectedEffort()
        {
            DateTime today = DateTime.Today;
            DateTime monday = CalulateNext(today, DayOfWeek.Monday);
            DateTime tuesday = CalulateNext(monday, DayOfWeek.Tuesday);
            DateTime wednesday = CalulateNext(monday, DayOfWeek.Wednesday);
            DateTime thursday = CalulateNext(monday, DayOfWeek.Thursday);
            DateTime friday = CalulateNext(monday, DayOfWeek.Friday);

            Mock<IProjectCalendar> calendarMock = new Mock<IProjectCalendar>();
            calendarMock.Setup(x => x.GetWorkingHours(It.IsAny<DateTime>())).Returns(8);
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar(calendarMock.Object);

            project.ExpectedStartDate = monday;
            project.ExpectedFinishDate = friday;

            float effort = project.CalculateExpectedEffort();

            Assert.That(effort, Is.EqualTo(40f));
        }

        [Test]
        public void AProjectWhichLastFromMondayToThursdayAnd6WorkingHoursPerDayHas24WorkingHoursExpectedEffort()
        {
            DateTime today = DateTime.Today;
            DateTime monday = CalulateNext(today, DayOfWeek.Monday);
            DateTime tuesday = CalulateNext(monday, DayOfWeek.Tuesday);
            DateTime wednesday = CalulateNext(monday, DayOfWeek.Wednesday);
            DateTime thursday = CalulateNext(monday, DayOfWeek.Thursday);
            
            Mock<IProjectCalendar> calendarMock = new Mock<IProjectCalendar>();
            calendarMock.Setup(x => x.GetWorkingHours(It.IsAny<DateTime>())).Returns(6);

            ProjectFactory projectFactory = new ProjectFactory(calendarFactory);
            Project project = projectFactory.CreateProject(calendarMock.Object, VALID_PROJECT_NAME);

            project.ExpectedStartDate = monday;
            project.ExpectedFinishDate = thursday;

            float effort = project.CalculateExpectedEffort();

            Assert.That(effort, Is.EqualTo(24f));
        }

        [Test]
        public void AProjectWhichLastFromMondayToThursdayAnd6WorkingHoursPerDayAndHasWednesdayHolidayHas18WorkingHoursExpectedEffort()
        {
            DateTime today = DateTime.Today;
            DateTime monday = CalulateNext(today, DayOfWeek.Monday);
            DateTime tuesday = CalulateNext(monday, DayOfWeek.Tuesday);
            DateTime wednesday = CalulateNext(monday, DayOfWeek.Wednesday);
            DateTime thursday = CalulateNext(monday, DayOfWeek.Thursday);

            Mock<IProjectCalendar> calendarMock = new Mock<IProjectCalendar>();
            calendarMock.Setup(x => x.GetWorkingHours(It.IsAny<DateTime>())).Returns(6);
            calendarMock.Setup(x => x.GetWorkingHours(wednesday)).Returns(0);


            ProjectFactory projectFactory = new ProjectFactory(calendarFactory);
            Project project = projectFactory.CreateProject(calendarMock.Object, VALID_PROJECT_NAME);
            
            project.ExpectedStartDate = monday;
            project.ExpectedFinishDate = thursday;

            float effort = project.CalculateExpectedEffort();

            Assert.That(effort, Is.EqualTo(18f));
        }

        [Test]
        public void AProjectWhichLastFromMondayToFridayOfNextWeekAnd8WorkingHoursPerDayHas80WorkingHoursExpectedEffort()
        {
            DateTime today = DateTime.Today;
            DateTime mondayWeek1 = CalulateNext(today, DayOfWeek.Monday);
            DateTime tuesdayWeek1 = CalulateNext(mondayWeek1, DayOfWeek.Tuesday);
            DateTime wednesdayWeek1 = CalulateNext(mondayWeek1, DayOfWeek.Wednesday);
            DateTime thursdayWeek1 = CalulateNext(mondayWeek1, DayOfWeek.Thursday);
            DateTime fridayWeek1 = CalulateNext(mondayWeek1, DayOfWeek.Friday);
            DateTime saturdayWeek1 = CalulateNext(mondayWeek1, DayOfWeek.Saturday);
            DateTime sundayWeek1 = CalulateNext(mondayWeek1, DayOfWeek.Sunday);
            DateTime mondayWeek2 = mondayWeek1.AddDays(7);
            DateTime tuesdayWeek2 = CalulateNext(mondayWeek2, DayOfWeek.Tuesday);
            DateTime wednesdayWeek2 = CalulateNext(mondayWeek2, DayOfWeek.Wednesday);
            DateTime thursdayWeek2 = CalulateNext(mondayWeek2, DayOfWeek.Thursday);
            DateTime fridayWeek2 = CalulateNext(mondayWeek2, DayOfWeek.Friday);

            Mock<IProjectCalendar> calendarMock = new Mock<IProjectCalendar>();
            calendarMock.Setup(x => x.GetWorkingHours(It.IsAny<DateTime>())).Returns(8);
            calendarMock.Setup(x => x.GetWorkingHours(saturdayWeek1)).Returns(0);
            calendarMock.Setup(x => x.GetWorkingHours(sundayWeek1)).Returns(0);            
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar(calendarMock.Object);
            project.ExpectedStartDate = mondayWeek1;
            project.ExpectedFinishDate = fridayWeek2;

            float effort = project.CalculateExpectedEffort();

            Assert.That(effort, Is.EqualTo(80f));
        }

        [Test]
        public void AProjectWhichDoesNotHaveExpectedStartDateNeitherExpecteFinishDateHas0ExpectedEffort()
        {
            Project project = CreateProjectWithDefaultNameAndDefaultCalendar();
            project.ExpectedStartDate = null;
            project.ExpectedFinishDate = null;

            float effort = project.CalculateExpectedEffort();

            Assert.That(effort, Is.EqualTo(0f));
        }
    }
}
