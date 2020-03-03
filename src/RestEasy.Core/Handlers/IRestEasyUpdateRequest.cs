using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RestEasy.Core.Handlers
{
    public interface IRestEasyUpdateRequest<in T, TR> where T : class
    {
        Task<TR> HandleAsync(T data);
    }

    public interface IRestEasyUpdateRequest<in T> where T : class
    {
        Task HandleAsync(T data);
    }
}