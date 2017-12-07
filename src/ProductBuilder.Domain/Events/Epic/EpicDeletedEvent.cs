namespace ProductBuilder.Domain.Events.Epic
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class EpicDeletedEvent : AsdEvent
    {
        public EpicDeletedEvent(Epic entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}