using System;

namespace Dolphin.Domain.Model.Entities.Calendars
{
    public class WorkingDay
    {
        private DayOfWeek dayOfWeek;
        private int workingHours;

        public WorkingDay(DayOfWeek dayOfWeek, int workingHours)
        {
            this.dayOfWeek = dayOfWeek;
            this.workingHours = workingHours;
        }

        public DayOfWeek DayOfWeek
        {
            get { return dayOfWeek; }
        }

        public int WorkingHours
        {
            get { return workingHours; }
        }
    }
}
