namespace ProductBuilder.Domain.Validations.Topic
{
    using ProductBuilder.Domain.Commands.Topic.Base;
    using FluentValidation;

    public class TopicValidator<T> : AbstractValidator<T> where T : TopicCommand { }
}