namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.Topic;

    public class TopicEventHandler : IAsdHandler<TopicUpdatedEvent>, 
        IAsdHandler<TopicCreatedEvent>, 
        IAsdHandler<TopicDeletedEvent>
    {
        public void Handle(TopicUpdatedEvent message) { }

        public void Handle(TopicCreatedEvent message) { }

        public void Handle(TopicDeletedEvent message) { }
    }
}