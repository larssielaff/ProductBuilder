namespace ProductBuilder.Domain.Validations.Query
{
    using ProductBuilder.Domain.Commands.Query.Base;
    using FluentValidation;

    public class QueryValidator<T> : AbstractValidator<T> where T : QueryCommand { }
}