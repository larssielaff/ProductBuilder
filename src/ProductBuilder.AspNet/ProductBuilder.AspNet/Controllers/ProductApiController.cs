namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.ProductApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class ProductApiController : AsdController
    {
        private readonly IProductAppService _appService;
        public ProductApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IProductAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("ProductsDataTable", Name = nameof(ProductsDataTable))]
        public IActionResult ProductsDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateProduct", Name = nameof(CreateProduct))]
        public IActionResult CreateProduct(CreateProductApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateProduct(model, User.Identity.Name);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}