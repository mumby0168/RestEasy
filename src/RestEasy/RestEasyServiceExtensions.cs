using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RestEasy.Builders;
using RestEasy.Core.Builders;
using RestEasy.Core.Dispatcher;
using RestEasy.Core.Factories;
using RestEasy.Core.Handlers;
using RestEasy.Core.Handlers.Generic;
using RestEasy.Core.Markers;
using RestEasy.Core.Middleware;
using RestEasy.Core.Persistence;
using RestEasy.Dispatchers;
using RestEasy.Factories;
using RestEasy.Handlers;

namespace RestEasy
{
    public static class RestEasyServiceExtensions
    {
        public static IServiceCollection AddRestEasy(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<RestEasyApiMiddleware>();
            serviceCollection.AddTransient<IRestEasyApiFactory, RestEastApiFactory>();
            serviceCollection.AddTransient<IRestEasyRepositoryFactory, RestEasyRepositoryFactory>();
            serviceCollection.AddSingleton<IRestEasyEndpointBuilder, RestEasyEndpointBuilder>();
            serviceCollection.Scan(s =>
                s.FromAssemblies(Assembly.GetEntryAssembly())
                    
                    .AddClasses(c => c.AssignableTo(typeof(IRestEasyReadManyRequest<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                    
                    .AddClasses(c => c.AssignableTo(typeof(IRestEasyReadRequest<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                    
                    .AddClasses(c => c.AssignableTo(typeof(IRestEasyCreateRequest<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                    
                    .AddClasses(c => c.AssignableTo(typeof(IRestEasyUpdateRequest<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                    
                    .AddClasses(c => c.AssignableTo(typeof(IRestEasyReadManyRequest<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                    
                    .AddClasses(c => c.AssignableTo(typeof(IRestEasyReadRequest<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
            );

            serviceCollection.AddTransient<IRestEasyDispatcher, RestEasyDispatcher>();
            return serviceCollection; 
        }

        public static IServiceCollection AddRestEasyApi<TDomain, TDto, TRepo>(this IServiceCollection services)
            where TRepo : class, IRepository<TDomain, TDto> where TDto : IDto, new() where TDomain : IDomain<TDto>, new()
        {

            services.AddTransient<RestEasyApiMiddleware>();
            services.AddTransient<IRepository<TDomain, TDto>, TRepo>();
            services.AddTransient<IRestEasyPostHandler<TDomain, TDto>, GenericRestEasyPostHandler<TDomain, TDto>>();
            services.AddTransient<IGetHandler<TDomain, TDto>, GenericGetHandler<TDomain, TDto>>();
            services.AddTransient<IPutHandler<TDomain, TDto>, GenericPutHandler<TDomain, TDto>>();
            services.AddTransient<IDeleteHandler<TDomain, TDto>, GenericDeleteHandler<TDomain, TDto>>();
            services.AddSingleton<IRestEasyEndpointBuilder, RestEasyEndpointBuilder>();
            return services;
        }
    }
}