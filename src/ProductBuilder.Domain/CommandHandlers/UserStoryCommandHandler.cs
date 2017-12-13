namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.UserStory;
    using ProductBuilder.Domain.Events.UserStory;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;

    public class UserStoryCommandHandler : AsdCommandHandler, 
        IAsdHandler<AssignTopicCommand>, 
        IAsdHandler<DeleteUserStoryCommand>, 
        IAsdHandler<CreateUserStoryCommand>, 
        IAsdHandler<RemoveTopicCommand>, 
        IAsdHandler<AssignUserRoleCommand>, 
        IAsdHandler<UpdateStoryPointsCommand>, 
        IAsdHandler<UpdateUserStoryCommand>, 
        IAsdHandler<AssignEpicCommand>, 
        IAsdHandler<UpdateValueCommand>, 
        IAsdHandler<RemoveUserRoleCommand>, 
        IAsdHandler<RemoveEpicCommand>
    {
        private readonly IUserStoryRepository _repository;
        public UserStoryCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IUserStoryRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(AssignTopicCommand message)
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
                throw new NullReferenceException(nameof(entity)); _repository.Update(entity);
            entity.TopicId = message.TopicId;
            if (Commit())
                Bus.RaiseEvent(new TopicAssignedEvent(entity, message.AggregateId));
        }

        public void Handle(DeleteUserStoryCommand message)
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
                Bus.RaiseEvent(new UserStoryDeletedEvent(entity, message.AggregateId));
        }

        public void Handle(CreateUserStoryCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new UserStory(message.Id)
            {
                Title = message.Title,
                Story = message.Story,
                StoryPoints = 21,
                ProductId = message.ProductId
            };
            _repository.Add(entity);
            if (Commit())
                Bus.RaiseEvent(new UserStoryCreatedEvent(entity, message.AggregateId));
        }

        public void Handle(RemoveTopicCommand message)
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
            entity.TopicId = null;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new TopicRemovedEvent(entity, message.AggregateId));
        }

        public void Handle(AssignUserRoleCommand message)
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
            entity.UserRoleId = message.UserRoleId;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new UserRoleAssignedEvent(entity, message.AggregateId));
        }

        public void Handle(UpdateStoryPointsCommand message)
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
            entity.StoryPoints = message.StoryPoints;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new StoryPointsUpdatedEvent(entity, message.AggregateId));
        }

        public void Handle(UpdateUserStoryCommand message)
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
            entity.Story = message.Story;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new UserStoryUpdatedEvent(entity, message.AggregateId));
        }

        public void Handle(AssignEpicCommand message)
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
            entity.EpicId = message.EpicId;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new EpicAssignedEvent(entity, message.AggregateId));
        }

        public void Handle(UpdateValueCommand message)
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
            entity.Value = message.Value;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new ValueUpdatedEvent(entity, message.AggregateId));
        }

        public void Handle(RemoveUserRoleCommand message)
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
            entity.UserRoleId = null;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new UserRoleRemovedEvent(entity, message.AggregateId));
        }

        public void Handle(RemoveEpicCommand message)
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
            entity.EpicId = null;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new EpicRemovedEvent(entity, message.AggregateId));
        }
    }
}