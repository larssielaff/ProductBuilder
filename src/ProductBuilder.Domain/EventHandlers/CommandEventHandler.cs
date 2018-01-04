namespace ProductBuilder.Domain.EventHandlers
{
    using Asd.Domain.Core.Events;
    using ProductBuilder.Domain.Events.Command;

    public class CommandEventHandler : IAsdHandler<CommandCreatedEvent>, 
        IAsdHandler<CommandDeletedEvent>, 
        IAsdHandler<CommandUpdatedEvent>
    {
        public void Handle(CommandCreatedEvent message) { }

        public void Handle(CommandDeletedEvent message) { }

        public void Handle(CommandUpdatedEvent message) { }
    }
}