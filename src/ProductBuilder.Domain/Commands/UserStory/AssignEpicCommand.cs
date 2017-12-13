namespace ProductBuilder.Domain.Commands.UserStory
{
    using ProductBuilder.Domain.Commands.UserStory.Base;
    using ProductBuilder.Domain.Validations.UserStory;
    using System;

    public class AssignEpicCommand : UserStoryCommand
    {
        public AssignEpicCommand(Guid id, Guid epicId, Guid aggregateId)
        {
            Id = id;
            EpicId = epicId;
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