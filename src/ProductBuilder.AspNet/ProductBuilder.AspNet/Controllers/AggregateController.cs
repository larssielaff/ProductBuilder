namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class AggregateController : AsdController
    {
        private readonly IAggregateAppService _appService;

        public AggregateController(IAsdDomainNotificationHandler<AsdDomainNotification> notifications, IAggregateAppService appService) 
            : base(notifications)
        {
            _appService = appService ?? throw new ArgumentNullException(nameof(appService));
        }

        [Route("products/{productid}/aggregates/{aggregateid}", Name = nameof(Aggregate))]
        public IActionResult Aggregate(Guid productId, Guid aggregateId)
        {
            return View(nameof(Aggregate), _appService.GetAggregateViewModel(aggregateId));
        }

        [Route("products/{productid}/aggregates/{aggregateid}/code", Name = nameof(AggregateCode))]
        public IActionResult AggregateCode(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
                return NotFound();
            return View(nameof(AggregateCode), _appService.GetAggregateCodeViewModel(aggregateId));
        }
    }
}