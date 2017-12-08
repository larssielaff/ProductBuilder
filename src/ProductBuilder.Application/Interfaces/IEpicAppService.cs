namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.EpicApi;

    public interface IEpicAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        void DeleteEpic(DeleteEpicApiViewModel model);

        void CreateEpic(CreateEpicApiViewModel model);

        void UpdateEpic(UpdateEpicApiViewModel model);
        ProductEpicsDataTableApiViewModel GetProductEpicsDataTableApiViewModel(Guid productId);
    }
}