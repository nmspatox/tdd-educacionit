using System;
using System.Collections.Generic;

using Dolphin.Domain.Model.Exceptions;

namespace Dolphin.Domain.Model.Entities.Calendars
{
    public class ProjectCalendar : IProjectCalendar
    {
        private WorkingDay[] workingDays;
        private IList<DateTime> holidays;

        public ProjectCalendar(WorkingDay[] workingDays)
        {
            ValidateNullOrEmptyWorkingDays(workingDays);
            ValidateWorkingHours(workingDays);
            ValidateNonDuplicatedWorkingDays(workingDays);

            this.workingDays = workingDays;
            this.holidays = new List<DateTime>();
        }

        private void ValidateWorkingHours(WorkingDay[] workingDays)
        {
            foreach (WorkingDay workingDay in workingDays)
            {
                if (workingDay.WorkingHours < 0 || workingDay.WorkingHours > 24)
                {
                    throw new InvalidWorkingHoursPerDayException();
                }
            }
        }

        private void ValidateNullOrEmptyWorkingDays(WorkingDay[] workingDays)
        {
            if (workingDays == null || workingDays.Length == 0)
            {
                throw new InvalidWorkingDaysException();
            }
        }

        private void ValidateNonDuplicatedWorkingDays(WorkingDay[] workingDays)
        {
            List<DayOfWeek> days = new List<DayOfWeek>();

            foreach (WorkingDay workingDay in workingDays)
            {
                if (days.Contains(workingDay.DayOfWeek))
                {
                    throw new InvalidWorkingDaysException();
                }
                else
                {
                    days.Add(workingDay.DayOfWeek);
                }
            }
        }

        public bool IsWorkingDay(DateTime date)
        {
            foreach (WorkingDay workingDay in workingDays)
            {
                if (date.DayOfWeek == workingDay.DayOfWeek)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetWorkingHours(DateTime date)
        {
            if (IsHoliday(date))
                return 0;

            foreach (WorkingDay workingDay in workingDays)
            {
                if (date.DayOfWeek == workingDay.DayOfWeek)
                {
                    return workingDay.WorkingHours;
                }
            }

            return 0;
        }

        public void AddHoliday(DateTime date)
        {
            holidays.Add(date.Date);
        }

        public bool IsHoliday(DateTime date)
        {
            return holidays.Contains(date.Date);
        }
    }
}
