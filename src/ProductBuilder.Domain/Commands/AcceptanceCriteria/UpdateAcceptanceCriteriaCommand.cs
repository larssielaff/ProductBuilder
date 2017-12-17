namespace ProductBuilder.Domain.Commands.AcceptanceCriteria
{
    using ProductBuilder.Domain.Commands.AcceptanceCriteria.Base;
    using ProductBuilder.Domain.Validations.AcceptanceCriteria;
    using System;

    public class UpdateAcceptanceCriteriaCommand : AcceptanceCriteriaCommand
    {
        public UpdateAcceptanceCriteriaCommand(Guid id, string title, Guid aggregateId)
        {
            Id = id;
            Title = title;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AcceptanceCriteriaValidator<AcceptanceCriteriaCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}