namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.CommandApi;
    using ProductBuilder.Application.ViewModels.Command;

    public interface ICommandAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel(Guid aggregateId);

        DomainCommandViewModel GetDomainCommandViewModel(Guid commandId);

        void UpdateCommand(UpdateCommandApiViewModel model);

        void DeleteCommand(DeleteCommandApiViewModel model);

        void CreateCommand(CreateCommandApiViewModel model);
    }
}