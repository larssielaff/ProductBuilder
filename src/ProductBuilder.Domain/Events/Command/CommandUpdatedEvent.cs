namespace ProductBuilder.Domain.Events.Command
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class CommandUpdatedEvent : AsdEvent
    {
        public CommandUpdatedEvent(Command entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}