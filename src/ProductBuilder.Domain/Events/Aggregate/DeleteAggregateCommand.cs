﻿namespace ProductBuilder.Domain.Commands.Aggregate
{
    using ProductBuilder.Domain.Commands.Aggregate.Base;
    using ProductBuilder.Domain.Validations.Aggregate;
    using System;

    public class DeleteAggregateCommand : AggregateCommand
    {
        public DeleteAggregateCommand(Guid id, Guid aggregateId)
        {
            Id = id;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AggregateValidator<AggregateCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}