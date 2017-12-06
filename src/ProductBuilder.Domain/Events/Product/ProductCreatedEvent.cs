namespace ProductBuilder.Domain.Events.Product
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Models;
    using System;

    public class ProductCreatedEvent : AsdEvent
    {
        public ProductCreatedEvent(Product entity, Guid aggregateId) 
            : base(entity, aggregateId) { }
    }
}