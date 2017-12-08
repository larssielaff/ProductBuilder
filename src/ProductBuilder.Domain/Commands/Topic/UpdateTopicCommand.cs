namespace ProductBuilder.Domain.Commands.Topic
{
    using ProductBuilder.Domain.Commands.Topic.Base;
    using ProductBuilder.Domain.Validations.Topic;
    using System;

    public class UpdateTopicCommand : TopicCommand
    {
        public UpdateTopicCommand(Guid id, string title, string description, Guid aggregateId)
        {
            Id = id;
            Title = title;
            Description = description;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new TopicValidator<TopicCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}