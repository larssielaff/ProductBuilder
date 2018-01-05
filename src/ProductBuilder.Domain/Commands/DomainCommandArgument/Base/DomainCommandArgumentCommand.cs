namespace ProductBuilder.Domain.Commands.DomainCommandArgument.Base
{
    using Asd.Domain.Core.Commands;
    using System;

    public abstract class DomainCommandArgumentCommand : AsdCommand
    {
        public Guid Id { get; protected set; }

        public Guid DomainAggregatePropertyId { get; protected set; }

        public Guid DomainCommandId { get; protected set; }
    }
}