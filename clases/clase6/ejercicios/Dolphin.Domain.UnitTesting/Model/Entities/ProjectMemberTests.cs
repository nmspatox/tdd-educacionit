using System;
using System.Collections.Generic;
using System.Linq;
using Dolphin.Domain.Model.Entities.Projects;
using Dolphin.Domain.Model.Exceptions;
using Dolphin.Domain.Model.Factories;
using NUnit.Framework;

namespace Dolphin.Domain.Model.Entities
{
    [TestFixture]
    public class ProjectMemberTests
    {
        private const string VALID_PROJECT_NAME = "Project Name";
        private const string VALID_PROJECTMEMBER_NAME = "Project Member Name";

        private static ProjectFactory projectFactory;
        private static ProjectCalendarFactory projectCalendarFactory;

        [TestFixtureSetUp]
        public static void TestFixtureSetUp()
        {
            projectCalendarFactory = new ProjectCalendarFactory();
            projectFactory = new ProjectFactory(projectCalendarFactory);
        }

        private Project CreateProject()
        {
            return projectFactory.CreateProject(VALID_PROJECT_NAME);
        }

        private ProjectMember AddProjectMember(Project project)
        {
            return project.AddMember(VALID_PROJECTMEMBER_NAME);
        }

        [Test]
        public void AProjectMemberCannotHaveEmptyName()
        {
            Project project = CreateProject();

            Exception caughtException = Assert.Catch(() => project.AddMember(""));

            Assert.That(caughtException, Is.InstanceOf<InvalidProjectMemberNameException>());
        }

        [Test]
        public void AProjectMemberCannotHaveNullName()
        {
            Project project = CreateProject();

            Exception caughtException = Assert.Catch(() => project.AddMember(null));

            Assert.That(caughtException, Is.InstanceOf<InvalidProjectMemberNameException>());
        }

        [Test]
        public void AProjectMemberHasName()
        {
            Project project = CreateProject();
            ProjectMember member = project.AddMember(VALID_PROJECTMEMBER_NAME);

            string name = member.Name;

            Assert.That(name, Is.EqualTo(VALID_PROJECTMEMBER_NAME));
        }

        [Test]
        public void ANewProjectDoesNotHaveMembers()
        {
            Project project = CreateProject();

            IEnumerable<ProjectMember> members = project.Members;

            Assert.That(members.Any(), Is.False);
        }

        [Test]
        public void WhenAddAMemberThenTheProjectHasOneMember()
        {
            Project project = CreateProject();

            ProjectMember member = AddProjectMember(project);
            IEnumerable<ProjectMember> members = project.Members;
            ProjectMember firstMember = members.First();

            Assert.That(members.Count(), Is.EqualTo(1));
            Assert.That(firstMember, Is.SameAs(member));
        }

        [Test]
        public void WhenAddThreeMembersProjectHasThreeMembers()
        {
            Project project = CreateProject();

            ProjectMember member1 = AddProjectMember(project);
            ProjectMember member2 = AddProjectMember(project);
            ProjectMember member3 = AddProjectMember(project);

            IEnumerable<ProjectMember> members = project.Members;
            ProjectMember firstMember = members.First();
            ProjectMember secondMember = members.Skip(1).First();
            ProjectMember thirdMember = members.Skip(2).First();

            Assert.That(members.Count(), Is.EqualTo(3));
            Assert.That(firstMember, Is.SameAs(member1));
            Assert.That(secondMember, Is.SameAs(member2));
            Assert.That(thirdMember, Is.SameAs(member3));
        }

        [Test]
        public void WhenAddAMemberItIsPossibleRemoveIt()
        {
            Project project = CreateProject();

            ProjectMember member = AddProjectMember(project);
            project.RemoveMember(member);

            IEnumerable<ProjectMember> members = project.Members;

            Assert.That(members.Any(), Is.False);
        }

        [Test]
        public void ItIsNotPossibleRemoveAMemberWichIsNotAddedBefore()
        {
            Project project = CreateProject();
            ProjectMember foreignMember = AddProjectMember(CreateProject());

            Exception caughtException = Assert.Catch(() => project.RemoveMember(foreignMember));

            Assert.That(caughtException, Is.InstanceOf<ProjectMemberDoesNotExistException>());
        }

        [Test]
        public void ItIsNotPossibleRemoveAMemberWithNullReference()
        {
            Project project = CreateProject();

            Exception caughtException = Assert.Catch(() => project.RemoveMember(null));

            Assert.That(caughtException, Is.InstanceOf<ProjectMemberNullException>());
        }

        [Test]
        public void WhenAddThreeMembersToAProjectItIsPossibleRemoveThem()
        {
            Project project = CreateProject();

            ProjectMember member1 = AddProjectMember(project);
            ProjectMember member2 = AddProjectMember(project);
            ProjectMember member3 = AddProjectMember(project);

            project.RemoveMember(member1);
            project.RemoveMember(member2);
            project.RemoveMember(member3);

            IEnumerable<ProjectMember> members = project.Members;

            Assert.That(members.Any(), Is.False);
        }

        [Test]
        public void ItIsPossibleRemoveAllMembersInAProject()
        {
            Project project = CreateProject();
            AddProjectMember(project);
            AddProjectMember(project);
            AddProjectMember(project);

            project.RemoveAllMembers();

            IEnumerable<ProjectMember> members = project.Members;

            Assert.That(members.Any(), Is.False);
        }

        [Test]
        public void MembersAddedToAProjectAreTheSameOnes()
        {
            Project project = CreateProject();

            List<ProjectMember> members = new List<ProjectMember>();
            ProjectMember member1 = AddProjectMember(project);
            members.Add(member1);
            ProjectMember member2 = AddProjectMember(project);
            members.Add(member2);
            ProjectMember member3 = AddProjectMember(project);
            members.Add(member3);

            IEnumerable<ProjectMember> projectMembers = project.Members;

            CollectionAssert.AreEquivalent(projectMembers, members);
        }
    }
}
