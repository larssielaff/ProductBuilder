namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.UserStoryApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class UserStoryApiController : AsdController
    {
        private readonly IUserStoryAppService _appService;

        public UserStoryApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IUserStoryAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("UserStoriesDataTable", Name = nameof(UserStoriesDataTable))]
        public IActionResult UserStoriesDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AssignTopic", Name = nameof(AssignTopic))]
        public IActionResult AssignTopic(AssignTopicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.AssignTopic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("DeleteUserStory", Name = nameof(DeleteUserStory))]
        public IActionResult DeleteUserStory(DeleteUserStoryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteUserStory(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateUserStory", Name = nameof(CreateUserStory))]
        public IActionResult CreateUserStory(CreateUserStoryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateUserStory(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("RemoveTopic", Name = nameof(RemoveTopic))]
        public IActionResult RemoveTopic(RemoveTopicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.RemoveTopic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AssignUserRole", Name = nameof(AssignUserRole))]
        public IActionResult AssignUserRole(AssignUserRoleApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.AssignUserRole(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("UpdateStoryPoints", Name = nameof(UpdateStoryPoints))]
        public IActionResult UpdateStoryPoints(UpdateStoryPointsApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateStoryPoints(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("UpdateUserStory", Name = nameof(UpdateUserStory))]
        public IActionResult UpdateUserStory(UpdateUserStoryApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateUserStory(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AssignEpic", Name = nameof(AssignEpic))]
        public IActionResult AssignEpic(AssignEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.AssignEpic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("UpdateValue", Name = nameof(UpdateValue))]
        public IActionResult UpdateValue(UpdateValueApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateValue(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("RemoveUserRole", Name = nameof(RemoveUserRole))]
        public IActionResult RemoveUserRole(RemoveUserRoleApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.RemoveUserRole(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("RemoveEpic", Name = nameof(RemoveEpic))]
        public IActionResult RemoveEpic(RemoveEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.RemoveEpic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}