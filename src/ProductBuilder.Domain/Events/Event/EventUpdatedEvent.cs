namespace ProductBuilder.Domain.Events.Event
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class EventUpdatedEvent : AsdEvent
    {
        public EventUpdatedEvent(Event entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}