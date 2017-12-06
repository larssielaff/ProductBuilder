namespace ProductBuilder.Infra.Data.Context
{
    using System.Data.Entity.Migrations;

    public class ProductBuilderSqlMigrationsConfiguration : DbMigrationsConfiguration<ProductBuilderSqlContext>
    {
        public ProductBuilderSqlMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}