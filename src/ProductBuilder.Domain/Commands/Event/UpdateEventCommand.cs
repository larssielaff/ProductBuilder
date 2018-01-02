namespace ProductBuilder.Domain.Commands.Event
{
    using ProductBuilder.Domain.Commands.Event.Base;
    using ProductBuilder.Domain.Validations.Event;
    using System;

    public class UpdateEventCommand : EventCommand
    {
        public UpdateEventCommand(Guid id, string eventName, Guid aggregateId)
        {
            Id = id;
            EventName = eventName;
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