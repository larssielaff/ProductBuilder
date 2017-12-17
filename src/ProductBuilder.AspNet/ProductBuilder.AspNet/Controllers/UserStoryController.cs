namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class UserStoryController : AsdController
    {
        private readonly IUserStoryAppService _appService;

        public UserStoryController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IUserStoryAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("products/{productid}/userstories/{userstoryid}", Name = nameof(ProductUserStory))]
        public IActionResult ProductUserStory(Guid productId, Guid userStoryId)
        {
            if (productId == Guid.Empty)
                return NotFound();
            if (userStoryId == Guid.Empty)
                return NotFound();
            return View(nameof(ProductUserStory), _appService.GetProductUserStoryViewModel(productId, userStoryId));
        }
    }
}