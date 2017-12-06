namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class TeamMemberApiController : AsdController
    {
        private readonly ITeamMemberAppService _appService;

        public TeamMemberApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, ITeamMemberAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("TeamMembersDataTable", Name = nameof(TeamMembersDataTable))]
        public IActionResult TeamMembersDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }
    }
}