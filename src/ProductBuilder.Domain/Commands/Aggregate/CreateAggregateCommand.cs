namespace ProductBuilder.Domain.Commands.Aggregate
{
    using ProductBuilder.Domain.Commands.Aggregate.Base;
    using ProductBuilder.Domain.Validations.Aggregate;
    using System;

    public class CreateAggregateCommand : AggregateCommand
    {
        public CreateAggregateCommand(Guid id, string name, string namePluralized, Guid productId, Guid aggregateId)
        {
            Id = id;
            Name = name;
            NamePluralized = namePluralized;
            ProductId = productId;
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