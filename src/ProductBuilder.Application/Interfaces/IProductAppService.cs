namespace ProductBuilder.Application.Interfaces
{
    using System;
    using ProductBuilder.Application.ViewModels;
    public interface IProductAppService : IDisposable
    {
        AjaxDataTableViewModel GetDataTableViewModel();
    }
}