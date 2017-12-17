namespace ProductBuilder.Domain.Commands.Aggregate.Base
{
    using Asd.Domain.Core.Commands;
    using System;

    public abstract class AggregateCommand : AsdCommand
    {
        public Guid Id { get; protected set; }

        public Guid ProductId { get; protected set; }

        public string NamePluralized { get; protected set; }

        public string Name { get; protected set; }
    }
}