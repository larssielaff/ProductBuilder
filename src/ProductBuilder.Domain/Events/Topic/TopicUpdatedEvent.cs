namespace ProductBuilder.Domain.Events.Topic
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class TopicUpdatedEvent : AsdEvent
    {
        public TopicUpdatedEvent(Topic entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}