namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.AcceptanceCriteriaApi;

    public interface IAcceptanceCriteriaAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        void CreateAcceptanceCriteria(CreateAcceptanceCriteriaApiViewModel model);

        void UpdateAcceptanceCriteria(UpdateAcceptanceCriteriaApiViewModel model);

        void DeleteAcceptanceCriteria(DeleteAcceptanceCriteriaApiViewModel model);
    }
}