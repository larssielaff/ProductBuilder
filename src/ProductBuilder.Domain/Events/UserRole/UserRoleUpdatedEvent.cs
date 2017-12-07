namespace ProductBuilder.Domain.Events.UserRole
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class UserRoleUpdatedEvent : AsdEvent
    {
        public UserRoleUpdatedEvent(UserRole entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}