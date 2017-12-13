namespace ProductBuilder.Domain.Commands.UserStory
{
    using ProductBuilder.Domain.Commands.UserStory.Base;
    using ProductBuilder.Domain.Validations.UserStory;
    using System;

    public class AssignTopicCommand : UserStoryCommand
    {
        public AssignTopicCommand(Guid id, Guid topicId, Guid aggregateId)
        {
            Id = id;
            TopicId = topicId;
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