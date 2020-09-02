
using DependencyInjection.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class MessageMiddleware
    {
        private readonly RequestDelegate _next;

        public MessageMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMessageSender messageSender)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync($"Message: {messageSender.Send()}");
        }
    }
}
