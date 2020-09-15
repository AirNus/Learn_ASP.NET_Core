using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Training
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940  
        public Startup(IConfiguration config)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("TrainyConf.json")
                .AddConfiguration(config);

            AppConfiguration = builder.Build();
        }
        public IConfiguration AppConfiguration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // ��������� ������������ � ��������� ��� DI
            services.AddTransient<IConfiguration>(provider => AppConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ���������� ���� ��� ��������� ����������� ������. ��� ����� ������� � ������ ����� �� ��������� ���������
            //app.UseMiddleware<ErrorHandlingMiddleware>();
            // ���������� ���� ��� �������� ����� ������ � �������� � ���� ������, �������� ����� ������ ��������������
            //app.UseToken("token");

            // ������ ������ ������� �� �������
            // env.EnvironmentName = "Production";

            // ������������� �� https ������ � ������ ��������� �� http
            app.UseHttpsRedirection();

            // ���� ������ ��������� � ������� ���������� - ���������� ����������� ������ �� ��������
            if ( env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //����� ���������� ������������ �� ���������� ���� ��� ��� ������ ����� ��������������
            else
            {
                // ������ ����� ���������� � ������� ���������, ��������� ��� � ����� ����� ����� ���������� �� https, ������� �������������
                app.UseHsts();

                app.UseExceptionHandler("/error");
            }
            
            // ��������� ������������ � ��������� � ���������� ����� DependencyInjection
            //app.UseMiddleware<ConfigurationMiddleware>();
           
            // ��������� ������-������������ � JSON �����
           // var headerColor = AppConfiguration["color:header"];
           // var footerColor = AppConfiguration["color:footer"];
           // var header = AppConfiguration["text:header"];
           // var footer = AppConfiguration["text:footer"];
           // app.Run( async (context) =>
           //{
           //    await context.Response.WriteAsync($"<h1><font color=\"{headerColor}\">{header}</font></h1><br/><p><font color=\"{footerColor}\">{footer}</font></p>");
           //});

            // ��������� ������ � HTTP
            app.UseStatusCodePages();
            // ����������� ������ ������, � ������� ����� ���������� ��������� ������������ ������������
            //app.UseStatusCodePages("text/plain","Error. Status code : {0}");


            // ���� � ������� ������� ���� � �� ������� ( ����� RoutingMiddleware )
            // app.UseMiddleware<RoutingMiddleware>();

            // ����� ������� ���� ����� � ��������� index\default  � ���������� �� ��� �������� ��� ������� ��������. ����� ��������
            app.UseFileServer();

            //app.Run(async (context) =>
            //{
            //    int y = 0;
            //    int x = 8 / y;
            //    await context.Response.WriteAsync($"Rezult: {x}");
            //});
        }
    }
}
