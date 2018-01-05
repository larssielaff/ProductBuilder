namespace ProductBuilder.Application.ViewModels.Command
{
    using System;

    public class DomainCommandViewModel
    {
        public Guid Id { get; set; }

        public Guid? DomainAggregateId { get; set; }

        public string CommandName { get; set; }

        public string RouteTemplate { get; set; }

        public string CommandType { get; set; }

        public Guid? DomainEventId { get; set; }
    }
}
