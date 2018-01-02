namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.Event;

    public class EventEventHandler : IAsdHandler<EventCreatedEvent>, 
        IAsdHandler<EventDeletedEvent>, 
        IAsdHandler<EventUpdatedEvent>
    {
        public void Handle(EventCreatedEvent message) { }

        public void Handle(EventDeletedEvent message) { }

        public void Handle(EventUpdatedEvent message) { }
    }
}