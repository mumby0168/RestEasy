using RestEasy.Core.Handlers;
using RestEasy.Core.Markers;

namespace RestEasy.Core.Factories
{
    public interface IRestEasyApiFactory
    {
        IPostHandler<TDomain, TDto> Resolve<TDomain, TDto>() where TDto : IDto where TDomain : IDomain<TDto>;
    }
}