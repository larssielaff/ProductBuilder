namespace ProductBuilder.Domain.Commands.Query
{
    using ProductBuilder.Domain.Commands.Query.Base;
    using ProductBuilder.Domain.Validations.Query;
    using System;

    public class CreateQueryCommand : QueryCommand
    {
        public CreateQueryCommand(Guid id, string queryName, string routeTemplate, Guid asdAggregateId, Guid productId, Guid aggregateId)
        {
            Id = id;
            QueryName = queryName;
            RouteTemplate = routeTemplate;
            AsdAggregateId = asdAggregateId;
            ProductId = productId;
            AggregateId = aggregateId;
        }

        public override bool IsValid()
        {
            ValidationResult = new QueryValidator<QueryCommand>()
                .Validate(this);
            return ValidationResult.IsValid;
        }
    }
}