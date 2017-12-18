namespace ProductBuilder.Application.ViewModels.AggregatePropertyApi
{
    using System;

    public class UpdateAggregateApiViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsAggregateRoot { get; set; }

        public Guid LinkedAggregateId { get; set; }

        public Guid AsdAggregateId { get; set; }

        public string Type { get; set; }
    }
}