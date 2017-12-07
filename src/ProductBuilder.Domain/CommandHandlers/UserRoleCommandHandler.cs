namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.UserRole;
    using ProductBuilder.Domain.Events.UserRole;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;

    public class UserRoleCommandHandler : AsdCommandHandler, 
        IAsdHandler<CreateUserRoleCommand>, 
        IAsdHandler<DeleteUserRoleCommand>, 
        IAsdHandler<UpdateUserRoleCommand>
    {
        private readonly IUserRoleRepository _repository;

        public UserRoleCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IUserRoleRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(CreateUserRoleCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new UserRole(message.Id)
            {
                Role = message.Role,
                ProductId = message.ProductId
            };
            _repository.Add(entity);
            if (Commit())
                Bus.RaiseEvent(new UserRoleCreatedEvent(entity, message.AggregateId));
        }

        public void Handle(DeleteUserRoleCommand message)
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
                Bus.RaiseEvent(new UserRoleDeletedEvent(entity, message.AggregateId));
        }

        public void Handle(UpdateUserRoleCommand message)
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
            entity.Role = message.Role;
            _repository.Update(entity);
            if (Commit())
                Bus.RaiseEvent(new UserRoleUpdatedEvent(entity, message.AggregateId));
        }
    }
}