namespace ProductBuilder.Domain.Events.Query
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class QueryDeletedEvent : AsdEvent
    {
        public QueryDeletedEvent(Query entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}