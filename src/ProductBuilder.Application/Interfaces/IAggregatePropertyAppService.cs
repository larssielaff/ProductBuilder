namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.AggregatePropertyApi;
    using System.Collections.Generic;

    public interface IAggregatePropertyAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel(Guid asdAggregateId);

        IEnumerable<AggregatePropertiesJsonArrayApiViewModel> GetAggregatePropertiesJsonArrayApiViewModel(Guid aggregateId);

        void UpdateAggregate(UpdateAggregatePropertyApiViewModel model);

        void DeleteAggregate(DeleteAggregatePropertyApiViewModel model);

        void CreateAggregate(CreateAggregatePropertyApiViewModel model);
    }
}