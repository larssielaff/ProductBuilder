namespace ProductBuilder.Domain.Events.Command
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class CommandCreatedEvent : AsdEvent
    {
        public CommandCreatedEvent(Command entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}