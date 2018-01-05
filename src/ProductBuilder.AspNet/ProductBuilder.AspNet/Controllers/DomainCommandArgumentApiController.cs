namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.DomainCommandArgumentApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class DomainCommandArgumentApiController : AsdController
    {
        private readonly IDomainCommandArgumentAppService _appService;

        public DomainCommandArgumentApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IDomainCommandArgumentAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("api/DomainCommandArgumentsDataTable", Name = nameof(DomainCommandArgumentsDataTable))]
        public IActionResult DomainCommandArgumentsDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/UpdateDomainCommandArgument", Name = nameof(UpdateDomainCommandArgument))]
        public IActionResult UpdateDomainCommandArgument(UpdateDomainCommandArgumentApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateDomainCommandArgument(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/CreateDomainCommandArgument", Name = nameof(CreateDomainCommandArgument))]
        public IActionResult CreateDomainCommandArgument(CreateDomainCommandArgumentApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateDomainCommandArgument(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/DeleteDomainCommandArgument", Name = nameof(DeleteDomainCommandArgument))]
        public IActionResult DeleteDomainCommandArgument(DeleteDomainCommandArgumentApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteDomainCommandArgument(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}