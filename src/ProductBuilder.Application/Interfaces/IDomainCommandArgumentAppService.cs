namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.DomainCommandArgumentApi;

    public interface IDomainCommandArgumentAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        void UpdateDomainCommandArgument(UpdateDomainCommandArgumentApiViewModel model);

        void CreateDomainCommandArgument(CreateDomainCommandArgumentApiViewModel model);

        void DeleteDomainCommandArgument(DeleteDomainCommandArgumentApiViewModel model);
    }
}