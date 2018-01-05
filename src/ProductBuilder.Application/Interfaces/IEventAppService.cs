namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.EventApi;
    using System.Collections.Generic;

    public interface IEventAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel(Guid aggregateId);

        IEnumerable<DomainAggregateEventsJsonArrayApiViewModel> GetDomainAggregateEventsJsonArrayApiViewModel(Guid aggregateId);

        void CreateEvent(CreateEventApiViewModel model);

        void UpdateEvent(UpdateEventApiViewModel model);

        void DeleteEvent(DeleteEventApiViewModel model);
    }
}