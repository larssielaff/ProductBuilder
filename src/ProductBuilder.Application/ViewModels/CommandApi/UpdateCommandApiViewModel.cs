namespace ProductBuilder.Application.ViewModels.CommandApi
{
    using System;

    public class UpdateCommandApiViewModel
    {
        public Guid Id { get; set; }

        public string RouteTemplate { get; set; }

        public string CommandType { get; set; }

        public string CommandName { get; set; }

        public Guid DomainAggregate { get; set; }
    }
}