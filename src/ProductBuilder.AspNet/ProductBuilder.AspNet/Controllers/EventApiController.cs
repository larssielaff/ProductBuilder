namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.EventApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class EventApiController : AsdController
    {
        private readonly IEventAppService _appService;

        public EventApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IEventAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("api/{productid}/{aggregateid}/events-data-table", Name = nameof(EventsDataTable))]
        public IActionResult EventsDataTable(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetDataTableViewModel(aggregateId));
        }

        [Route("api/{productid}/{aggregateid}/domain-aggregate-events-json-array", Name = nameof(DomainAggregateEventsJsonArray))]
        public IActionResult DomainAggregateEventsJsonArray(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetDomainAggregateEventsJsonArrayApiViewModel(aggregateId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/{aggregateId}/create-event", Name = nameof(CreateEvent))]
        public IActionResult CreateEvent(CreateEventApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateEvent(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/{aggregateid}/UpdateEvent", Name = nameof(UpdateEvent))]
        public IActionResult UpdateEvent(UpdateEventApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateEvent(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/{aggregateid}/delete-event", Name = nameof(DeleteEvent))]
        public IActionResult DeleteEvent(DeleteEventApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteEvent(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}