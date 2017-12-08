namespace ProductBuilder.Domain.Events.Topic
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class TopicCreatedEvent : AsdEvent
    {
        public TopicCreatedEvent(Topic entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}