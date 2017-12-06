namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.Product;
    using ProductBuilder.Domain.Events.Product;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;
    public class ProductCommandHandler : 
        AsdCommandHandler, 
        IAsdHandler<CreateProductCommand>
    {
        private readonly IProductRepository _repository;

        public ProductCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IProductRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(CreateProductCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new Product(message.Id)
            {
                Title = message.Title
            };
            _repository.Add(entity);
            if (Commit())
                Bus.RaiseEvent(new ProductCreatedEvent(entity, message.AggregateId));
        }
    }
}