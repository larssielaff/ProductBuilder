namespace ProductBuilder.Domain.Events.Command
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class CommandDeletedEvent : AsdEvent
    {
        public CommandDeletedEvent(Command entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}