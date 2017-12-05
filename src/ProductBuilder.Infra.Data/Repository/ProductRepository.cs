namespace ProductBuilder.Infra.Data.Repository
{
    using Asd.Infra.Data.Context;
    using Asd.Infra.Data.Repository;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.Models;
    public class ProductRepository : AsdRepository<Product>, IProductRepository
    {
        public ProductRepository(AsdSqlContext context) 
            : base(context) { }
    }
}