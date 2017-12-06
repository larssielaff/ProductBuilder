namespace ProductBuilder.Domain.Validations.Team
{
    using ProductBuilder.Domain.Commands.Team.Base;
    using FluentValidation;
    public class TeamValidator<T> : AbstractValidator<T> where T : TeamCommand { }
}