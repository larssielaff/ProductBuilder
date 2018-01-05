namespace ProductBuilder.Domain.Validations.DomainCommandArgument
{
    using ProductBuilder.Domain.Commands.DomainCommandArgument.Base;
    using FluentValidation;

    public class DomainCommandArgumentValidator<T> : AbstractValidator<T> where T : DomainCommandArgumentCommand { }
}