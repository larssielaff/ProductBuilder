namespace ProductBuilder.AspNet.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;

    public class HomeController : AsdController
    {
        public HomeController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications) 
            : base(notifications) { }

        [Route("", Name = nameof(Index))]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
