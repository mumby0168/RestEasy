using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RestEasy.Core.Dispatcher;
using RestEasy.Core.Exceptions;
using RestEasy.Core.Handlers;

namespace RestEasy.Dispatchers
{
    public class RestEasyDispatcher : IRestEasyDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public RestEasyDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public Task<TR> DispatchRead<T, TR>(T data) where T : class
        {
            var handler = 
            GetService <IRestEasyReadRequest<T, TR>>();
            return handler.HandleAsync(data);
        }

        public Task<T> DispatchRead<T>(Guid id)
        {
            var handler = 
            GetService<IRestEasyReadRequest<T>>();
            return handler.HandleAsync(id);
        }

        public Task<IEnumerable<T>> DispatchReadMany<T>()
        {
            var handler = 
            GetService<IRestEasyReadManyRequest<T>>();
            return handler.HandleAsync();
        }

        public Task<IEnumerable<TR>> DispatchReadMany<T, TR>(T data) where T : class
        {
            var handler = 
            GetService<IRestEasyReadManyRequest<T, TR>>();
            return handler.HandleAsync(data);
        }

        public Task DispatchCreate<T>(T data) where T : class
        {
            var handler = 
            GetService<IRestEasyCreateRequest<T>>();
            return handler.HandleAsync(data);
        }

        public Task DispatchDelete<T>(T data) where T : class
        {
            var handler = 
            GetService<IRestEasyDeleteReadRequest<T>>();
            return handler.HandleAsync(data); 
        }

        private T GetService<T>()
        {
            var service =_serviceProvider.GetService<T>();
            if (service is null)
            {
                throw new RestEasyHandlerNotRegisteredException(typeof(T));
            }

            return service;
        }
    }
}