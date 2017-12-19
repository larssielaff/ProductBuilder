namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.AggregatePropertyApi;

    public interface IAggregatePropertyAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel(Guid asdAggregateId);

        void UpdateAggregate(UpdateAggregatePropertyApiViewModel model);

        void DeleteAggregate(DeleteAggregatePropertyApiViewModel model);

        void CreateAggregate(CreateAggregatePropertyApiViewModel model);
    }
}