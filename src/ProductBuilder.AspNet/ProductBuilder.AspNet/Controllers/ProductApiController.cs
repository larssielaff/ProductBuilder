namespace ProductBuilder.WebApp.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
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

        [Route("5ed93f89-47c1-4506-9559-cbd3c1ecf73b", Name = nameof(ProductsDataTable))]
        public IActionResult ProductsDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }
    }
}