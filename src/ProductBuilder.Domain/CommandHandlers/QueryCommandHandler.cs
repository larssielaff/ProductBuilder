namespace ProductBuilder.Domain.CommandHandlers
{
    using Asd.Domain.CommandHandler;
    using Asd.Domain.Core.Bus;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Notifications;
    using Asd.Domain.Interfaces;
    using ProductBuilder.Domain.Commands.Query;
    using ProductBuilder.Domain.Events.Query;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    using System;

    public class QueryCommandHandler : AsdCommandHandler, 
        IAsdHandler<CreateQueryCommand>, 
        IAsdHandler<UpdateQueryCommand>, 
        IAsdHandler<DeleteQueryCommand>
    {
        private readonly IQueryRepository _repository;

        public QueryCommandHandler(IAsdUnitOfWork unitOfWork, IAsdBus bus, IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IQueryRepository repository) 
            : base(unitOfWork, bus, notifications)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(CreateQueryCommand message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return;
            }
            var entity = new Query(message.Id) { };
            _repository.Add(entity);
            throw new NotImplementedException();
            if (Commit())
                Bus.RaiseEvent(new QueryCreatedEvent(entity, message.AggregateId));
        }

        public void Handle(UpdateQueryCommand message)
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
                Bus.RaiseEvent(new QueryUpdatedEvent(entity, message.AggregateId));
        }

        public void Handle(DeleteQueryCommand message)
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
                Bus.RaiseEvent(new QueryDeletedEvent(entity, message.AggregateId));
        }
    }
}