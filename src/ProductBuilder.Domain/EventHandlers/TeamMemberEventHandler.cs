namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.TeamMember;
    public class TeamMemberEventHandler : IAsdHandler<TeamMemberCreatedEvent>
    {
        public void Handle(TeamMemberCreatedEvent message) { }
    }
}
