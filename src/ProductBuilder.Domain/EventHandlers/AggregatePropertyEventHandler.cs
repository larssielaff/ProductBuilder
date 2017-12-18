namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.AggregateProperty;

    public class AggregatePropertyEventHandler : IAsdHandler<AggregatePropertyUpdatedEvent>, 
        IAsdHandler<AggregatePropertyDeletedEvent>, 
        IAsdHandler<AggregatePropertyCreatedEvent>
    {
        public void Handle(AggregatePropertyUpdatedEvent message) { }

        public void Handle(AggregatePropertyDeletedEvent message) { }

        public void Handle(AggregatePropertyCreatedEvent message) { }
    }
}