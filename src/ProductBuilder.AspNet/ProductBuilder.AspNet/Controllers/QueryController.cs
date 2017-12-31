namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class QueryController : AsdController
    {
        private readonly IQueryAppService _appService;

        public QueryController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IQueryAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("products/{productid}/queries/{queryid}", Name = nameof(Query))]
        public IActionResult Query(Guid queryId)
        {
            if (queryId == Guid.Empty)
                return NotFound();
            return View(nameof(Query), _appService.GetQueryViewModel(queryId));
        }
    }
}