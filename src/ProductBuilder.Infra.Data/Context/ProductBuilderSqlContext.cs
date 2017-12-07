namespace ProductBuilder.Infra.Data.Context
{
    using Asd.Infra.Data.Context;
    using ProductBuilder.Domain.Models;
    using System.Data.Entity;
    public class ProductBuilderSqlContext : AsdSqlContext
    {
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<TeamMember> TeamMembers { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public ProductBuilderSqlContext(string connectionString) 
            : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductBuilderSqlContext, ProductBuilderSqlMigrationsConfiguration>(true));
            modelBuilder.Entity<TeamMember>()
                .HasOptional(x => x.UserProfile)
                .WithMany(x => x.TeamMembers)
                .HasForeignKey(x => x.UserProfileId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<UserRole>()
                .HasOptional(x => x.Product)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Team>()
                .HasOptional(x => x.Product)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<TeamMember>()
                .HasOptional(x => x.Team)
                .WithMany(x => x.TeamMembers)
                .HasForeignKey(x => x.TeamId)
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}