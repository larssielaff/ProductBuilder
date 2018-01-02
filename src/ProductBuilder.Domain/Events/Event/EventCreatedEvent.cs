namespace ProductBuilder.Domain.Events.Event
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class EventCreatedEvent : AsdEvent
    {
        public EventCreatedEvent(Event entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}