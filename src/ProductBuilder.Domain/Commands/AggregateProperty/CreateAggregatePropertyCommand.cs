namespace ProductBuilder.Domain.Commands.AggregateProperty
{
    using ProductBuilder.Domain.Commands.AggregateProperty.Base;
    using ProductBuilder.Domain.Validations.AggregateProperty;
    using System;

    public class CreateAggregatePropertyCommand : AggregatePropertyCommand
    {
        public CreateAggregatePropertyCommand(Guid id, string name, string type, Guid asdAggregateId, Guid linkedAggregateId, string linkedAggregateName, bool isAggregateRoot, Guid aggregateId)
        {
            Id = id;
            Name = name;
            Type = type;
            AsdAggregateId = asdAggregateId;
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