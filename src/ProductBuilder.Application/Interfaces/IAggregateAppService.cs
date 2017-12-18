namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.AggregateApi;
    using ProductBuilder.Application.ViewModels.Aggregate;

    public interface IAggregateAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel(Guid productId);

        AggregateViewModel GetAggregateViewModel(Guid aggregateId);

        void DeleteAggregate(DeleteAggregateApiViewModel model);

        void CreateAggregate(CreateAggregateApiViewModel model);

        void UpdateAggregate(UpdateAggregateApiViewModel model);
    }
}