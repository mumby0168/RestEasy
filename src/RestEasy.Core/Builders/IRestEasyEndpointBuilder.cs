using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Routing;

namespace RestEasy.Core.Builders
{
    public interface IRestEasyEndpointBuilder
    {
        void Initialize(IEndpointRouteBuilder builder);
        IRestEasyEndpointBuilder Get<TReturns, TTakes>(string path) where TReturns : class where TTakes : class;

        IRestEasyEndpointBuilder Get<TReturns>(string path);

        IRestEasyEndpointBuilder GetMany<TReturns>(string path);

        IRestEasyEndpointBuilder GetMany<TTakes, TReturns>(string path) where TTakes : class;

        IRestEasyEndpointBuilder Post<TTakes>(string path) where TTakes : class;

        IRestEasyEndpointBuilder Put<TTakes>(string path) where TTakes : class;
        
        IRestEasyEndpointBuilder Delete<TTakes>(string path) where TTakes : class;
    }
}