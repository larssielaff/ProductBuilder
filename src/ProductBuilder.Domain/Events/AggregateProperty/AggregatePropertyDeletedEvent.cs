namespace ProductBuilder.Domain.Events.AggregateProperty
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class AggregatePropertyDeletedEvent : AsdEvent
    {
        public AggregatePropertyDeletedEvent(AggregateProperty entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}