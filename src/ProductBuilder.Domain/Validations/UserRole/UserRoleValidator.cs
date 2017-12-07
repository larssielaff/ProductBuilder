namespace ProductBuilder.Domain.Validations.UserRole
{
    using ProductBuilder.Domain.Commands.UserRole.Base;
    using FluentValidation;

    public class UserRoleValidator<T> : AbstractValidator<T> where T : UserRoleCommand { }
}