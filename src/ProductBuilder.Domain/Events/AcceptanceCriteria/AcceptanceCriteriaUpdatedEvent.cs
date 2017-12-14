namespace ProductBuilder.Domain.Events.AcceptanceCriteria
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class AcceptanceCriteriaUpdatedEvent : AsdEvent
    {
        public AcceptanceCriteriaUpdatedEvent(AcceptanceCriteria entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}