using System;
using System.Collections.Generic;
using Dolphin.Domain.Model.Entities.Calendars;
using Dolphin.Domain.Model.Exceptions;

namespace Dolphin.Domain.Model.Entities.Projects
{
    public class Project : ProjectTask
    {
        private IList<ProjectMember> members;        

        public Project(IProjectCalendar calendar, string name)
            : base(null, calendar, name)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            members = new List<ProjectMember>();
        }

        public ProjectMember AddMember(string memberName)
        {
            ProjectMember member = new ProjectMember(memberName);
            members.Add(member);
            return member;
        }

        public IEnumerable<ProjectMember> Members
        {
            get { return members; }
        }        

        public void RemoveMember(ProjectMember member)
        {
            ValidateMemberIsNotNull(member);
            ValidateMemberBelongsToProject(member);

            members.Remove(member);
        }

        public void RemoveAllMembers()
        {
            InitializeMembers();
        }

        private void ValidateMemberIsNotNull(ProjectMember member)
        {
            if (member == null)
                throw new ProjectMemberNullException();
        }

        private void ValidateMemberBelongsToProject(ProjectMember member)
        {
            if (!members.Contains(member))
                throw new ProjectMemberDoesNotExistException();
        }
    }
}
