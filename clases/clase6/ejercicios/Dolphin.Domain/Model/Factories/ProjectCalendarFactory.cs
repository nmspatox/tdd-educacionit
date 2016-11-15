using System;

using Dolphin.Domain.Model.Entities.Calendars;

namespace Dolphin.Domain.Model.Factories
{
    public class ProjectCalendarFactory : IProjectCalendarFactory
    {
        public IProjectCalendar CreateDefaultWorkingCalendar()
        {
            return CreateWorkingCalendar(new WorkingDay[] {
				new WorkingDay(DayOfWeek.Monday, 8),
				new WorkingDay(DayOfWeek.Tuesday, 8),
				new WorkingDay(DayOfWeek.Wednesday, 8),
				new WorkingDay(DayOfWeek.Thursday, 8),
				new WorkingDay(DayOfWeek.Friday, 8)
            });
        }

        public IProjectCalendar CreateWorkingCalendar(WorkingDay[] workingDays)
        {
            return new ProjectCalendar(workingDays);
        }
    }
}
