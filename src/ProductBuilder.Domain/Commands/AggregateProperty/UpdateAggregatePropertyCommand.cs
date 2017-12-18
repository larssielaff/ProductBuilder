namespace ProductBuilder.Domain.Commands.AggregateProperty
{
    using ProductBuilder.Domain.Commands.AggregateProperty.Base;
    using ProductBuilder.Domain.Validations.AggregateProperty;
    using System;

    public class UpdateAggregatePropertyCommand : AggregatePropertyCommand
    {
        public UpdateAggregatePropertyCommand(Guid id, Guid aggregateId)
        {
            Id = id;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AggregatePropertyValidator<AggregatePropertyCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}