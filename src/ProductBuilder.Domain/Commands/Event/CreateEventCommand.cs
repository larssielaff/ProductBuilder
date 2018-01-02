namespace ProductBuilder.Domain.Commands.Event
{
    using ProductBuilder.Domain.Commands.Event.Base;
    using ProductBuilder.Domain.Validations.Event;
    using System;

    public class CreateEventCommand : EventCommand
    {
        public CreateEventCommand(Guid id, string eventName, Guid asdAggregate, Guid aggregateId)
        {
            Id = id;
            EventName = eventName;
            AsdAggregateId = asdAggregate;
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