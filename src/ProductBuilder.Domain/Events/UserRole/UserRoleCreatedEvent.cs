namespace ProductBuilder.Domain.Events.UserRole
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class UserRoleCreatedEvent : AsdEvent
    {
        public UserRoleCreatedEvent(UserRole entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}