namespace ProductBuilder.Application.ViewModels.AggregateApi
{
    using System;

    public class CreateAggregateApiViewModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string NamePluralized { get; set; }

        public string Name { get; set; }
    }
}