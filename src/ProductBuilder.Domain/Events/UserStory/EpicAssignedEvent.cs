namespace ProductBuilder.Domain.Events.UserStory
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class EpicAssignedEvent : AsdEvent
    {
        public EpicAssignedEvent(UserStory entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}