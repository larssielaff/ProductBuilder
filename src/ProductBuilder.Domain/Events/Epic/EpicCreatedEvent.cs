namespace ProductBuilder.Domain.Events.Epic
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class EpicCreatedEvent : AsdEvent
    {
        public EpicCreatedEvent(Epic entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}