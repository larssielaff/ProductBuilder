namespace ProductBuilder.Application.ViewModels.AggregatePropertyApi
{
    using System;

    public class UpdateAggregatePropertyApiViewModel
    {
        public Guid Id { get; set; }

        public bool IsAggregateRoot { get; set; }

        public string LinkedAggregateName { get; set; }

        public Guid AsdAggregateId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public Guid LinkedAggregateId { get; set; }
    }
}