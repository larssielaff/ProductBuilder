namespace ProductBuilder.Domain.Commands.Command
{
    using ProductBuilder.Domain.Commands.Command.Base;
    using ProductBuilder.Domain.Validations.Command;
    using System;

    public class UpdateCommandCommand : CommandCommand
    {
        public UpdateCommandCommand(Guid id, string commandName, string routeTemplate, string commandType, Guid domainEventId, Guid aggregateId)
        {
            Id = id;
            CommandName = commandName;
            RouteTemplate = routeTemplate;
            CommandType = commandType;
            DomainEventId = domainEventId;
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