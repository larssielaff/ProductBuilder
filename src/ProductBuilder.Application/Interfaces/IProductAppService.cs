namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    using ProductBuilder.Application.ViewModels.ProductApi;

    public interface IProductAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();
        void CreateProduct(CreateProductApiViewModel model, string userProfileEmailAddress);
        ProductTeamMembersApiViewModel GetProductTeamMembersApiViewModel(Guid productId);
    }
}