using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestEasy.Core.Handlers;
using RestEasy.Core.Markers;
using RestEasy.Core.Persistence;

namespace RestEasy.Handlers
{
    public class GenericGetHandler<TDomain, TDto> : IGetHandler<TDomain, TDto> where TDomain : IDomain<TDto>, new() where TDto : IDto, new()
    {
        private readonly IRepository<TDomain, TDto> _repository;

        public GenericGetHandler(IRepository<TDomain, TDto> repository)
        {
            _repository = repository;
        }
        
        public async Task<TDto> GetAsync(Guid id)
        {
            var domain = await _repository.GetAsync(id);
            if (domain is null)
            {
                throw new Exception();
            }
            var dto = domain.Map();
            return dto;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var domains = await _repository.GetAllAsync();

            return domains.Select(domain => domain.Map()).ToList();
        }
    }
}