namespace ProductBuilder.Domain.Commands.AcceptanceCriteria
{
    using ProductBuilder.Domain.Commands.AcceptanceCriteria.Base;
    using ProductBuilder.Domain.Validations.AcceptanceCriteria;
    using System;

    public class CreateAcceptanceCriteriaCommand : AcceptanceCriteriaCommand
    {
        public CreateAcceptanceCriteriaCommand(Guid id, Guid aggregateId)
        {
            Id = id;
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