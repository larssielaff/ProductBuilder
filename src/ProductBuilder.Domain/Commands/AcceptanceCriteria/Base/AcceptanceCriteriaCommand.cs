namespace ProductBuilder.Domain.Commands.AcceptanceCriteria.Base
{
    using Asd.Domain.Core.Commands;
    using System;

    public abstract class AcceptanceCriteriaCommand : AsdCommand
    {
        public Guid Id { get; protected set; }

        public Guid UserStoryId { get; protected set; }

        public string Title { get; protected set; }
    }
}