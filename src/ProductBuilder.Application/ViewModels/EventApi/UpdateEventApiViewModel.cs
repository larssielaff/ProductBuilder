namespace ProductBuilder.Application.ViewModels.EventApi
{
    using System;

    public class UpdateEventApiViewModel
    {
        public Guid Id { get; set; }

        public Guid AsdAggregateId { get; set; }

        public string EventName { get; set; }
    }
}