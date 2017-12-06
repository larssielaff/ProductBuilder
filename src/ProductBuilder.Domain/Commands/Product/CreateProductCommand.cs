namespace ProductBuilder.Domain.Commands.Product
{
    using ProductBuilder.Domain.Commands.Product.Base;
    using ProductBuilder.Domain.Validations.Product;
    using System;
    public class CreateProductCommand : ProductCommand
    {
        public CreateProductCommand(Guid id, string title, Guid aggregateId)
        {
            Id = id;
            Title = title;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new ProductValidator<ProductCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}