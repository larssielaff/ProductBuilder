namespace ProductBuilder.Application.ViewModels.CommandApi
{
    using System;

    public class DeleteCommandApiViewModel
    {
        public Guid Id { get; set; }

        public Guid DomainAggregate { get; set; }
    }
}