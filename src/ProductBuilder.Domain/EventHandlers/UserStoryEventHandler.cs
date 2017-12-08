namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.UserStory;
    public class UserStoryEventHandler : IAsdHandler<StoryPointsUpdatedEvent>, 
        IAsdHandler<UserStoryCreatedEvent>, 
        IAsdHandler<UserRoleRemovedEvent>, 
        IAsdHandler<ValueUpdatedEvent>, 
        IAsdHandler<UserStoryUpdatedEvent>, 
        IAsdHandler<EpicRemovedEvent>, 
        IAsdHandler<UserStoryDeletedEvent>, 
        IAsdHandler<TopicAssignedEvent>, 
        IAsdHandler<UserRoleAssignedEvent>, 
        IAsdHandler<EpicAssignedEvent>, 
        IAsdHandler<TopicRemovedEvent>
    {
        public void Handle(StoryPointsUpdatedEvent message) { }

        public void Handle(UserStoryCreatedEvent message) { }

        public void Handle(UserRoleRemovedEvent message) { }

        public void Handle(ValueUpdatedEvent message) { }

        public void Handle(UserStoryUpdatedEvent message) { }

        public void Handle(EpicRemovedEvent message) { }

        public void Handle(UserStoryDeletedEvent message) { }

        public void Handle(TopicAssignedEvent message) { }

        public void Handle(UserRoleAssignedEvent message) { }

        public void Handle(EpicAssignedEvent message) { }

        public void Handle(TopicRemovedEvent message) { }
    }
}