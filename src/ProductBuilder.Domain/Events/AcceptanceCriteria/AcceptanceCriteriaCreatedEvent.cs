namespace ProductBuilder.Domain.Events.AcceptanceCriteria
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class AcceptanceCriteriaCreatedEvent : AsdEvent
    {
        public AcceptanceCriteriaCreatedEvent(AcceptanceCriteria entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}