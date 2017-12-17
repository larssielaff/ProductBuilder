namespace ProductBuilder.Application.ViewModels.AggregateApi
{
    using System;

    public class DeleteAggregateApiViewModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
    }
}