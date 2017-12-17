namespace ProductBuilder.Domain.Events.Aggregate
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class AggregateUpdatedEvent : AsdEvent
    {
        public AggregateUpdatedEvent(Aggregate entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}