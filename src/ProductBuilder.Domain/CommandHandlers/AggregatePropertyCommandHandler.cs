namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.AggregateProperty;
    using ProductBuilder.Domain.Events.AggregateProperty;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;

    public class AggregatePropertyCommandHandler : AsdCommandHandler, 
        IAsdHandler<UpdateAggregatePropertyCommand>, 
        IAsdHandler<DeleteAggregatePropertyCommand>, 
        IAsdHandler<CreateAggregatePropertyCommand>
    {
        private readonly IAggregatePropertyRepository _repository;

        public AggregatePropertyCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IAggregatePropertyRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(UpdateAggregatePropertyCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = _repository.GetById(message.Id);
            if (entity == null)
                throw new NullReferenceException(nameof(entity));
            entity.Name = message.Name;
            entity.Type = message.Type;
            entity.LinkedAggregateId = message.LinkedAggregateId;
            entity.LinkedAggregateName = message.LinkedAggregateName;
            entity.IsAggregateRoot = message.IsAggregateRoot;
            if (entity.LinkedAggregateId == Guid.Empty)
                entity.LinkedAggregateId = null;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new AggregatePropertyUpdatedEvent(entity, message.AggregateId));
        }

        public void Handle(DeleteAggregatePropertyCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = _repository.GetById(message.Id);
            if (entity == null)
                throw new NullReferenceException(nameof(entity));
            _repository.Remove(entity);
            if (Commit())
                Bus.RaiseEvent(new AggregatePropertyDeletedEvent(entity, message.AggregateId));
        }

        public void Handle(CreateAggregatePropertyCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new AggregateProperty(message.Id)
            {
                Name = message.Name,
                Type = message.Type,
                AsdAggregateId = message.AsdAggregateId,
                LinkedAggregateId = message.LinkedAggregateId,
                LinkedAggregateName = message.LinkedAggregateName,
                IsAggregateRoot = message.IsAggregateRoot
            };

            if (entity.LinkedAggregateId == Guid.Empty)
                entity.LinkedAggregateId = null;

            _repository.Add(entity);
            if (Commit())
                Bus.RaiseEvent(new AggregatePropertyCreatedEvent(entity, message.AggregateId));
        }
    }
}