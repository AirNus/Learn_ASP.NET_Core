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
using Training.Lesson6;
using Training.Lesson7;

namespace Training
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940  
        public Startup(IConfiguration config)
        {
            var builder = new ConfigurationBuilder()
                //.AddXmlFile("//Lesson6//person.xml");
                //���������� JSON
                .AddJsonFile("//Lesson6//person.json");
            AppConfiguration = builder.Build();

            //// ��������� ������������ �� ���������� ����� ��� ������ ����������� ������� Lesson5
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(System.IO.Directory.GetCurrentDirectory() + "//Lesson5");
            //builder.AddTextFile("config.txt");
            //AppConfiguration = builder.Build();

            ///
            //// ��������� ����������� ������������ � ������������ �� JSON
            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("TrainyConf.json")
            //    .AddConfiguration(config);

            //AppConfiguration = builder.Build();
        }
        public IConfiguration AppConfiguration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Lesson7 ��������� ����������� ��� ������ Person
            services.Configure<Person>(AppConfiguration);
            // ��� ������ ������ �������������� ���� �� �������� � JSON ����� ���������
            services.Configure<Person>(option => option.Name = "Zimbambe");

            //// ��������� ������������ � ��������� ��� DI Lesson5
            //services.AddTransient<IConfiguration>(provider => AppConfiguration);
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
            // ����������� ������ ������, � ���9���� ����� ���������� ��������� ������������ ������������
            //app.UseStatusCodePages("text/plain","Error. Status code : {0}");


            // ���� � ������� ������� ���� � �� ������� ( ����� RoutingMiddleware )
            // app.UseMiddleware<RoutingMiddleware>();

            // ����� ������� ���� ����� � ��������� index\default  � ���������� �� ��� �������� ��� ������� ��������. ����� ��������
            //app.UseFileServer();

            ////Lesson 5
            //var color = AppConfiguration["color"];
            //var text = AppConfiguration["text"];
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"<h1 style='color:{color};'>{text}</h1>");
            //});

            // Lesson 6
            //var Ainur = new Person();
            //AppConfiguration.Bind(Ainur);
            //app.Run(async (context) =>
            //{
            //    string name = $"<p>Name: {Ainur.Name}</p>";
            //    string age = $"<p>Age: {Ainur.Age}</p>";
            //    string languageList = "<p>Languages:</p><ul>";
            //    foreach(var language in Ainur.Languages)
            //    {
            //        languageList += $"<li><p>{language}</p></li>";
            //    }
            //    languageList += "</ul>";
            //    string company = $"<p>Company: {AppConfiguration.GetSection("company").Get<Company>().Tittle}</p>";
            //    await context.Response.WriteAsync($"{name}{age}{languageList}{company}");                    
            //});

            // Lesson 7 
            app.UseMiddleware<PersonMiddleware>();
        }
    }
}
