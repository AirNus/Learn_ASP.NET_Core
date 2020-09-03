using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;        

        public TimerMiddleware(RequestDelegate next)
        {
            this._next = next;            
        }

        public async Task InvokeAsync(HttpContext context, TimeService _timeService)
        {
            if(context.Request.Path.Value.ToLower() == "/time")
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync($ "Текущее время: {_timeService?.Time}");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
