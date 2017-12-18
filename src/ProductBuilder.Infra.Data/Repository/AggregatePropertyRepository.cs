namespace ProductBuilder.Infra.Data.Repository
{
    using Asd.Infra.Data.Context;
    using Asd.Infra.Data.Repository;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;

    public class AggregatePropertyRepository : AsdRepository<AggregateProperty>, IAggregatePropertyRepository
    {
        public AggregatePropertyRepository(AsdSqlContext context) 
            : base(context) { }
    }
}