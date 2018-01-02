namespace ProductBuilder.Domain.Events.Event
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class EventDeletedEvent : AsdEvent
    {
        public EventDeletedEvent(Event entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}