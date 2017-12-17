namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.AggregateApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class AggregateApiController : AsdController
    {
        private readonly IAggregateAppService _appService;

        public AggregateApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IAggregateAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("api/AggregatesDataTable", Name = nameof(AggregatesDataTable))]
        public IActionResult AggregatesDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/DeleteAggregate", Name = nameof(DeleteAggregate))]
        public IActionResult DeleteAggregate(DeleteAggregateApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteAggregate(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/CreateAggregate", Name = nameof(CreateAggregate))]
        public IActionResult CreateAggregate(CreateAggregateApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateAggregate(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/UpdateAggregate", Name = nameof(UpdateAggregate))]
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