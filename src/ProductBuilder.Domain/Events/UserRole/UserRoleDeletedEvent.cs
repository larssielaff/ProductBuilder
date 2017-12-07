namespace ProductBuilder.Domain.Events.UserRole
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class UserRoleDeletedEvent : AsdEvent
    {
        public UserRoleDeletedEvent(UserRole entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}