namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.UserRoleApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class UserRoleApiController : AsdController
    {
        private readonly IUserRoleAppService _appService;
        public UserRoleApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IUserRoleAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("api/userrolesdatatable", Name = nameof(UserRolesDataTable))]
        public IActionResult UserRolesDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/createuserrole", Name = nameof(CreateUserRole))]
        public IActionResult CreateUserRole(CreateUserRoleApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateUserRole(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/deleteuserrole", Name = nameof(DeleteUserRole))]
        public IActionResult DeleteUserRole(DeleteUserRoleApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteUserRole(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/updateuserrole", Name = nameof(UpdateUserRole))]
        public IActionResult UpdateUserRole(UpdateUserRoleApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateUserRole(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}