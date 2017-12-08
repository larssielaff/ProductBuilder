namespace ProductBuilder.Domain.Validations.UserStory
{
    using ProductBuilder.Domain.Commands.UserStory.Base;
    using FluentValidation;

    public class UserStoryValidator<T> : AbstractValidator<T> where T : UserStoryCommand { }
}