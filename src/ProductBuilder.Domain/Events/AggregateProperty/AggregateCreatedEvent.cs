namespace ProductBuilder.Domain.Events.AggregateProperty
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class AggregateCreatedEvent : AsdEvent
    {
        public AggregateCreatedEvent(AggregateProperty entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}