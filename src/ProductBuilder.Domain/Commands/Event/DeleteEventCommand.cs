namespace ProductBuilder.Domain.Commands.Event
{
    using ProductBuilder.Domain.Commands.Event.Base;
    using ProductBuilder.Domain.Validations.Event;
    using System;

    public class DeleteEventCommand : EventCommand
    {
        public DeleteEventCommand(Guid id, Guid aggregateId)
        {
            Id = id;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new EventValidator<EventCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}