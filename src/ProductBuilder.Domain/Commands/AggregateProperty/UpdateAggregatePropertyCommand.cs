namespace ProductBuilder.Domain.Commands.AggregateProperty
{
    using ProductBuilder.Domain.Commands.AggregateProperty.Base;
    using ProductBuilder.Domain.Validations.AggregateProperty;
    using System;

    public class UpdateAggregatePropertyCommand : AggregatePropertyCommand
    {
        public UpdateAggregatePropertyCommand(Guid id, string name, string type, Guid linkedAggregateId, string linkedAggregateName, bool isAggregateRoot, Guid aggregateId)
        {
            Id = id;
            Name = name;
            Type = type;
            LinkedAggregateId = linkedAggregateId;
            LinkedAggregateName = linkedAggregateName;
            IsAggregateRoot = isAggregateRoot;
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