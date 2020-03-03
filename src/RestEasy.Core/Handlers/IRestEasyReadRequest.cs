using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RestEasy.Core.Handlers
{
    public interface IRestEasyReadRequest<T, TR> where T : class
    {
        Task<TR> HandleAsync(T data);
    }
    
    public interface IRestEasyReadRequest<TR>
    {
        Task<TR> HandleAsync(Guid id);
    }
}