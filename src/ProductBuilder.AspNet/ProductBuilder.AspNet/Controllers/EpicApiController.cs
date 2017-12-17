namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.EpicApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class EpicApiController : AsdController
    {
        private readonly IEpicAppService _appService;
        public EpicApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IEpicAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("api/{productid}/productepicsdatatable", Name = nameof(ProductEpicsDataTable))]
        public IActionResult ProductEpicsDataTable(Guid productId)
        {
            if (productId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetProductEpicsDataTableApiViewModel(productId));
        }

        [Route("api/{productid}/product-epics-json-array", Name = nameof(ProductEpicsJsonArray))]
        public IActionResult ProductEpicsJsonArray(Guid productId)
        {
            if (productId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetProductEpicsJsonArray(productId));
        }

        [Route("api/EpicsDataTable", Name = nameof(EpicsDataTable))]
        public IActionResult EpicsDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/DeleteEpic", Name = nameof(DeleteEpic))]
        public IActionResult DeleteEpic(DeleteEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteEpic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/createepic", Name = nameof(CreateEpic))]
        public IActionResult CreateEpic(CreateEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateEpic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/updateepic", Name = nameof(UpdateEpic))]
        public IActionResult UpdateEpic(UpdateEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateEpic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}