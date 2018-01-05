namespace ProductBuilder.Application.ViewModels.DomainCommandArgumentApi
{
    using System;

    public class UpdateDomainCommandArgumentApiViewModel
    {
        public Guid Id { get; set; }

        public Guid DomainCommandId { get; set; }

        public Guid DomainAggregatePropertyId { get; set; }
    }
}