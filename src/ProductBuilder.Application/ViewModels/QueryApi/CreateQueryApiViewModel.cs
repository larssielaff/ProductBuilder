namespace ProductBuilder.Application.ViewModels.QueryApi
{
    using System;

    public class CreateQueryApiViewModel
    {
        public Guid Id { get; set; }

        public string RouteTemplate { get; set; }

        public Guid ProductId { get; set; }

        public string QueryName { get; set; }

        public Guid AsdAggregateId { get; set; }
    }
}