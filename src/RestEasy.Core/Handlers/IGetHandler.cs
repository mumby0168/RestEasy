using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEasy.Core.Markers;

namespace RestEasy.Core.Handlers
{
    public interface IGetHandler<TDomain, TDto> where TDomain : IDomain<TDto> where TDto : IDto
    {
        Task<TDto> GetAsync(Guid id);
        
        Task<IEnumerable<TDto>> GetAllAsync();
    }
}