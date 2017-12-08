namespace ProductBuilder.Domain.Events.UserStory
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class EpicRemovedEvent : AsdEvent
    {
        public EpicRemovedEvent(UserStory entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}