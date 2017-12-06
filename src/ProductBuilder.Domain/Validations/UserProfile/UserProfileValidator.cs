namespace ProductBuilder.Domain.Validations.UserProfile
{
    using ProductBuilder.Domain.Commands.UserProfile.Base;
    using FluentValidation;
    public class UserProfileValidator<T> : AbstractValidator<T> where T : UserProfileCommand { }
}
