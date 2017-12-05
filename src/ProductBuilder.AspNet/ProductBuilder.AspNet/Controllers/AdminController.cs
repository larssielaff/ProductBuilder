namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class AdminController : AsdController
    {
        public AdminController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications) 
            : base(notifications)
        {
        }

        [Route("admin", Name = nameof(Admin))]
        public IActionResult Admin()
        {
            return View(nameof(Admin));
        }
    }
}
