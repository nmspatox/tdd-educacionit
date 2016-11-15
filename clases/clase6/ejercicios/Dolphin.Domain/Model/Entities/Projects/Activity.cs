using System;
using Dolphin.Domain.Model.Entities.Calendars;
using Dolphin.Domain.Model.Exceptions;

namespace Dolphin.Domain.Model.Entities.Projects
{
    public abstract class Activity
    {
        protected IProjectCalendar calendar;
        protected string name;
        protected DateTime? expectedStartDate;
        protected DateTime? expectedFinishDate;
        protected DateTime? actualStartDate;
        protected DateTime? actualFinishDate;

        protected Activity(IProjectCalendar calendar, String name)
        {
            ValidateNullWorkingCalendar(calendar);
            ValidateNullOrEmptyProjectName(name);

            this.calendar = calendar;
            this.name = name;
        }

        private void ValidateNullOrEmptyProjectName(String name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new InvalidActivityNameException();
        }

        private void ValidateNullWorkingCalendar(IProjectCalendar calendar)
        {
            if (calendar == null)
                throw new InvalidActivityCalendarException();
        }

        public IProjectCalendar Calendar
        {
            get { return calendar; }
        }

        public string Name
        {
            get { return name; }
        }

        public DateTime? ExpectedStartDate
        {
            get { return expectedStartDate; }
            set
            {
                DateTime? date = NormalizeDate(value);
                ValidateStartDateIsBeforeFinishDate(date, expectedFinishDate, new InvalidStartDateException());

                expectedStartDate = date;
                OnSetExpectedStartDate();
            }
        }

        protected virtual void OnSetExpectedStartDate()
        {
        }

        public DateTime? ExpectedFinishDate
        {
            get { return expectedFinishDate; }
            set
            {
                DateTime? date = NormalizeDate(value);
                ValidateStartDateIsBeforeFinishDate(expectedStartDate, date, new InvalidFinishDateException());

                expectedFinishDate = date;
                OnSetExpectedFinishDate();
            }
        }

        protected virtual void OnSetExpectedFinishDate()
        {
        }

        public DateTime? ActualStartDate
        {
            get { return actualStartDate; }
            set
            {
                DateTime? date = NormalizeDate(value);
                ValidateStartDateIsBeforeFinishDate(date, actualFinishDate, new InvalidStartDateException());

                actualStartDate = date;
                OnSetActualStartDate();
            }
        }

        protected virtual void OnSetActualStartDate()
        {
        }

        public DateTime? ActualFinishDate
        {
            get { return actualFinishDate; }
            set
            {
                DateTime? date = NormalizeDate(value);
                ValidateStartDateIsBeforeFinishDate(actualStartDate, date, new InvalidFinishDateException());

                actualFinishDate = date;
                OnSetActualFinishDate();
            }
        }

        protected virtual void OnSetActualFinishDate()
        {
        }

        private void ValidateStartDateIsBeforeFinishDate(DateTime? start, DateTime? finish, ExceptionBase exceptionToThrow)
        {
            if (start != null && finish != null && start.Value > finish.Value)
                throw exceptionToThrow;
        }

        private DateTime? NormalizeDate(DateTime? date)
        {
            if (date == null)
                return date;
            return date.Value.Date;
        }
    }
}
