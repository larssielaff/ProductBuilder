namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.Query;

    public class QueryEventHandler : IAsdHandler<QueryDeletedEvent>, 
        IAsdHandler<QueryUpdatedEvent>, 
        IAsdHandler<QueryCreatedEvent>
    {
        public void Handle(QueryDeletedEvent message) { }

        public void Handle(QueryUpdatedEvent message) { }

        public void Handle(QueryCreatedEvent message) { }
    }
}