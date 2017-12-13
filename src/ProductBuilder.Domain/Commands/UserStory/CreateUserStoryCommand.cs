namespace ProductBuilder.Domain.Commands.UserStory
{
    using ProductBuilder.Domain.Commands.UserStory.Base;
    using ProductBuilder.Domain.Validations.UserStory;
    using System;

    public class CreateUserStoryCommand : UserStoryCommand
    {
        public CreateUserStoryCommand(Guid id, string title, string story, Guid productId, Guid aggregateId)
        {
            Id = id;
            Title = title;
            Story = story;
            ProductId = productId;
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