namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.EpicApi;
    using System.Collections.Generic;

    public interface IEpicAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        ProductEpicsDataTableApiViewModel GetProductEpicsDataTableApiViewModel(Guid productId);

        IEnumerable<EpicQueryResult> GetProductEpicsJsonArray(Guid productId);

        void DeleteEpic(DeleteEpicApiViewModel model);

        void CreateEpic(CreateEpicApiViewModel model);

        void UpdateEpic(UpdateEpicApiViewModel model);
    }
}