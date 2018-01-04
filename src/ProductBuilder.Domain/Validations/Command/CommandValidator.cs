namespace ProductBuilder.Domain.Validations.Command
{
    using ProductBuilder.Domain.Commands.Command.Base;
    using FluentValidation;

    public class CommandValidator<T> : AbstractValidator<T> where T : CommandCommand { }
}