namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.AcceptanceCriteriaApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class AcceptanceCriteriaApiController : AsdController
    {
        private readonly IAcceptanceCriteriaAppService _appService;

        public AcceptanceCriteriaApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IAcceptanceCriteriaAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("api/{userstoryid}/user-story-acceptance-criterias-dataTable", Name = nameof(UserStoryAcceptanceCriteriasDataTable))]
        public IActionResult UserStoryAcceptanceCriteriasDataTable(Guid userStoryId)
        {
            if (userStoryId == Guid.Empty)
                return NotFound();
            return Json(_appService.GetUserStoryAcceptanceCriteriasDataTable(userStoryId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{userstoryid}/createacceptancecriteria", Name = nameof(CreateAcceptanceCriteria))]
        public IActionResult CreateAcceptanceCriteria(CreateAcceptanceCriteriaApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateAcceptanceCriteria(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{userstoryid}/update-acceptance-criteria", Name = nameof(UpdateAcceptanceCriteria))]
        public IActionResult UpdateAcceptanceCriteria(UpdateAcceptanceCriteriaApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateAcceptanceCriteria(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/{userstoryid}/deleteacceptancecriteria", Name = nameof(DeleteAcceptanceCriteria))]
        public IActionResult DeleteAcceptanceCriteria(DeleteAcceptanceCriteriaApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteAcceptanceCriteria(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}