using System;
using Microsoft.Extensions.DependencyInjection;
using RestEasy.Core.Exceptions;
using RestEasy.Core.Factories;
using RestEasy.Core.Handlers;
using RestEasy.Core.Markers;

namespace RestEasy.Factories
{
    public class RestEastApiFactory : IRestEasyApiFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RestEastApiFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public IPostHandler<TDomain, TDto> Resolve<TDomain, TDto>() where TDomain : IDomain<TDto> where TDto : IDto
        {
            var handler = _serviceProvider.GetService<IPostHandler<TDomain, TDto>>();
            if (handler is null)
            {
                throw new RestEasyResolveException();
            }

            return handler;
        }
    }
}