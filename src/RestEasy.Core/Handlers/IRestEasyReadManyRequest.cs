using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RestEasy.Core.Handlers
{
    public interface IRestEasyReadManyRequest<TReturns>
    {
        Task<IEnumerable<TReturns>> HandleAsync();
    }
    
    public interface IRestEasyReadManyRequest<in TTakes, TReturns>
    {
        Task<IEnumerable<TReturns>> HandleAsync(TTakes data);
    }
    
}