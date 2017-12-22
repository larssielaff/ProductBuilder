﻿namespace ProductBuilder.Domain.Commands.Query
{
    using ProductBuilder.Domain.Commands.Query.Base;
    using ProductBuilder.Domain.Validations.Query;
    using System;

    public class UpdateQueryCommand : QueryCommand
    {
        public UpdateQueryCommand(Guid id, Guid aggregateId)
        {
            Id = id;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new QueryValidator<QueryCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}