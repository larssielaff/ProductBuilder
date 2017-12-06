namespace ProductBuilder.Domain.Events.Team
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;
    public class TeamCreatedEvent : AsdEvent
    {
        public TeamCreatedEvent(Team entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}