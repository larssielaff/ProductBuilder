namespace ProductBuilder.Domain.Commands.AggregateProperty
{
    using ProductBuilder.Domain.Commands.AggregateProperty.Base;
    using ProductBuilder.Domain.Validations.AggregateProperty;
    using System;

    public class CreateAggregateCommand : AggregatePropertyCommand
    {
        public CreateAggregateCommand(Guid id, Guid aggregateId)
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