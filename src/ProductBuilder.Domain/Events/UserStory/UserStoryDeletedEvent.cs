namespace ProductBuilder.Domain.Events.UserStory
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class UserStoryDeletedEvent : AsdEvent
    {
        public UserStoryDeletedEvent(UserStory entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}