namespace ProductBuilder.Infra.Data.Context
{
    using Asd.Infra.Data.Context;
    using ProductBuilder.Domain.Models;
    using System.Data.Entity;

    public class ProductBuilderSqlContext : AsdSqlContext
    {
        public virtual DbSet<Command> Commands { get; set; }

        public virtual DbSet<UserStory> UserStories { get; set; }

        public virtual DbSet<AggregateProperty> AggregateProperties { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Topic> Topics { get; set; }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public virtual DbSet<DomainCommandArgument> DomainCommandArguments { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<Query> Queries { get; set; }

        public virtual DbSet<TeamMember> TeamMembers { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<Epic> Epics { get; set; }

        public virtual DbSet<Aggregate> Aggregates { get; set; }

        public virtual DbSet<AcceptanceCriteria> AcceptanceCriterias { get; set; }

        public ProductBuilderSqlContext(string connectionString) 
            : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductBuilderSqlContext, ProductBuilderSqlMigrationsConfiguration>(true));

            modelBuilder.Entity<DomainCommandArgument>()
                .HasOptional(x => x.Command)
                .WithMany(x => x.DomainCommandArguments)
                .HasForeignKey(x => x.DomainCommandId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcceptanceCriteria>()
                .HasOptional(x => x.UserStory)
                .WithMany(x => x.AcceptanceCriterias)
                .HasForeignKey(x => x.UserStoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DomainCommandArgument>()
                .HasOptional(x => x.AggregateProperty)
                .WithMany(x => x.DomainCommandArguments)
                .HasForeignKey(x => x.DomainAggregatePropertyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Command>()
                .HasOptional(x => x.Event)
                .WithMany(x => x.Commands)
                .HasForeignKey(x => x.DomainEventId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserStory>()
                .HasOptional(x => x.Topic)
                .WithMany(x => x.UserStories)
                .HasForeignKey(x => x.TopicId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TeamMember>()
                .HasOptional(x => x.UserProfile)
                .WithMany(x => x.TeamMembers)
                .HasForeignKey(x => x.UserProfileId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserStory>()
                .HasOptional(x => x.Product)
                .WithMany(x => x.UserStories)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Topic>()
                .HasOptional(x => x.Product)
                .WithMany(x => x.Topics)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRole>()
                .HasOptional(x => x.Product)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Query>()
                .HasOptional(x => x.Product)
                .WithMany(x => x.Queries)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Epic>()
                .HasOptional(x => x.Product)
                .WithMany(x => x.Epics)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Aggregate>()
                .HasOptional(x => x.Product)
                .WithMany(x => x.Aggregates)
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

            modelBuilder.Entity<UserStory>()
                .HasOptional(x => x.UserRole)
                .WithMany(x => x.UserStories)
                .HasForeignKey(x => x.UserRoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserStory>()
                .HasOptional(x => x.Epic)
                .WithMany(x => x.UserStories)
                .HasForeignKey(x => x.EpicId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Command>()
                .HasOptional(x => x.Aggregate)
                .WithMany(x => x.Commands)
                .HasForeignKey(x => x.DomainAggregateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AggregateProperty>()
                .HasOptional(x => x.Aggregate)
                .WithMany(x => x.AggregateProperties)
                .HasForeignKey(x => x.AsdAggregateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AggregateProperty>()
                .HasOptional(x => x.LinkedAggregate)
                .WithMany(x => x.LinkedAggregateProperties)
                .HasForeignKey(x => x.LinkedAggregateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasOptional(x => x.Aggregate)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.AsdAggregateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Query>()
                .HasOptional(x => x.Aggregate)
                .WithMany(x => x.Queries)
                .HasForeignKey(x => x.AsdAggregateId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}