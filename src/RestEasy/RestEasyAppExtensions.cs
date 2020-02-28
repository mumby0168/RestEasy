


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using RestEasy.Core.Factories;
using RestEasy.Core.Handlers;
using RestEasy.Core.Markers;

namespace RestEasy
{
    public static class RestEasyAppExtensions
    {
        public static IEndpointRouteBuilder MapRestEasyApi<TDomain, TDto>(this IEndpointRouteBuilder builder, string basePath) where TDto : IDto where TDomain : IDomain<TDto>
        {
            builder.MapPost(basePath, async context =>
            {
                var genericPost = builder.ServiceProvider.GetService<IPostHandler<TDomain, TDto>>();
                var dto = await context.Request.ReadAsJsonAsync<TDto>();
                await genericPost.HandleAsync(dto, context);
            });
            
            
            return builder;
        }
        
    }
}