namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.AggregatePropertyApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class AggregatePropertyApiController : AsdController
    {
        private readonly IAggregatePropertyAppService _appService;

        public AggregatePropertyApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IAggregatePropertyAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("api/AggregatePropertiesDataTable", Name = nameof(AggregatePropertiesDataTable))]
        public IActionResult AggregatePropertiesDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] [Route("api/UpdateAggregate", Name = nameof(UpdateAggregate))]
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
    }
}