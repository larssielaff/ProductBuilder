namespace ProductBuilder.Domain.Events.UserProfile
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;
    public class UserProfileCreatedEvent : AsdEvent
    {
        public UserProfileCreatedEvent(UserProfile entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}