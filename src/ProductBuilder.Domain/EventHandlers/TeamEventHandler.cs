namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.Team;
    public class TeamEventHandler : IAsdHandler<TeamCreatedEvent>
    {
        public void Handle(TeamCreatedEvent message) { }
    }
}
