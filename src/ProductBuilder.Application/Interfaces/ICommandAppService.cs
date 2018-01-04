namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.CommandApi;

    public interface ICommandAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        void UpdateCommand(UpdateCommandApiViewModel model);

        void DeleteCommand(DeleteCommandApiViewModel model);

        void CreateCommand(CreateCommandApiViewModel model);
    }
}