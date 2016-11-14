using System;
using Dolphin.Domain.Model.Exceptions;

namespace Dolphin.Domain.Model.Entities.Projects
{
    public class ProjectMember
    {
        private string name;

        public ProjectMember(string name)
        {
            ValidateNullOrEmptyProjectMemberName(name);
            this.name = name;
        }

        private void ValidateNullOrEmptyProjectMemberName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new InvalidProjectMemberNameException();
        }

        public string Name
        {
            get { return name; }
        }
    }
}
