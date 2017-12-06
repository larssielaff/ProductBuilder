namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.TeamMember;
    using ProductBuilder.Domain.Events.TeamMember;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;

    public class TeamMemberCommandHandler : AsdCommandHandler, 
        IAsdHandler<CreateTeamMemberCommand>
    {
        private readonly ITeamMemberRepository _repository;

        public TeamMemberCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, ITeamMemberRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(CreateTeamMemberCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new TeamMember(message.Id)
            {
                Role = message.Role,
                UserProfileId = message.UserProfileId,
                TeamId = message.TeamId
            };
            _repository.Add(entity);
            if (Commit())
                Bus.RaiseEvent(new TeamMemberCreatedEvent(entity, message.AggregateId));
        }
    }
}