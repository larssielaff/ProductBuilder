namespace ProductBuilder.Domain.Commands.Query.Base
{
    using Asd.Domain.Core.Commands;
    using System;

    public abstract class QueryCommand : AsdCommand
    {
        public Guid Id { get; protected set; }

        public Guid ProductId { get; protected set; }

        public string QueryName { get; protected set; }

        public Guid AsdAggregateId { get; protected set; }

        public string RouteTemplate { get; protected set; }
    }
}