namespace ProductBuilder.Domain.Events.Aggregate
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class AggregateCreatedEvent : AsdEvent
    {
        public AggregateCreatedEvent(Aggregate entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}