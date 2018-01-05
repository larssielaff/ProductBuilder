namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.Command;
    using ProductBuilder.Domain.Events.Command;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;

    public class CommandCommandHandler : AsdCommandHandler, 
        IAsdHandler<UpdateCommandCommand>, 
        IAsdHandler<DeleteCommandCommand>, 
        IAsdHandler<CreateCommandCommand>
    {
        private readonly ICommandRepository _repository;

        public CommandCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, ICommandRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(UpdateCommandCommand message)
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
            entity.CommandName = message.CommandName;
            entity.RouteTemplate = message.RouteTemplate;
            entity.CommandType = message.CommandType;
            entity.DomainEventId = message.DomainEventId;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new CommandUpdatedEvent(entity, message.AggregateId));
        }

        public void Handle(DeleteCommandCommand message)
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
                Bus.RaiseEvent(new CommandDeletedEvent(entity, message.AggregateId));
        }

        public void Handle(CreateCommandCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new Command(message.Id)
            {
                CommandName = message.CommandName,
                RouteTemplate = message.RouteTemplate,
                CommandType = message.CommandType,
                DomainEventId = message.DomainEventId,
                DomainAggregateId = message.DomainAggregateId
            };
            _repository.Add(entity);
            if (Commit())
                Bus.RaiseEvent(new CommandCreatedEvent(entity, message.AggregateId));
        }
    }
}