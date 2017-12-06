namespace ProductBuilder.Domain.Commands.TeamMember.Base
{
    using Asd.Domain.Core.Commands;
    using System;
    public abstract class TeamMemberCommand : AsdCommand
    {
        public Guid Id { get; protected set; }
        public Guid UserProfileId { get; protected set; }
        public string Role { get; protected set; }
        public Guid TeamId { get; protected set; }
    }
}