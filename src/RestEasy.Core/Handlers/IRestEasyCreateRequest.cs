using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RestEasy.Core.Handlers
{
    public interface IRestEasyCreateRequest<T> where T : class
    {
        Task HandleAsync(T data);
    }
}