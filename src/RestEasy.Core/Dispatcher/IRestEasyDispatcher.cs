using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestEasy.Core.Dispatcher
{
    public interface IRestEasyDispatcher
    {
        Task<TR> DispatchRead<T, TR>(T data) where T : class;

        Task<T> DispatchRead<T>(Guid id);

        Task<IEnumerable<T>> DispatchReadMany<T>();

        Task<IEnumerable<TR>> DispatchReadMany<T, TR>(T data) where T : class;
    
        Task DispatchCreate<T>(T data) where T : class;

        Task DispatchDelete<T>(T data) where T : class;
    }
}    