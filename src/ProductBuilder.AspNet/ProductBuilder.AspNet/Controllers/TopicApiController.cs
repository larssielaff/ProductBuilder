namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.TopicApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class TopicApiController : AsdController
    {
        private readonly ITopicAppService _appService;

        public TopicApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, ITopicAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("TopicsDataTable", Name = nameof(TopicsDataTable))]
        public IActionResult TopicsDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DeleteTopic", Name = nameof(DeleteTopic))]
        public IActionResult DeleteTopic(DeleteTopicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteTopic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateTopic", Name = nameof(CreateTopic))]
        public IActionResult CreateTopic(CreateTopicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateTopic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("UpdateTopic", Name = nameof(UpdateTopic))]
        public IActionResult UpdateTopic(UpdateTopicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateTopic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}