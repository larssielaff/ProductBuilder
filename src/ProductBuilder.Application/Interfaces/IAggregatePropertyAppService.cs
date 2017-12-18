namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.AggregatePropertyApi;

    public interface IAggregatePropertyAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        void UpdateAggregate(UpdateAggregateApiViewModel model);

        void DeleteAggregate(DeleteAggregateApiViewModel model);

        void CreateAggregate(CreateAggregateApiViewModel model);
    }
}