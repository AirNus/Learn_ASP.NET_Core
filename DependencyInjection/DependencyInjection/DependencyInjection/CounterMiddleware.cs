using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services
{
    public class CounterMiddleware
    {
        private readonly RequestDelegate _next;
        // Счетчик запросов
        private int i = 0;
        public CounterMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        
        public async Task InvokeAsync(HttpContext context, ICounter counter, CounterService counterService)
        {
            i++;
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync($"Запрос: {i}<p>Counter: {counter.Value}</p><p>Service: {counterService._counter.Value}</p>");
        }
    }
}
