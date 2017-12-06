namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.UserProfileApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class UserProfileApiController : AsdController
    {
        private readonly IUserProfileAppService _appService;

        public UserProfileApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IUserProfileAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("UserProfilesDataTable", Name = nameof(UserProfilesDataTable))]
        public IActionResult UserProfilesDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateUserProfile", Name = nameof(CreateUserProfile))]
        public IActionResult CreateUserProfile(CreateUserProfileApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateUserProfile(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}