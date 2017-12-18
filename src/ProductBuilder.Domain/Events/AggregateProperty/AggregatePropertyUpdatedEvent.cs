namespace ProductBuilder.Domain.Events.AggregateProperty
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class AggregatePropertyUpdatedEvent : AsdEvent
    {
        public AggregatePropertyUpdatedEvent(AggregateProperty entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}