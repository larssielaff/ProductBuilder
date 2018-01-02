namespace ProductBuilder.Application.ViewModels.EventApi
{
    using System;

    public class CreateEventApiViewModel
    {
        public Guid Id { get; set; }

        public string EventName { get; set; }

        public Guid AsdAggregateId { get; set; }
    }
}