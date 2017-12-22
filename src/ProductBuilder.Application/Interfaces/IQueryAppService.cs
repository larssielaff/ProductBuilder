namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.QueryApi;

    public interface IQueryAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        void CreateQuery(CreateQueryApiViewModel model);

        void UpdateQuery(UpdateQueryApiViewModel model);

        void DeleteQuery(DeleteQueryApiViewModel model);
    }
}