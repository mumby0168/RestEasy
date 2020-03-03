using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using RestEasy.Core.Builders;
using RestEasy.Core.Dispatcher;

namespace RestEasy.Builders
{
    public class RestEasyEndpointBuilder : IRestEasyEndpointBuilder
    {
        private IEndpointRouteBuilder _routeBuilder;
        private readonly IRestEasyDispatcher _dispatcher;

        public RestEasyEndpointBuilder(IRestEasyDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }


        public void Initialize(IEndpointRouteBuilder builder)
        {
            _routeBuilder = builder;
        }    

        public IRestEasyEndpointBuilder Get<TReturns, TTakes>(string path) where TReturns : class where TTakes : class
        {
            _routeBuilder.MapGet(path, async context =>
            {
                var data = await context.ExtractRouteDataAsync<TTakes>();
                var result = await _dispatcher.DispatchRead<TTakes, TReturns>(data);
                if (result is null) context.NoContent(); else context.Ok(result);

            });
            return this;
        }

        public IRestEasyEndpointBuilder Get<TReturns>(string path)
        {
            _routeBuilder.MapGet(path, async context =>
            {
                var data = context.Request.ExtractGuidFromRoute();
                var result = await _dispatcher.DispatchRead<TReturns>(data);
                if (result is null) context.NoContent(); else context.Ok(result);

            });
            return this;
        }

        public IRestEasyEndpointBuilder GetMany<TReturns>(string path)
        {
            _routeBuilder.MapGet(path, async context =>
            {
                var result = await _dispatcher.DispatchReadMany<TReturns>();
                var enumerable = result.ToList();
                if (!enumerable.Any()) context.NoContent(); else context.Ok(enumerable);
    
            });
            return this;
        }
        
        public IRestEasyEndpointBuilder GetMany<TTakes,TReturns>(string path) where TTakes : class
        {
            _routeBuilder.MapGet(path, async context =>
            {
                var data = await context.ExtractRouteDataAsync<TTakes>();
                var result = await _dispatcher.DispatchReadMany<TTakes, TReturns>(data);
                var enumerable = result.ToList();
                if (!enumerable.Any()) context.NoContent(); else context.Ok(enumerable);
    
            });
            return this;
        }

        public IRestEasyEndpointBuilder Post<TTakes>(string path) where TTakes : class
        {
            _routeBuilder.MapPost(path, async context =>
            {
                var data = await context.Request.ReadAsJsonAsync<TTakes>();
                await _dispatcher.DispatchCreate(data);
                context.Ok();
            });
            return this;
        }

        public IRestEasyEndpointBuilder Put<TTakes>(string path) where TTakes : class
        {
            _routeBuilder.MapPut(path, async context =>
            {
                var data = await context.Request.ReadAsJsonAsync<TTakes>();
                await _dispatcher.DispatchCreate(data);
                context.Ok();
            });
            return this;
        }

        public IRestEasyEndpointBuilder Delete<TTakes>(string path) where TTakes : class
        {
            _routeBuilder.MapDelete(path, async context =>
            {
                var data = await context.ExtractRouteDataAsync<TTakes>();
                await _dispatcher.DispatchDelete(data);
                context.Ok();

            });
            return this;
        }
    }
}