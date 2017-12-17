namespace ProductBuilder.Domain.Commands.AcceptanceCriteria
{
    using ProductBuilder.Domain.Commands.AcceptanceCriteria.Base;
    using ProductBuilder.Domain.Validations.AcceptanceCriteria;
    using System;

    public class CreateAcceptanceCriteriaCommand : AcceptanceCriteriaCommand
    {
        public CreateAcceptanceCriteriaCommand(Guid id, string title, Guid userStoryId, Guid aggregateId)
        {
            Id = id;
            Title = title;
            UserStoryId = userStoryId;
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