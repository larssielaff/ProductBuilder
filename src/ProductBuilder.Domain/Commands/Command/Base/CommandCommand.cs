namespace ProductBuilder.Domain.Commands.Command.Base
{
    using Asd.Domain.Core.Commands;
    using System;

    public abstract class CommandCommand : AsdCommand
    {
        public Guid Id { get; protected set; }

        public string CommandName { get; protected set; }

        public string RouteTemplate { get; protected set; }

        public string CommandType { get; protected set; }

        public Guid DomainAggregate { get; protected set; }
    }
}