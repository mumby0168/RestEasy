


using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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

            var genericGet = builder.ServiceProvider.GetService<IGetHandler<TDomain, TDto>>();


            builder.MapGet(basePath + "/{id}", async context =>
            {
                var guid = context.Request.ExtractGuidFromRoute();
                try
                {
                    var result = await genericGet.GetAsync(guid);
                    context.Ok(result);
                }
                catch (Exception e)
                {
                    context.NoContent();
                }
            });

            builder.MapGet(basePath, async context =>
            {
                var result = await genericGet.GetAllAsync();
                var enumerable = result.ToList();
                if (enumerable.Any()) context.Ok(enumerable);
                else context.NoContent(enumerable);
            });
            
            
            return builder;
        }
        
    }
}