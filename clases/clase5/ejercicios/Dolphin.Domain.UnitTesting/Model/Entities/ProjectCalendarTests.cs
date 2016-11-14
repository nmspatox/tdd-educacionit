using System;

using Dolphin.Domain.Model.Entities.Calendars;
using Dolphin.Domain.Model.Exceptions;
using Dolphin.Domain.Model.Factories;

using NUnit.Framework;

namespace Dolphin.Domain.Model.Entities
{
    [TestFixture]
    public class ProjectCalendarTests
    {
        private readonly DateTime monday = new DateTime(2013, 7, 1);
        private readonly DateTime tuesday = new DateTime(2013, 7, 2);
        private readonly DateTime wednesday = new DateTime(2013, 7, 3);
        private readonly DateTime thursday = new DateTime(2013, 7, 4);
        private readonly DateTime friday = new DateTime(2013, 7, 5);
        private readonly DateTime saturday = new DateTime(2013, 7, 6);
        private readonly DateTime sunday = new DateTime(2013, 7, 7);

        public ProjectCalendarTests()
        {
            Assert.That(monday.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
            Assert.That(tuesday.DayOfWeek, Is.EqualTo(DayOfWeek.Tuesday));
            Assert.That(wednesday.DayOfWeek, Is.EqualTo(DayOfWeek.Wednesday));
            Assert.That(thursday.DayOfWeek, Is.EqualTo(DayOfWeek.Thursday));
            Assert.That(friday.DayOfWeek, Is.EqualTo(DayOfWeek.Friday));
            Assert.That(saturday.DayOfWeek, Is.EqualTo(DayOfWeek.Saturday));
            Assert.That(sunday.DayOfWeek, Is.EqualTo(DayOfWeek.Sunday));
        }

        [Test]
        public void DefaultWorkingCalendarHasMondayToFridayWorkingDays()
        {
            ProjectCalendarFactory calendarFactory = new ProjectCalendarFactory();

            IProjectCalendar calendar = calendarFactory.CreateDefaultWorkingCalendar();

            Assert.That(calendar.IsWorkingDay(monday), Is.True);
            Assert.That(calendar.IsWorkingDay(tuesday), Is.True);
            Assert.That(calendar.IsWorkingDay(wednesday), Is.True);
            Assert.That(calendar.IsWorkingDay(thursday), Is.True);
            Assert.That(calendar.IsWorkingDay(friday), Is.True);
            Assert.That(calendar.IsWorkingDay(saturday), Is.False);
            Assert.That(calendar.IsWorkingDay(sunday), Is.False);
        }

        [Test]
        public void DefaultWorkingCalendarHas8WorkingHoursFromMondayToFriday()
        {
            ProjectCalendarFactory calendarFactory = new ProjectCalendarFactory();

            IProjectCalendar calendar = calendarFactory.CreateDefaultWorkingCalendar();

            Assert.That(calendar.GetWorkingHours(monday), Is.EqualTo(8));
            Assert.That(calendar.GetWorkingHours(tuesday), Is.EqualTo(8));
            Assert.That(calendar.GetWorkingHours(wednesday), Is.EqualTo(8));
            Assert.That(calendar.GetWorkingHours(thursday), Is.EqualTo(8));
            Assert.That(calendar.GetWorkingHours(friday), Is.EqualTo(8));
            Assert.That(calendar.GetWorkingHours(saturday), Is.EqualTo(0));
            Assert.That(calendar.GetWorkingHours(sunday), Is.EqualTo(0));
        }

        [Test]
        public void CannnotCreateWorkingCalendarWithRepeatedWorkingDays()
        {
            ProjectCalendarFactory calendarFactory = new ProjectCalendarFactory();

            Exception caughtException = Assert.Catch(() => calendarFactory.CreateWorkingCalendar(
                new WorkingDay[] { new WorkingDay(DayOfWeek.Monday, 8), new WorkingDay(DayOfWeek.Monday, 8) }
            ));

            Assert.That(caughtException, Is.InstanceOf<InvalidWorkingDaysException>());
        }

        [Test]
        public void CannnotCreateWorkingCalendarWithNullWorkingDays()
        {
            ProjectCalendarFactory calendarFactory = new ProjectCalendarFactory();

            Exception caughtException = Assert.Catch(() => calendarFactory.CreateWorkingCalendar(null));

            Assert.That(caughtException, Is.InstanceOf<InvalidWorkingDaysException>());
        }

        [Test]
        public void CannnotCreateWorkingCalendarWithNegativeWorkingHoursPerDay()
        {
            ProjectCalendarFactory calendarFactory = new ProjectCalendarFactory();

            Exception caughtException = Assert.Catch(() => calendarFactory.CreateWorkingCalendar(
                new WorkingDay[] { new WorkingDay(DayOfWeek.Monday, -1) }
            ));

            Assert.That(caughtException, Is.InstanceOf<InvalidWorkingHoursPerDayException>());
        }

        [Test]
        public void CannnotCreateWorkingCalendarWithGreaterThan24WorkingHoursPerDay()
        {
            ProjectCalendarFactory calendarFactory = new ProjectCalendarFactory();

            Exception caughtException = Assert.Catch(() => calendarFactory.CreateWorkingCalendar(
                new WorkingDay[] { new WorkingDay(DayOfWeek.Monday, 25) }
            ));

            Assert.That(caughtException, Is.InstanceOf<InvalidWorkingHoursPerDayException>());
        }

        [Test]
        public void ItCanAddAHolidayAndThenRetrieveIt()
        {
            ProjectCalendarFactory calendarFactory = new ProjectCalendarFactory();

            IProjectCalendar calendar = calendarFactory.CreateDefaultWorkingCalendar();
            calendar.AddHoliday(wednesday);

            bool isHoliday = calendar.IsHoliday(wednesday);

            Assert.That(isHoliday, Is.True);
        }

        [Test]
        public void IfADateIsNoHolidayItCannotRetrieveIt()
        {
            ProjectCalendarFactory calendarFactory = new ProjectCalendarFactory();

            IProjectCalendar calendar = calendarFactory.CreateDefaultWorkingCalendar();

            bool isHoliday = calendar.IsHoliday(wednesday);

            Assert.That(isHoliday, Is.False);
        }

        [Test]
        public void IfADateIsHolidayWotkingHoursAre0()
        {
            ProjectCalendarFactory calendarFactory = new ProjectCalendarFactory();

            IProjectCalendar calendar = calendarFactory.CreateDefaultWorkingCalendar();
            calendar.AddHoliday(wednesday);

            int workingHours = calendar.GetWorkingHours(wednesday);

            Assert.That(workingHours, Is.EqualTo(0));
        }
    }
}
