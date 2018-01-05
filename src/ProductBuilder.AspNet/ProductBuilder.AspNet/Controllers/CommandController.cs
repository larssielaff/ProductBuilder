namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class CommandController : AsdController
    {
        private readonly ICommandAppService _appService;

        public CommandController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, ICommandAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("products/{productid}/aggregates/{aggregateid}/domain-commands/{commandid}", Name = nameof(DomainCommand))]
        public IActionResult DomainCommand(Guid commandId)
        {
            if (commandId == Guid.Empty)
                return NotFound();
            return View(nameof(DomainCommand), _appService.GetDomainCommandViewModel(commandId));
        }
    }
}