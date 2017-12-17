namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.AggregateApi;

    public interface IAggregateAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        void DeleteAggregate(DeleteAggregateApiViewModel model);

        void CreateAggregate(CreateAggregateApiViewModel model);

        void UpdateAggregate(UpdateAggregateApiViewModel model);
    }
}