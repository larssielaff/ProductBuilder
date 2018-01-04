namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.CommandApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class CommandApiController : AsdController
    {
        private readonly ICommandAppService _appService;

        public CommandApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, ICommandAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("", Name = nameof(CommandsDataTable))]
        public IActionResult CommandsDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("", Name = nameof(UpdateCommand))]
        public IActionResult UpdateCommand(UpdateCommandApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateCommand(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("", Name = nameof(DeleteCommand))]
        public IActionResult DeleteCommand(DeleteCommandApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteCommand(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("", Name = nameof(CreateCommand))]
        public IActionResult CreateCommand(CreateCommandApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateCommand(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}