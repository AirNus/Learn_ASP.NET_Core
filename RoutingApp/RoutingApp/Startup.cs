using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RoutingApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseRouting();

            //app.Use(async (context, next) =>
            //{
            //    // Получаем конечную точку по которой обращается пользователь
            //    // Конечные точки используются для обработки запросов по определенным маршрутам
            //    // EndpointRoutingMiddleware позволяет определить маршрут, который соотвествует запрошенному адресу,
            //    // и установить для его обработки конечную точку  в виде обьекта Endpoint, а также определить данные маршрута
            //    Endpoint endpoint = context.GetEndpoint();
            //    if(endpoint != null)
            //    {
            //        // Получаем шаблон маршрута, который ассоциирован с конечной точкой
            //        // Объект RouteEndpoint наследник EndPoint и добавляет несколько свойств
            //        // Например RoutePattern.RawText - хранит всю информацию ассоциированную с маршрутом конечной точки
            //        var routePattern = (endpoint as Microsoft.AspNetCore.Routing.RouteEndpoint)?.RoutePattern?.RawText;

            //        Debug.WriteLine($"Endpoint Name: {endpoint.DisplayName}");
            //        Debug.WriteLine($"Route pattern: {routePattern}");

            //        /// Если конечная точка определена, передаем обработку дальше
            //        await next();
            //    }
            //    else
            //    {
            //        Debug.WriteLine("Endpoint is null");
            //        /// Если конечная точка не определена завершаем обработку
            //        await context.Response.WriteAsync("Endpoint is not defined");
            //    }
            //});

            //// Встраивает в конвейер EndpointMiddleware
            //app.UseEndpoints(endpoints =>
            //{
            //    // MapGet добавляет конечную точку для определенного маршрута по запросу GET и ее обработчик
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //    endpoints.MapGet("/ainur", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello, Ainur!!!!");
            //    });
            //});

            //////// Lesson 9

            // Определяем обработчик маршрута
            var myRouteHandler = new RouteHandler(Handle);
            // Создаем маршрут, используя обработчик
            var routeBuilder = new RouteBuilder(app, myRouteHandler);
            // Задаем маршрут - он должен соотвествовать запросу *адрес сайта*/controller/action
            // Запрос не обязательно должен содержать эти слова главное соблюдать шаблон Например: *адрес*/ainur/molodec тоже сработает
            routeBuilder.MapRoute("default", "{controller}/{action}");
            // строим маршрут 
            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("HelloWorld!");
            });

        }

        // Обработка события Lesson 9
        private async Task Handle(HttpContext context)
        {
            await context.Response.WriteAsync("Hello Ainur! My project about ASP.NET");
        }
    }
}
