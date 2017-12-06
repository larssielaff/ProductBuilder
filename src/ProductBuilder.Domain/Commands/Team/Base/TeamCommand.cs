namespace ProductBuilder.Domain.Commands.Team.Base
{
    using Asd.Domain.Core.Commands;
    using System;
    public abstract class TeamCommand : AsdCommand
    {
        public Guid Id { get; protected set; }
        public string Title { get; protected set; }
        public Guid ProductId { get; protected set; }
    }
}