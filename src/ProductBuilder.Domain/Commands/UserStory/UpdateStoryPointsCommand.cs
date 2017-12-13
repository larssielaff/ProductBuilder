namespace ProductBuilder.Domain.Commands.UserStory
{
    using ProductBuilder.Domain.Commands.UserStory.Base;
    using ProductBuilder.Domain.Validations.UserStory;
    using System;

    public class UpdateStoryPointsCommand : UserStoryCommand
    {
        public UpdateStoryPointsCommand(Guid id, int storyPoints, Guid aggregateId)
        {
            Id = id;
            StoryPoints = storyPoints;
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