namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.UserRoleApi;

    public interface IUserRoleAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        void CreateUserRole(CreateUserRoleApiViewModel model);

        void DeleteUserRole(DeleteUserRoleApiViewModel model);

        void UpdateUserRole(UpdateUserRoleApiViewModel model);
        ProductUserRolesApiViewModel GetProductUserRolesApiViewModel(Guid productId);
    }
}