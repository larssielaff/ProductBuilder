namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.QueryApi;
    using ProductBuilder.Application.ViewModels.Query;

    public interface IQueryAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel(Guid productId);

        QueryViewModel GetQueryViewModel(Guid queryId);

        void CreateQuery(CreateQueryApiViewModel model);

        void UpdateQuery(UpdateQueryApiViewModel model);

        void DeleteQuery(DeleteQueryApiViewModel model);
    }
}