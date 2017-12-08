namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.Topic;
    using ProductBuilder.Domain.Events.Topic;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;

    public class TopicCommandHandler : AsdCommandHandler, 
        IAsdHandler<DeleteTopicCommand>, 
        IAsdHandler<CreateTopicCommand>, 
        IAsdHandler<UpdateTopicCommand>
    {
        private readonly ITopicRepository _repository;

        public TopicCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, ITopicRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(DeleteTopicCommand message)
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
                Bus.RaiseEvent(new TopicDeletedEvent(entity, message.AggregateId));
        }

        public void Handle(CreateTopicCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new Topic(message.Id)
            {
                Title = message.Title,
                Description = message.Description,
                ProductId = message.ProductId
            };
            _repository.Add(entity);
            if (Commit())
                Bus.RaiseEvent(new TopicCreatedEvent(entity, message.AggregateId));
        }

        public void Handle(UpdateTopicCommand message)
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
                Bus.RaiseEvent(new TopicUpdatedEvent(entity, message.AggregateId));
        }
    }
}