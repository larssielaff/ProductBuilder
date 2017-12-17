namespace ProductBuilder.Domain.Validations.Aggregate
{
    using ProductBuilder.Domain.Commands.Aggregate.Base;
    using FluentValidation;

    public class AggregateValidator<T> : AbstractValidator<T> where T : AggregateCommand { }
}