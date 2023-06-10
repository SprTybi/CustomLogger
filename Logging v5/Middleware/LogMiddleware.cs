using Logging_v5.Logger.Repositories;
using Logging_v5.Logger.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Logging_v5.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, [FromServices]ILogType logger)
        {
            logger.Log(context.Request);
            
            //var originalBodyStream = context.Response.Body;
            //using var responseBody = new MemoryStream();
            //context.Response.Body = responseBody;
            
            await _next(context);

            //var body = string.Empty;

            //context.Response.Body.Seek(0, SeekOrigin.Begin);
            //using var reader = new StreamReader(context.Response.Body, Encoding.UTF8, true, 1024, true);
            //body = await reader.ReadToEndAsync();

            //context.Response.Body.Seek(0, SeekOrigin.Begin);

            //var log = new StringBuilder();
            //log.AppendLine($"Response: {body}");
            //log.AppendLine($"StatusCode: {context.Response.StatusCode}");
            //log.AppendLine($"Headers: {context.Response.Headers}");

            logger.Log(context.Response);


            //await responseBody.CopyToAsync(originalBodyStream);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
