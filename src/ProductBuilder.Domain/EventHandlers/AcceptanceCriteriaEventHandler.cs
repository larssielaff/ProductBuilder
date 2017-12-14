namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.AcceptanceCriteria;

    public class AcceptanceCriteriaEventHandler : IAsdHandler<AcceptanceCriteriaUpdatedEvent>, 
        IAsdHandler<AcceptanceCriteriaCreatedEvent>, 
        IAsdHandler<AcceptanceCriteriaDeletedEvent>
    {
        public void Handle(AcceptanceCriteriaUpdatedEvent message) { }

        public void Handle(AcceptanceCriteriaCreatedEvent message) { }

        public void Handle(AcceptanceCriteriaDeletedEvent message) { }
    }
}