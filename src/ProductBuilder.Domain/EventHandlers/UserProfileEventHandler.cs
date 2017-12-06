namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.UserProfile;
    public class UserProfileEventHandler : IAsdHandler<UserProfileCreatedEvent>
    {
        public void Handle(UserProfileCreatedEvent message) { }
    }
}