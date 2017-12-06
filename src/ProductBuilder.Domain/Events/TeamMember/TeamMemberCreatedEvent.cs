namespace ProductBuilder.Domain.Events.TeamMember
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;
    public class TeamMemberCreatedEvent : AsdEvent
    {
        public TeamMemberCreatedEvent(TeamMember entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}