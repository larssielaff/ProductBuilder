﻿namespace ProductBuilder.AspNet.Controllers
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

        [Route("api/{productid}/product-topics-json-array", Name = nameof(ProductTopicsJsonArray))]
        public IActionResult ProductTopicsJsonArray(Guid productId)
        {
            if (productId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetProductTopicsJsonArray(productId));
        }

        [Route("api/{productid}/producttopicsdatatables", Name = nameof(ProductTopicsDataTable))]
        public IActionResult ProductTopicsDataTable(Guid productId)
        {
            if (productId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetProductTopicsDataTableApiViewModel(productId));
        }

        [Route("TopicsDataTable", Name = nameof(TopicsDataTable))]
        public IActionResult TopicsDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/deletetopic", Name = nameof(DeleteTopic))]
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
        [Route("api/{productid}/createtopic", Name = nameof(CreateTopic))]
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
        [Route("api/{productid}/updatetopic", Name = nameof(UpdateTopic))]
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