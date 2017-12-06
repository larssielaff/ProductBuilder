namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.Product;

    public class ProductEventHandler : IAsdHandler<ProductCreatedEvent>
    {
        public void Handle(ProductCreatedEvent message) { }
    }
}
