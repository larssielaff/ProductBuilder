namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.EventApi;

    public interface IEventAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        void CreateEvent(CreateEventApiViewModel model);

        void UpdateEvent(UpdateEventApiViewModel model);

        void DeleteEvent(DeleteEventApiViewModel model);
    }
}