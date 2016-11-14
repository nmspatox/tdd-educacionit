using System;

namespace Dolphin.Domain.Model.Entities.Calendars
{
    public interface IProjectCalendar
    {
        void AddHoliday(DateTime date);
        int GetWorkingHours(DateTime date);
        bool IsHoliday(DateTime date);
        bool IsWorkingDay(DateTime date);
    }
}
