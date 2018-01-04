namespace ProductBuilder.Domain.Commands.Command
{
    using ProductBuilder.Domain.Commands.Command.Base;
    using ProductBuilder.Domain.Validations.Command;
    using System;

    public class DeleteCommandCommand : CommandCommand
    {
        public DeleteCommandCommand(Guid id, Guid aggregateId)
        {
            Id = id;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CommandValidator<CommandCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}