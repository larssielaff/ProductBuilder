namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.Epic;
    using ProductBuilder.Domain.Events.Epic;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;

    public class EpicCommandHandler : AsdCommandHandler, IAsdHandler<DeleteEpicCommand>, 
        IAsdHandler<CreateEpicCommand>, 
        IAsdHandler<UpdateEpicCommand>
    {
        private readonly IEpicRepository _repository;

        public EpicCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IEpicRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(DeleteEpicCommand message)
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
                Bus.RaiseEvent(new EpicDeletedEvent(entity, message.AggregateId));
        }

        public void Handle(CreateEpicCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new Epic(message.Id)
            {
                Title = message.Title,
                Description = message.Description,
                ProductId = message.ProductId
            };
            _repository.Add(entity);
            if (Commit())
                Bus.RaiseEvent(new EpicCreatedEvent(entity, message.AggregateId));
        }

        public void Handle(UpdateEpicCommand message)
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
            entity.Title = message.Title;
            entity.Description = message.Description;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new EpicUpdatedEvent(entity, message.AggregateId));
        }
    }
}