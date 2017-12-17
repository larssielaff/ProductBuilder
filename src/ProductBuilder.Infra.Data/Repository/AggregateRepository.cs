namespace ProductBuilder.Infra.Data.Repository
{
    using Asd.Infra.Data.Context;
    using Asd.Infra.Data.Repository;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;

    public class AggregateRepository : AsdRepository<Aggregate>, IAggregateRepository
    {
        public AggregateRepository(AsdSqlContext context) 
            : base(context) { }
    }
}