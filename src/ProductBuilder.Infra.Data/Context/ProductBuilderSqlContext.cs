namespace ProductBuilder.Infra.Data.Context
{
    using Asd.Infra.Data.Context;
    using ProductBuilder.Domain.Models;
    using System.Data.Entity;
    public class ProductBuilderSqlContext : AsdSqlContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public ProductBuilderSqlContext(string connectionString) 
            : base(connectionString) { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}