using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebSite.MiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ForeignIPBlocker
    {
        private readonly RequestDelegate _next;

        public ForeignIPBlocker(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            
            await _next(httpContext);
            string ip = httpContext.Connection.RemoteIpAddress.ToString();
            if (!(ip.StartsWith("192.168.") || ip.StartsWith("10.") || ip.StartsWith("172.16.") || ip.StartsWith("172.17.") ||
            ip.StartsWith("172.18.") || ip.StartsWith("172.19.") || ip.StartsWith("172.20.") || ip.StartsWith("172.21.") ||
            ip.StartsWith("172.22.") || ip.StartsWith("172.23.") || ip.StartsWith("172.24.") || ip.StartsWith("172.25.") ||
            ip.StartsWith("172.26.") || ip.StartsWith("172.27.") || ip.StartsWith("172.28.") || ip.StartsWith("172.29.") ||
            ip.StartsWith("172.30.") || ip.StartsWith("172.31.") || ip == "127.0.0.1"))
            {
                httpContext.Response.StatusCode = 403;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ForeignIPBlockerExtensions
    {
        public static IApplicationBuilder UseForeignIPBlocker(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ForeignIPBlocker>();
        }
    }
}
