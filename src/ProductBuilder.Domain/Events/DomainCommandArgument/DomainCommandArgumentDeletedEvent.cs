namespace ProductBuilder.Domain.Events.DomainCommandArgument
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class DomainCommandArgumentDeletedEvent : AsdEvent
    {
        public DomainCommandArgumentDeletedEvent(DomainCommandArgument entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}