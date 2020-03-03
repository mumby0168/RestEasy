using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RestEasy.Core.Handlers
{

    public interface IRestEasyDeleteReadRequest<T> where T : class
    {
        Task HandleAsync(T data);
    }
}    