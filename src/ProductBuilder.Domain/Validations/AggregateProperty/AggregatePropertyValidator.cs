namespace ProductBuilder.Domain.Validations.AggregateProperty
{
    using ProductBuilder.Domain.Commands.AggregateProperty.Base;
    using FluentValidation;

    public class AggregatePropertyValidator<T> : AbstractValidator<T> where T : AggregatePropertyCommand { }
}