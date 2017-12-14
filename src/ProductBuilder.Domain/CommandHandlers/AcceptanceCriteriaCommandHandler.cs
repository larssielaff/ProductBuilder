namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.AcceptanceCriteria;
    using ProductBuilder.Domain.Events.AcceptanceCriteria;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;

    public class AcceptanceCriteriaCommandHandler : AsdCommandHandler, 
        IAsdHandler<CreateAcceptanceCriteriaCommand>, 
        IAsdHandler<UpdateAcceptanceCriteriaCommand>, 
        IAsdHandler<DeleteAcceptanceCriteriaCommand>
    {
        private readonly IAcceptanceCriteriaRepository _repository;

        public AcceptanceCriteriaCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IAcceptanceCriteriaRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(CreateAcceptanceCriteriaCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new AcceptanceCriteria(message.Id) { };
            _repository.Add(entity);
            throw new NotImplementedException();
            if (Commit())
                Bus.RaiseEvent(new AcceptanceCriteriaCreatedEvent(entity, message.AggregateId));
        }

        public void Handle(UpdateAcceptanceCriteriaCommand message)
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
                Bus.RaiseEvent(new AcceptanceCriteriaUpdatedEvent(entity, message.AggregateId));
        }

        public void Handle(DeleteAcceptanceCriteriaCommand message)
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
                Bus.RaiseEvent(new AcceptanceCriteriaDeletedEvent(entity, message.AggregateId));
        }
    }
}