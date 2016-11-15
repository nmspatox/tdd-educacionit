using System;
using Dolphin.Domain.Model.Entities.Calendars;

namespace Dolphin.Domain.Model.Factories
{
    public interface IProjectCalendarFactory
    {
        IProjectCalendar CreateDefaultWorkingCalendar();
        IProjectCalendar CreateWorkingCalendar(WorkingDay[] workingDays);
    }
}
