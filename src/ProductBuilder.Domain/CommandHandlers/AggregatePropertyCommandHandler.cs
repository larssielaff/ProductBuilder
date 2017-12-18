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
        IAsdHandler<UpdateAggregateCommand>, 
        IAsdHandler<DeleteAggregateCommand>, 
        IAsdHandler<CreateAggregateCommand>
    {
        private readonly IAggregatePropertyRepository _repository;

        public AggregatePropertyCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IAggregatePropertyRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(UpdateAggregateCommand message)
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
            _repository.Update(entity);
            throw new NotImplementedException();
            if (Commit())
                Bus.RaiseEvent(new AggregateUpdatedEvent(entity, message.AggregateId));
        }

        public void Handle(DeleteAggregateCommand message)
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
            throw new NotImplementedException();
            if (Commit())
                Bus.RaiseEvent(new AggregateDeletedEvent(entity, message.AggregateId));
        }

        public void Handle(CreateAggregateCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new AggregateProperty(message.Id) { };
            _repository.Add(entity);
            throw new NotImplementedException();
            if (Commit())
                Bus.RaiseEvent(new AggregateCreatedEvent(entity, message.AggregateId));
        }
    }
}