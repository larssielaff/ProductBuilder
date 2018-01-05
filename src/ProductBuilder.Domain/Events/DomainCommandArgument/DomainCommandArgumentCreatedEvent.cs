namespace ProductBuilder.Domain.Events.DomainCommandArgument
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class DomainCommandArgumentCreatedEvent : AsdEvent
    {
        public DomainCommandArgumentCreatedEvent(DomainCommandArgument entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}