namespace ProductBuilder.Domain.Commands.Epic
{
    using ProductBuilder.Domain.Commands.Epic.Base;
    using ProductBuilder.Domain.Validations.Epic;
    using System;

    public class CreateEpicCommand : EpicCommand
    {
        public CreateEpicCommand(Guid id, string title, string description, Guid productId, Guid aggregateId)
        {
            Id = id;
            Title = title;
            Description = description;
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