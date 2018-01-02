namespace ProductBuilder.Domain.Commands.Event.Base
{
    using Asd.Domain.Core.Commands;
    using System;

    public abstract class EventCommand : AsdCommand
    {
        public Guid Id { get; protected set; }

        public string EventName { get; protected set; }

        public Guid AsdAggregateId { get; protected set; }
    }
}