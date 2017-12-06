namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.UserProfile;
    using ProductBuilder.Domain.Events.UserProfile;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;
    public class UserProfileCommandHandler : AsdCommandHandler, IAsdHandler<CreateUserProfileCommand>
    {
        private readonly IUserProfileRepository _repository;
        public UserProfileCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IUserProfileRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public void Handle(CreateUserProfileCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new UserProfile(message.Id)
            {
            };
            _repository.Add(entity);
            throw new NotImplementedException();
            if (Commit())
                Bus.RaiseEvent(new UserProfileCreatedEvent(entity, message.AggregateId));
        }
    }
}