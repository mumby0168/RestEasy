using Microsoft.Extensions.DependencyInjection;
using RestEasy.Core.Factories;
using RestEasy.Core.Handlers;
using RestEasy.Core.Markers;
using RestEasy.Core.Persistence;
using RestEasy.Factories;
using RestEasy.Handlers;

namespace RestEasy
{
    public static class RestEasyServiceExtensions
    {
        public static IServiceCollection AddRestEasy(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRestEasyApiFactory, RestEastApiFactory>();
            serviceCollection.AddTransient<IRestEasyRepositoryFactory, RestEasyRepositoryFactory>();
            return serviceCollection;
        }

        public static IServiceCollection AddRestEasyApi<TDomain, TDto, TRepo>(this IServiceCollection services)
            where TRepo : class, IRepository<TDomain, TDto> where TDto : IDto where TDomain : IDomain<TDto>, new()
        {

            services.AddTransient<IRepository<TDomain, TDto>, TRepo>();
            services.AddTransient<IPostHandler<TDomain, TDto>, GenericPostHandler<TDomain, TDto>>();
            return services;
        }
        
        
    }
}