namespace ProductBuilder.Application.ViewModels.Query
{
    using System;

    public class QueryViewModel
    {
        public Guid Id { get; set; }

        public string QueryName { get; set; }

        public string RouteTemplate { get; set; }

        public Guid AsdAggregateId { get; set; }
    }
}
