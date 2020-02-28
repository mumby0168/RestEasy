using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RestEasy.Core.Exceptions;
using RestEasy.Core.Factories;
using RestEasy.Core.Handlers;
using RestEasy.Core.Markers;
using RestEasy.Core.Persistence;

namespace RestEasy.Handlers
{
    public class GenericPostHandler<TDomain, TDto> : IPostHandler<TDomain, TDto> where TDomain : IDomain<TDto>, new() where TDto : IDto
    {
        private readonly ILogger<GenericPostHandler<TDomain, TDto>> _logger;
        private readonly IRepository<TDomain, TDto> _repository;

        public GenericPostHandler(ILogger<GenericPostHandler<TDomain, TDto>> logger, IRestEasyRepositoryFactory repositoryFactory)
        {
            _logger = logger;
            _repository = repositoryFactory.ResolveRepository<TDomain, TDto>();
        }
        
        public async Task HandleAsync(TDto dto, HttpContext context)
        {
            if (dto is null)
            {
                context.BadRequest("The dto provided was null");
                return;
            }
            
            var domain = new TDomain();
            domain.Map(dto);

            await _repository.AddAsync(domain);
            context.Ok();
        }
    }
}