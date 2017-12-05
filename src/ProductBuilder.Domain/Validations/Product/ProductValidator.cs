namespace ProductBuilder.Domain.Validations.Product
{
    using ProductBuilder.Domain.Commands.Product.Base;
    using FluentValidation;
    public class ProductValidator<T> : AbstractValidator<T> where T : ProductCommand { }
}
