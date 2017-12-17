namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.Aggregate;

    public class AggregateEventHandler : IAsdHandler<AggregateDeletedEvent>, 
        IAsdHandler<AggregateCreatedEvent>, 
        IAsdHandler<AggregateUpdatedEvent>
    {
        public void Handle(AggregateDeletedEvent message) { }

        public void Handle(AggregateCreatedEvent message) { }

        public void Handle(AggregateUpdatedEvent message) { }
    }
}