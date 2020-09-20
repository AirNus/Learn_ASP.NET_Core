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
            //    // �������� �������� ����� �� ������� ���������� ������������
            //    // �������� ����� ������������ ��� ��������� �������� �� ������������ ���������
            //    // EndpointRoutingMiddleware ��������� ���������� �������, ������� ������������ ������������ ������,
            //    // � ���������� ��� ��� ��������� �������� �����  � ���� ������� Endpoint, � ����� ���������� ������ ��������
            //    Endpoint endpoint = context.GetEndpoint();
            //    if(endpoint != null)
            //    {
            //        // �������� ������ ��������, ������� ������������ � �������� ������
            //        // ������ RouteEndpoint ��������� EndPoint � ��������� ��������� �������
            //        // �������� RoutePattern.RawText - ������ ��� ���������� ��������������� � ��������� �������� �����
            //        var routePattern = (endpoint as Microsoft.AspNetCore.Routing.RouteEndpoint)?.RoutePattern?.RawText;

            //        Debug.WriteLine($"Endpoint Name: {endpoint.DisplayName}");
            //        Debug.WriteLine($"Route pattern: {routePattern}");

            //        /// ���� �������� ����� ����������, �������� ��������� ������
            //        await next();
            //    }
            //    else
            //    {
            //        Debug.WriteLine("Endpoint is null");
            //        /// ���� �������� ����� �� ���������� ��������� ���������
            //        await context.Response.WriteAsync("Endpoint is not defined");
            //    }
            //});

            //// ���������� � �������� EndpointMiddleware
            //app.UseEndpoints(endpoints =>
            //{
            //    // MapGet ��������� �������� ����� ��� ������������� �������� �� ������� GET � �� ����������
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

            // ���������� ���������� ��������
            var myRouteHandler = new RouteHandler(Handle);
            // ������� �������, ��������� ����������
            var routeBuilder = new RouteBuilder(app, myRouteHandler);
            // ������ ������� - �� ������ �������������� ������� *����� �����*/controller/action
            // ������ �� ����������� ������ ��������� ��� ����� ������� ��������� ������ ��������: *�����*/ainur/molodec ���� ���������
            routeBuilder.MapRoute("default", "{controller}/{action}");
            // ������ ������� 
            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("HelloWorld!");
            });

        }

        // ��������� ������� Lesson 9
        private async Task Handle(HttpContext context)
        {
            await context.Response.WriteAsync("Hello Ainur! My project about ASP.NET");
        }
    }
}
