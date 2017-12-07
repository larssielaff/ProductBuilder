namespace ProductBuilder.Domain.Commands.Epic.Base
{
    using Asd.Domain.Core.Commands;
    using System;

    public abstract class EpicCommand : AsdCommand
    {
        public Guid Id { get; protected set; }

        public Guid ProductId { get; protected set; }

        public string Title { get; protected set; }

        public string Description { get; protected set; }
    }
}