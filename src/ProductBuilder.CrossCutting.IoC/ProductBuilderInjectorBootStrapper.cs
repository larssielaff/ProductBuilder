/*namespace ProductBuilder.Infra.CrossCutting.IoC
{
    using ProductBuilder.Application.Services;
    using ProductBuilder.Application.Interfaces;
    using ProductBuilder.Infra.Data.Repository;
    using ProductBuilder.Domain.Interfaces;
    using ProductBuilder.Domain.CommandHandlers;
    using ProductBuilder.Domain.EventHandlers;
    using ProductBuilder.Application.AutoMapper;
    using ProductBuilder.Infra.Data.Context;
    using ProductBuilder.Domain.Commands.Product;
    //using ProductBuilder.Domain.Events.Product;
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
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

    public static class ProductBuilderInjectorBootStrapper
    {
        public static IServiceCollection AddProductBuilderDDD(this IServiceCollection services, string dataConnectionString, string eventStoreConnectionString) => services?
            .RegisterDDD(dataConnectionString, eventStoreConnectionString)?
            .RegisterProduct();

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

        private static IServiceCollection RegisterProduct(this IServiceCollection services)
        {
            services?.AddScoped<IProductAppService, ProductAppService>();
            services?.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}*/

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
    using ProductBuilder.Domain.Commands.Product;
    //using ProductBuilder.Domain.Events.Product;
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;
    public static class ProductBuilderInjectorBootStrapper
    {
        public static IServiceCollection AddProductBuilderDDD(this IServiceCollection services, string dataConnectionString, string eventStoreConnectionString) => services?
            .RegisterDDD(dataConnectionString, eventStoreConnectionString)?
            .RegisterProduct();
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
        private static IServiceCollection RegisterProduct(this IServiceCollection services)
        {
            services?.AddScoped<IProductAppService, ProductAppService>();
            services?.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
