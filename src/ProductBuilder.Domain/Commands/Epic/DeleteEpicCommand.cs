﻿namespace ProductBuilder.Domain.Commands.Epic
{
    using ProductBuilder.Domain.Commands.Epic.Base;
    using ProductBuilder.Domain.Validations.Epic;
    using System;

    public class DeleteEpicCommand : EpicCommand
    {
        public DeleteEpicCommand(Guid id, Guid aggregateId)
        {
            Id = id;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new EpicValidator<EpicCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}