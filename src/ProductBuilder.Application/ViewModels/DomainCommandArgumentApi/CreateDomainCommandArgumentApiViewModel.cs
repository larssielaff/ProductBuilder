namespace ProductBuilder.Application.ViewModels.DomainCommandArgumentApi
{
    using System;

    public class CreateDomainCommandArgumentApiViewModel
    {
        public Guid Id { get; set; }

        public Guid DomainAggregatePropertyId { get; set; }

        public Guid DomainCommandId { get; set; }
    }
}