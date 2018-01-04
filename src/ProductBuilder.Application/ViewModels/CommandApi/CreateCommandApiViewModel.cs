namespace ProductBuilder.Application.ViewModels.CommandApi
{
    using System;

    public class CreateCommandApiViewModel
    {
        public Guid Id { get; set; }

        public string RouteTemplate { get; set; }

        public Guid DomainAggregateId { get; set; }

        public Guid DomainEventId { get; set; }

        public string CommandType { get; set; }

        public string CommandName { get; set; }
    }
}