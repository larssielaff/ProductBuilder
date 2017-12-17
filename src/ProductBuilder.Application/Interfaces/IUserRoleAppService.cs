namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.UserRoleApi;
    using System.Collections.Generic;

    public interface IUserRoleAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();

        ProductUserRolesApiViewModel GetProductUserRolesApiViewModel(Guid productId);

        IEnumerable<UserRoleQueryResult> GetProductUserRolesJsonArray(Guid productId);

        void CreateUserRole(CreateUserRoleApiViewModel model);

        void DeleteUserRole(DeleteUserRoleApiViewModel model);

        void UpdateUserRole(UpdateUserRoleApiViewModel model);
    }
}