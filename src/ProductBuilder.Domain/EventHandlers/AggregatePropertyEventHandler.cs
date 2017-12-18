namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.AggregateProperty;

    public class AggregatePropertyEventHandler : IAsdHandler<AggregateUpdatedEvent>, 
        IAsdHandler<AggregateDeletedEvent>, 
        IAsdHandler<AggregateCreatedEvent>
    {
        public void Handle(AggregateUpdatedEvent message) { }

        public void Handle(AggregateDeletedEvent message) { }

        public void Handle(AggregateCreatedEvent message) { }
    }
}