namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.AggregateApi;
    using ProductBuilder.Application.ViewModels.Aggregate;
    using System.Collections.Generic;

    public interface IAggregateAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel(Guid productId);

        AggregateViewModel GetAggregateViewModel(Guid aggregateId);

        VisJsNetworkApiViewModel GetAggregateMapQueryResult(Guid productId);

        AggregateCodeViewModel GetAggregateCodeViewModel(Guid aggregateId);

        IEnumerable<AggregateQueryResult> GetProductAggregatesJsonArray(Guid productId);

        void DeleteAggregate(DeleteAggregateApiViewModel model);

        void CreateAggregate(CreateAggregateApiViewModel model);

        void UpdateAggregate(UpdateAggregateApiViewModel model);
    }
}