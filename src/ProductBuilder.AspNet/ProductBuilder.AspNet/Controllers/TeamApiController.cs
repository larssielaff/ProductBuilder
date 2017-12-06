namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.TeamApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class TeamApiController : AsdController
    {
        private readonly ITeamAppService _appService;
        public TeamApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, ITeamAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("TeamsDataTable", Name = nameof(TeamsDataTable))]
        public IActionResult TeamsDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateTeam", Name = nameof(CreateTeam))]
        public IActionResult CreateTeam(CreateTeamApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateTeam(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}