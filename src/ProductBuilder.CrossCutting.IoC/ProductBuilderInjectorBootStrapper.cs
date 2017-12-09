namespace ProductBuilder.Infra.CrossCutting.IoC
{
    using Asd.Infra.Data.Interfaces;
    using Asd.Infra.Data.Repository;
    using Asd.Domain.Core.Events;
    using Asd.Infra.Data.EventSourcing;
    using Asd.Infra.Data.Context;
    using Asd.Domain.Core.Bus;
    using Asd.Infra.CrossCutting.Bus;
    using Asd.Domain.Interfaces;
    using Asd.Infra.Data.UoW;
    using Asd.Domain.Core.Notifications;
    using ProductBuilder.Application.Services;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Infra.Data.Repository;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.CommandHandlers;
    using ProductBuilder.Domain.EventHandlers;
    using ProductBuilder.Application.AutoMapper;
    using ProductBuilder.Infra.Data.Context;
    using ProductBuilder.Domain.Commands.UserStory;
    using ProductBuilder.Domain.Events.UserStory;
    using ProductBuilder.Domain.Commands.Topic;
    using ProductBuilder.Domain.Events.Topic;
    using ProductBuilder.Domain.Commands.UserProfile;
    using ProductBuilder.Domain.Events.UserProfile;
    using ProductBuilder.Domain.Commands.Product;
    using ProductBuilder.Domain.Events.Product;
    using ProductBuilder.Domain.Commands.Team;
    using ProductBuilder.Domain.Events.Team;
    using ProductBuilder.Domain.Commands.TeamMember;
    using ProductBuilder.Domain.Events.TeamMember;
    using ProductBuilder.Domain.Commands.UserRole;
    using ProductBuilder.Domain.Events.UserRole;
    using ProductBuilder.Domain.Commands.Epic;
    using ProductBuilder.Domain.Events.Epic;
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;

    public static class ProductBuilderInjectorBootStrapper
    {
        public static IServiceCollection AddProductBuilderDDD(this IServiceCollection services, string dataConnectionString, string eventStoreConnectionString) => services?
            .RegisterDDD(dataConnectionString, eventStoreConnectionString)?
            .RegisterUserStory()?
            .RegisterTopic()?
            .RegisterUserProfile()?
            .RegisterProduct()?
            .RegisterTeam()?
            .RegisterTeamMember()?
            .RegisterUserRole()?
            .RegisterEpic();

        private static IServiceCollection RegisterDDD(this IServiceCollection services, string dataConnectionString, string eventStoreConnectionString)
        {
            services?.AddAutoMapper(x => 
            {
                x.AddProfile(new ViewModelToDomainMappingProfile());
                x.AddProfile(new DomainToViewModelMappingProfile());
            });
            services?.AddSingleton(Mapper.Configuration);
            services?.AddScoped<IMapper>(x => new Mapper(x.GetRequiredService<IConfigurationProvider>(), x.GetService));
            services?.AddScoped<IAsdEventStoreRepository, AsdEventStoreSqlRepository>();
            services?.AddScoped<IAsdIEventStore, AsdSqlEventStore>();
            services?.AddScoped(x => new AsdEventStoreSqlContext(eventStoreConnectionString));
            services?.AddScoped<IAsdBus, AsdInMemoryBus>();
            services?.AddScoped(p => new ProductBuilderSqlContext(dataConnectionString) as AsdSqlContext);
            services?.AddScoped<IAsdUnitOfWork, AsdUnitOfWork>();
            services?.AddScoped<IAsdDomainNotificationHandler<AsdDomainNotification>, AsdDomainNotificationHandler>();
            return services;
        }

        private static IServiceCollection RegisterUserStory(this IServiceCollection services)
        {
            services?.AddScoped<IUserStoryAppService, UserStoryAppService>();
            services?.AddScoped<IUserStoryRepository, UserStoryRepository>();
            services?.AddScoped<IAsdHandler<AssignTopicCommand>, UserStoryCommandHandler>();
            services?.AddScoped<IAsdHandler<DeleteUserStoryCommand>, UserStoryCommandHandler>();
            services?.AddScoped<IAsdHandler<CreateUserStoryCommand>, UserStoryCommandHandler>();
            services?.AddScoped<IAsdHandler<RemoveTopicCommand>, UserStoryCommandHandler>();
            services?.AddScoped<IAsdHandler<AssignUserRoleCommand>, UserStoryCommandHandler>();
            services?.AddScoped<IAsdHandler<UpdateStoryPointsCommand>, UserStoryCommandHandler>();
            services?.AddScoped<IAsdHandler<UpdateUserStoryCommand>, UserStoryCommandHandler>();
            services?.AddScoped<IAsdHandler<AssignEpicCommand>, UserStoryCommandHandler>();
            services?.AddScoped<IAsdHandler<UpdateValueCommand>, UserStoryCommandHandler>();
            services?.AddScoped<IAsdHandler<RemoveUserRoleCommand>, UserStoryCommandHandler>();
            services?.AddScoped<IAsdHandler<RemoveEpicCommand>, UserStoryCommandHandler>();
            services?.AddScoped<IAsdHandler<StoryPointsUpdatedEvent>, UserStoryEventHandler>();
            services?.AddScoped<IAsdHandler<UserStoryCreatedEvent>, UserStoryEventHandler>();
            services?.AddScoped<IAsdHandler<UserRoleRemovedEvent>, UserStoryEventHandler>();
            services?.AddScoped<IAsdHandler<ValueUpdatedEvent>, UserStoryEventHandler>();
            services?.AddScoped<IAsdHandler<UserStoryUpdatedEvent>, UserStoryEventHandler>();
            services?.AddScoped<IAsdHandler<EpicRemovedEvent>, UserStoryEventHandler>();
            services?.AddScoped<IAsdHandler<UserStoryDeletedEvent>, UserStoryEventHandler>();
            services?.AddScoped<IAsdHandler<TopicAssignedEvent>, UserStoryEventHandler>();
            services?.AddScoped<IAsdHandler<UserRoleAssignedEvent>, UserStoryEventHandler>();
            services?.AddScoped<IAsdHandler<EpicAssignedEvent>, UserStoryEventHandler>();
            services?.AddScoped<IAsdHandler<TopicRemovedEvent>, UserStoryEventHandler>();
            return services;
        }

        private static IServiceCollection RegisterTopic(this IServiceCollection services)
        {
            services?.AddScoped<ITopicAppService, TopicAppService>();
            services?.AddScoped<ITopicRepository, TopicRepository>();
            services?.AddScoped<IAsdHandler<DeleteTopicCommand>, TopicCommandHandler>();
            services?.AddScoped<IAsdHandler<CreateTopicCommand>, TopicCommandHandler>();
            services?.AddScoped<IAsdHandler<UpdateTopicCommand>, TopicCommandHandler>();
            services?.AddScoped<IAsdHandler<TopicUpdatedEvent>, TopicEventHandler>();
            services?.AddScoped<IAsdHandler<TopicCreatedEvent>, TopicEventHandler>();
            services?.AddScoped<IAsdHandler<TopicDeletedEvent>, TopicEventHandler>();
            return services;
        }

        private static IServiceCollection RegisterUserProfile(this IServiceCollection services)
        {
            services?.AddScoped<IUserProfileAppService, UserProfileAppService>();
            services?.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services?.AddScoped<IAsdHandler<CreateUserProfileCommand>, UserProfileCommandHandler>();
            services?.AddScoped<IAsdHandler<UserProfileCreatedEvent>, UserProfileEventHandler>();
            return services;
        }

        private static IServiceCollection RegisterProduct(this IServiceCollection services)
        {
            services?.AddScoped<IProductAppService, ProductAppService>();
            services?.AddScoped<IProductRepository, ProductRepository>();
            services?.AddScoped<IAsdHandler<CreateProductCommand>, ProductCommandHandler>();
            services?.AddScoped<IAsdHandler<ProductCreatedEvent>, ProductEventHandler>();
            return services;
        }

        private static IServiceCollection RegisterTeam(this IServiceCollection services)
        {
            services?.AddScoped<ITeamAppService, TeamAppService>();
            services?.AddScoped<ITeamRepository, TeamRepository>();
            services?.AddScoped<IAsdHandler<CreateTeamCommand>, TeamCommandHandler>();
            services?.AddScoped<IAsdHandler<TeamCreatedEvent>, TeamEventHandler>();
            return services;
        }

        private static IServiceCollection RegisterTeamMember(this IServiceCollection services)
        {
            services?.AddScoped<ITeamMemberAppService, TeamMemberAppService>();
            services?.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
            services?.AddScoped<IAsdHandler<CreateTeamMemberCommand>, TeamMemberCommandHandler>();
            services?.AddScoped<IAsdHandler<TeamMemberCreatedEvent>, TeamMemberEventHandler>();
            return services;
        }

        private static IServiceCollection RegisterUserRole(this IServiceCollection services)
        {
            services?.AddScoped<IUserRoleAppService, UserRoleAppService>();
            services?.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services?.AddScoped<IAsdHandler<CreateUserRoleCommand>, UserRoleCommandHandler>();
            services?.AddScoped<IAsdHandler<DeleteUserRoleCommand>, UserRoleCommandHandler>();
            services?.AddScoped<IAsdHandler<UpdateUserRoleCommand>, UserRoleCommandHandler>();
            services?.AddScoped<IAsdHandler<UserRoleUpdatedEvent>, UserRoleEventHandler>();
            services?.AddScoped<IAsdHandler<UserRoleCreatedEvent>, UserRoleEventHandler>();
            services?.AddScoped<IAsdHandler<UserRoleDeletedEvent>, UserRoleEventHandler>();
            return services;
        }

        private static IServiceCollection RegisterEpic(this IServiceCollection services)
        {
            services?.AddScoped<IEpicAppService, EpicAppService>();
            services?.AddScoped<IEpicRepository, EpicRepository>();
            services?.AddScoped<IAsdHandler<DeleteEpicCommand>, EpicCommandHandler>();
            services?.AddScoped<IAsdHandler<CreateEpicCommand>, EpicCommandHandler>();
            services?.AddScoped<IAsdHandler<UpdateEpicCommand>, EpicCommandHandler>();
            services?.AddScoped<IAsdHandler<EpicCreatedEvent>, EpicEventHandler>();
            services?.AddScoped<IAsdHandler<EpicDeletedEvent>, EpicEventHandler>();
            services?.AddScoped<IAsdHandler<EpicUpdatedEvent>, EpicEventHandler>();
            return services;
        }
    }
}