namespace ProductBuilder.Domain.Validations.Epic
{
    using ProductBuilder.Domain.Commands.Epic.Base;
    using FluentValidation;

    public class EpicValidator<T> : AbstractValidator<T> where T : EpicCommand { }
}