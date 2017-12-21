namespace ProductBuilder.Domain.Commands.AggregateProperty.Base
{
    using Asd.Domain.Core.Commands;
    using System;

    public abstract class AggregatePropertyCommand : AsdCommand
    {
        public Guid Id { get; protected set; }

        public string LinkedAggregateName { get; protected set; }

        public bool IsAggregateRoot { get; protected set; }

        public Guid AsdAggregateId { get; protected set; }

        public string Name { get; protected set; }

        public Guid LinkedAggregateId { get; protected set; }

        public string Type { get; protected set; }
    }
}