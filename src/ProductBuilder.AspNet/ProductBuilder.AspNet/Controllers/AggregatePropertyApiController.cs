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

        [Route("api/{productid}/{aggregateid}/aggregate-properties-data-table", Name = nameof(AggregatePropertiesDataTable))]
        public IActionResult AggregatePropertiesDataTable(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetDataTableViewModel(aggregateId));
        }

        [Route("api/{productid}/{aggregateid}/aggregate-properties-json-array", Name = nameof(AggregatePropertiesJsonArray))]
        public IActionResult AggregatePropertiesJsonArray(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetAggregatePropertiesJsonArrayApiViewModel(aggregateId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken] [Route("api/{productid}/{aggregateid}/update-aggregate-property", Name = nameof(UpdateAggregateProperty))]
        public IActionResult UpdateAggregateProperty(UpdateAggregatePropertyApiViewModel model)
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
        [Route("api/{aggregateid}/delete-aggregate-property", Name = nameof(DeleteAggregateProperty))]
        public IActionResult DeleteAggregateProperty(DeleteAggregatePropertyApiViewModel model)
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
        [Route("api/{aggregateid}/CreateAggregateProperty", Name = nameof(CreateAggregateProperty))]
        public IActionResult CreateAggregateProperty(CreateAggregatePropertyApiViewModel model)
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