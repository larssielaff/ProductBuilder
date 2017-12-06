namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.UserProfileApi;
    public interface IUserProfileAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();
        void CreateUserProfile(CreateUserProfileApiViewModel model);
    }
}
