namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.Team;
    using ProductBuilder.Domain.Events.Team;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;
    public class TeamCommandHandler : AsdCommandHandler, 
        IAsdHandler<CreateTeamCommand>
    {
        private readonly ITeamRepository _repository;

        public TeamCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, ITeamRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(CreateTeamCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new Team(message.Id)
            {
                Title = message.Title,
                ProductId = message.ProductId
            };
            _repository.Add(entity);
            if (Commit())
                Bus.RaiseEvent(new TeamCreatedEvent(entity, message.AggregateId));
        }
    }
}