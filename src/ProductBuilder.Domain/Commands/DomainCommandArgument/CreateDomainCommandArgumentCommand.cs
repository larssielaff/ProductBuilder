namespace ProductBuilder.Domain.Commands.DomainCommandArgument
{
    using ProductBuilder.Domain.Commands.DomainCommandArgument.Base;
    using ProductBuilder.Domain.Validations.DomainCommandArgument;
    using System;

    public class CreateDomainCommandArgumentCommand : DomainCommandArgumentCommand
    {
        public CreateDomainCommandArgumentCommand(Guid id, Guid aggregateId)
        {
            Id = id; AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DomainCommandArgumentValidator<DomainCommandArgumentCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}