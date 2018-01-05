namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.DomainCommandArgument;

    public class DomainCommandArgumentEventHandler : IAsdHandler<DomainCommandArgumentUpdatedEvent>, 
        IAsdHandler<DomainCommandArgumentCreatedEvent>, 
        IAsdHandler<DomainCommandArgumentDeletedEvent>
    {
        public void Handle(DomainCommandArgumentUpdatedEvent message) { }

        public void Handle(DomainCommandArgumentCreatedEvent message) { }

        public void Handle(DomainCommandArgumentDeletedEvent message) { }
    }
}