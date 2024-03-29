﻿namespace ProductBuilder.Domain.Commands.Topic
{
    using ProductBuilder.Domain.Commands.Topic.Base;
    using ProductBuilder.Domain.Validations.Topic;
    using System;

    public class DeleteTopicCommand : TopicCommand
    {
        public DeleteTopicCommand(Guid id, Guid aggregateId)
        {
            Id = id;
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