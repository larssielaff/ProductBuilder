namespace ProductBuilder.Domain.Events.AggregateProperty
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class AggregateDeletedEvent : AsdEvent
    {
        public AggregateDeletedEvent(AggregateProperty entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}