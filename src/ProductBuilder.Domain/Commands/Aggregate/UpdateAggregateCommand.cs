namespace ProductBuilder.Domain.Commands.Aggregate
{
    using ProductBuilder.Domain.Commands.Aggregate.Base;
    using ProductBuilder.Domain.Validations.Aggregate;
    using System;

    public class UpdateAggregateCommand : AggregateCommand
    {
        public UpdateAggregateCommand(Guid id, string name, string namePluralized, Guid aggregateId)
        {
            Id = id;
            Name = name;
            NamePluralized = namePluralized;
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