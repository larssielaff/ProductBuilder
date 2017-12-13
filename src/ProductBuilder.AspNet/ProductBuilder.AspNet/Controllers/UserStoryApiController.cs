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

        [Route("api/{productid}/productuserstoriesdatatable", Name = nameof(ProductUserStoriesDataTable))]
        public IActionResult ProductUserStoriesDataTable(Guid productId)
        {
            if (productId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetProductUserStoriesDataTableApiViewModel(productId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{productid}/assigntopic", Name = nameof(AssignTopic))]
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
        [Route("api/{productid}/deleteuserstory", Name = nameof(DeleteUserStory))]
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
        [Route("api/{productid}/createuserstory", Name = nameof(CreateUserStory))]
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
        [Route("api/{productid}/removetopic", Name = nameof(RemoveTopic))]
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
        [Route("api/{productid}/assignuserrole", Name = nameof(AssignUserRole))]
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
        [Route("api/{productid}/updatestorypoints", Name = nameof(UpdateStoryPoints))]
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
        [Route("api/{productid}/updateuserstory", Name = nameof(UpdateUserStory))]
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
        [Route("api/{productid}/assignepic", Name = nameof(AssignEpic))]
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
        [Route("api/{productid}/updatevalue", Name = nameof(UpdateValue))]
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
        [Route("api/{productId}/removeuserrole", Name = nameof(RemoveUserRole))]
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
        [Route("api/{productid}/removeepic", Name = nameof(RemoveEpic))]
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