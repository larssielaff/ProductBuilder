namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.AggregateApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;
    using ProductBuilder.Application.ViewModels;

    [Authorize]
    public class AggregateApiController : AsdController
    {
        private readonly IAggregateAppService _appService;

        public AggregateApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IAggregateAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("api/{productid}/aggregatesdatatable", Name = nameof(AggregatesDataTable))]
        public IActionResult AggregatesDataTable(Guid productId)
        {
            if (productId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetDataTableViewModel(productId));
        }

        [Route("api/{productid}/aggregates-json-array", Name = nameof(ProductAggregatesJsonArray))]
        public IActionResult ProductAggregatesJsonArray(Guid productId)
        {
            if (productId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetProductAggregatesJsonArray(productId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/delete-aggregate", Name = nameof(DeleteAggregate))]
        public IActionResult DeleteAggregate(DeleteAggregateApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteAggregate(model);
            if (IsValidOperation)
                return Ok(new OkApiViewModel()
                {
                    RedirectUrl = Url.RouteUrl(nameof(ProductController.Product), new
                    {
                        productId = model.ProductId
                    })
                });
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/create-aggregate", Name = nameof(CreateAggregate))]
        public IActionResult CreateAggregate(CreateAggregateApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            model.Id = Guid.NewGuid();
            _appService.CreateAggregate(model);
            if (IsValidOperation)
                return Ok(new OkApiViewModel()
                {
                   RedirectUrl = Url.RouteUrl(nameof(AggregateController.Aggregate), new
                   {
                       productId = model.ProductId,
                       aggregateId = model.Id
                   })
                });
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/update-aggregate", Name = nameof(UpdateAggregate))]
        public IActionResult UpdateAggregate(UpdateAggregateApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateAggregate(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}