namespace ProductBuilder.AspNet.Controllers
{
    using Asd.AspNet.Controllers;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis;

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

            var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("asd")).NormalizeWhitespace();
            namespaceDeclaration = namespaceDeclaration.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")));

            var classDeclaration = SyntaxFactory.ClassDeclaration("Test");
            classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            classDeclaration = classDeclaration.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("AsdEntity")));


            namespaceDeclaration = namespaceDeclaration.AddMembers(classDeclaration);

            var code = namespaceDeclaration.NormalizeWhitespace()
                .ToFullString();

            ViewBag.Code = code;


            return View(nameof(AggregateCode), _appService.GetAggregateCodeViewModel(aggregateId));
        }
    }
}

namespace ProductBuilder.Domain.Models
{
    using Asd.Domain.Core.Models;
    using System;
    using System.Collections.Generic;

    public class Query : AsdEntity
    {
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string QueryName { get; set; }
        public Guid? AsdAggregateId { get; set; }
        public virtual Aggregate Aggregate { get; set; }
        public string RouteTemplate { get; set; }
        public Query(Guid id) 
            : this()
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));
            Id = id;
        }
        protected Query() { }
    }
}