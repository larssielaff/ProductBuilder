namespace ProductBuilder.Domain.Commands.UserStory
{
    using ProductBuilder.Domain.Commands.UserStory.Base;
    using ProductBuilder.Domain.Validations.UserStory;
    using System;

    public class UpdateUserStoryCommand : UserStoryCommand
    {
        public UpdateUserStoryCommand(Guid id, string title, string story, Guid aggregateId)
        {
            Id = id;
            Title = title;
            Story = story;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UserStoryValidator<UserStoryCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}