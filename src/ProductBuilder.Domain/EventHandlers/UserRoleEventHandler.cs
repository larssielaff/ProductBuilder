namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.UserRole;

    public class UserRoleEventHandler : IAsdHandler<UserRoleUpdatedEvent>, 
        IAsdHandler<UserRoleCreatedEvent>, 
        IAsdHandler<UserRoleDeletedEvent>
    {
        public void Handle(UserRoleUpdatedEvent message) { }

        public void Handle(UserRoleCreatedEvent message) { }

        public void Handle(UserRoleDeletedEvent message) { }
    }
}