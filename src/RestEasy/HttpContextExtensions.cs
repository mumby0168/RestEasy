using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RestEasy
{
    public static class HttpContextExtensions
    {
        public static async void Ok(this HttpContext context, string message = null)
        {
            context.Response.StatusCode = (int) HttpStatusCode.OK;
            if (message != null)
            {
                await context.Response.WriteAsync(message);
            }
        }

        public static async void Ok<T>(this HttpContext context, T obj)
        {
            context.Response.StatusCode = (int) HttpStatusCode.OK;
            var result = JsonSerializer.Serialize(obj);
            await context.Response.WriteAsync(result);
        }
        
        public static async void BadRequest(this HttpContext context, string message = null)
        {
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            if (message != null)
            {
                await context.Response.WriteAsync(message);
            }
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpRequest request)
        {
            if (request.Body is null)
            {
                throw new Exception();
            }

            using var reader = new StreamReader(request.Body);
            
            var str = await reader.ReadToEndAsync();
            
            
            return JsonConvert.DeserializeObject<T>(str);
        }

        public static Guid ExtractGuidFromRoute(this HttpRequest request)
        {
            var routeData = request.HttpContext.GetRouteData();
            if (routeData.Values.Keys.Contains("id"))
            {
                var guidString = routeData.Values["id"];
                if (Guid.TryParse(guidString.ToString(), out Guid result))
                {
                    return result;
                }
                else
                {
                    //Need to replace with an exception handled by middleware,
                    throw new Exception();
                }
            }
            else
            {
                //Need to replace with exception handled by middleware.
                throw new Exception();
            }
        }


    }
}