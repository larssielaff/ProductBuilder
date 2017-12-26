﻿namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.QueryApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class QueryApiController : AsdController
    {
        private readonly IQueryAppService _appService;

        public QueryApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IQueryAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("api/{productid}/queriesdatatable", Name = nameof(QueriesDataTable))]
        public IActionResult QueriesDataTable(Guid productId)
        {
            if (productId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetDataTableViewModel(productId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/create-query", Name = nameof(CreateQuery))]
        public IActionResult CreateQuery(CreateQueryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateQuery(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/UpdateQuery", Name = nameof(UpdateQuery))]
        public IActionResult UpdateQuery(UpdateQueryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateQuery(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/DeleteQuery", Name = nameof(DeleteQuery))]
        public IActionResult DeleteQuery(DeleteQueryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteQuery(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}