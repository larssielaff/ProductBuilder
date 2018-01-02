namespace ProductBuilder.Domain.Validations.Event
{
    using ProductBuilder.Domain.Commands.Event.Base;
    using FluentValidation;

    public class EventValidator<T> : AbstractValidator<T> where T : EventCommand { }
}