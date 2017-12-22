namespace ProductBuilder.Domain.Events.Query
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;

    using System; public class QueryCreatedEvent : AsdEvent
    {
        public QueryCreatedEvent(Query entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}