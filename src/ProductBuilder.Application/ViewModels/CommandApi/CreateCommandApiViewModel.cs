namespace ProductBuilder.Application.ViewModels.CommandApi
{
    using System;

    public class CreateCommandApiViewModel
    {
        public Guid Id { get; set; }

        public Guid DomainAggregate { get; set; }

        public string CommandName { get; set; }

        public string CommandType { get; set; }

        public string RouteTemplate { get; set; }
    }
}