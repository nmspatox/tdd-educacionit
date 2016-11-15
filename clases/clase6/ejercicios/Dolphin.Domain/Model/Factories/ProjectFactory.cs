using Dolphin.Domain.Model.Entities.Calendars;
using Dolphin.Domain.Model.Entities.Projects;

namespace Dolphin.Domain.Model.Factories
{
    public class ProjectFactory
    {
        private IProjectCalendarFactory calendarFactory;

        public ProjectFactory(IProjectCalendarFactory calendarFactory)
        {
            this.calendarFactory = calendarFactory;
        }

        public Project CreateProject(IProjectCalendar calendar, string name)
        {
            return new Project(calendar, name);
        }

        public Project CreateProject(string name)
        {
            return CreateProject(calendarFactory.CreateDefaultWorkingCalendar(), name);
        }
    }
}
