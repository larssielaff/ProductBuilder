namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class UserProfileController : AsdController
    {
        private readonly IUserProfileAppService _appService;
        public UserProfileController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IUserProfileAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("myprofile", Name = nameof(MyProfile))]
        public IActionResult MyProfile()
        {
            return View(nameof(MyProfile));
        }
    }
}