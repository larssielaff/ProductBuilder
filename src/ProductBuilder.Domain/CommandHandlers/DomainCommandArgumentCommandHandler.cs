namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.DomainCommandArgument;
    using ProductBuilder.Domain.Events.DomainCommandArgument;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;

    public class DomainCommandArgumentCommandHandler : AsdCommandHandler, 
        IAsdHandler<UpdateDomainCommandArgumentCommand>, 
        IAsdHandler<CreateDomainCommandArgumentCommand>, 
        IAsdHandler<DeleteDomainCommandArgumentCommand>
    {
        private readonly IDomainCommandArgumentRepository _repository;

        public DomainCommandArgumentCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IDomainCommandArgumentRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(UpdateDomainCommandArgumentCommand message)
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
            entity.DomainAggregatePropertyId = message.DomainAggregatePropertyId;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new DomainCommandArgumentUpdatedEvent(entity, message.AggregateId));
        }

        public void Handle(CreateDomainCommandArgumentCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new DomainCommandArgument(message.Id)
            {
                DomainAggregatePropertyId = message.DomainAggregatePropertyId,
                DomainCommandId = message.DomainCommandId
            };
            _repository.Add(entity);
            if (Commit())
                Bus.RaiseEvent(new DomainCommandArgumentCreatedEvent(entity, message.AggregateId));
        }

        public void Handle(DeleteDomainCommandArgumentCommand message)
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
                Bus.RaiseEvent(new DomainCommandArgumentDeletedEvent(entity, message.AggregateId));
        }
    }
}