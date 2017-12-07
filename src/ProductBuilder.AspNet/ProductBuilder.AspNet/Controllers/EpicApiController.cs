namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Application.ViewModels.EpicApi;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class EpicApiController : AsdController
    {
        private readonly IEpicAppService _appService;
        public EpicApiController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IEpicAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("api/EpicsDataTable", Name = nameof(EpicsDataTable))]
        public IActionResult EpicsDataTable()
        {
            return Json(_appService.GetDataTableViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/DeleteEpic", Name = nameof(DeleteEpic))]
        public IActionResult DeleteEpic(DeleteEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.DeleteEpic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/CreateEpic", Name = nameof(CreateEpic))]
        public IActionResult CreateEpic(CreateEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.CreateEpic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("api/UpdateEpic", Name = nameof(UpdateEpic))]
        public IActionResult UpdateEpic(UpdateEpicApiViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid)
                return BadRequest();
            _appService.UpdateEpic(model);
            if (IsValidOperation)
                return Ok();
            return BadRequest();
        }
    }
}