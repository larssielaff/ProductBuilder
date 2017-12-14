namespace ProductBuilder.Domain.Validations.AcceptanceCriteria
{
    using ProductBuilder.Domain.Commands.AcceptanceCriteria.Base;
    using FluentValidation;

    public class AcceptanceCriteriaValidator<T> : AbstractValidator<T> where T : AcceptanceCriteriaCommand { }
}