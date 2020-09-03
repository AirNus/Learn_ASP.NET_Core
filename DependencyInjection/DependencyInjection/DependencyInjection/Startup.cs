using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.ServiceImpl;
using DependencyInjection.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DependencyInjection
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        //private IServiceCollection _services;

        public void ConfigureServices(IServiceCollection services)
        {
            //this._services = services;

            /// ���������� ���������� ������ ��� ������������� ������� ��������. 
            /// ������� ��������� ��������� ����� ������� ������ �� �������� �������
            services.AddTransient<IMessageSender>(provider => 
            {
                if (DateTime.Now.Hour >= 12)
                    return new EmailMessageSender();
                else
                    return new SmsMessageSender();
            });

            

            services.AddTimeService();


            services.AddTransient<ICounter, RandomCounter>();
            services.AddSingleton<CounterService>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // �������� ����������� ����� ����� ApplicationServices �� IApplicationBuilder
            IMessageSender messageSender = app.ApplicationServices.GetService<IMessageSender>();

            // �������� ����������� ����� ������� middleware
            //app.UseMiddleware<MessageMiddleware>();

            // �������� Counter ����� Middleware
            //app.UseMiddleware<CounterMiddleware>();

            app.UseMiddleware<TimerMiddleware>();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello! my little friend");
            });
            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/html;charset=utf-8";
            //    await context.Response.WriteAsync($"������� ���� � �����: {timeService?.GetTime()} <p>message: {messageSender.Send()}</p>");
            //});

            //app.Run(async (context) =>
            //{
            //    var fillingPage = new StringBuilder();
            //    fillingPage.Append("<h1> ��� ������� ASP.NET Core ��������� �� ��������� </h1>");
            //    fillingPage.Append("<table>");
            //    fillingPage.Append("<tr><th>��� �������</th><th>Lifetime</th><th>���������� �������</th></tr>");
            //    foreach (var service in _services)
            //    {
            //        fillingPage.Append("<tr>");
            //        fillingPage.Append($"<td>{service.ServiceType}</td>");
            //        fillingPage.Append($"<td>{service.Lifetime}</td>");
            //        fillingPage.Append($"<td>{service.ImplementationType?.FullName}</td>");
            //        fillingPage.Append("</tr>");
            //    }
            //    fillingPage.Append("</table>");
            //    context.Response.ContentType = "text/html;charset=utf-8";
            //    await context.Response.WriteAsync(fillingPage.ToString());
            //});


        }
    }
}
