namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.Epic;

    public class EpicEventHandler : IAsdHandler<EpicCreatedEvent>, 
        IAsdHandler<EpicDeletedEvent>, 
        IAsdHandler<EpicUpdatedEvent>
    {
        public void Handle(EpicCreatedEvent message) { }

        public void Handle(EpicDeletedEvent message) { }

        public void Handle(EpicUpdatedEvent message) { }
    }
}