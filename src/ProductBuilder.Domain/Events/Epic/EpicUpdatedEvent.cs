namespace ProductBuilder.Domain.Events.Epic
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class EpicUpdatedEvent : AsdEvent
    {
        public EpicUpdatedEvent(Epic entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}