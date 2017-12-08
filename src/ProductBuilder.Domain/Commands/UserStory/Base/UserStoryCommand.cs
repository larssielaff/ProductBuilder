namespace ProductBuilder.Domain.Commands.UserStory.Base
{
    using Asd.Domain.Core.Commands;
    using System;

    public abstract class UserStoryCommand : AsdCommand
    {
        public Guid Id { get; protected set; }

        public Guid UserRoleId { get; protected set; }

        public Guid ProductId { get; protected set; }

        public string Story { get; protected set; }

        public Guid TopicId { get; protected set; }

        public Guid EpicId { get; protected set; }

        public int Value { get; protected set; }

        public int StoryPoints { get; protected set; }

        public string Title { get; protected set; }
    }
}