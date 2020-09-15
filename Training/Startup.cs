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
            // Добавляем конфигурацию в контейнер для DI
            services.AddTransient<IConfiguration>(provider => AppConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Используем слой для обработки возникающих ошибок. Его стоит ставить в начало чтобы он корректно отработал
            //app.UseMiddleware<ErrorHandlingMiddleware>();
            // Используем слой для проверки ввода токена и передаем в него шаблон, которому токен должен соотвествовать
            //app.UseToken("token");

            // Меняем статус проекта на Продакш
            // env.EnvironmentName = "Production";

            // Переадрисация на https адреса в случае обращение по http
            app.UseHttpsRedirection();

            // Если проект находится в статусе Разработка - показывать возникающие ошибки на странице
            if ( env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Иначе переводить пользователя по указанному пути где эта ошибка будет обрабатываться
            else
            {
                // Данные метод отправляет с сервера заголовок, говорящий что к этому сайту нужно обращаться по https, избегая переадресации
                app.UseHsts();

                app.UseExceptionHandler("/error");
            }
            
            // Отправили конфигурацию в контейнер и используем через DependencyInjection
            //app.UseMiddleware<ConfigurationMiddleware>();
           
            // Считываем данные-конфигурацию с JSON файла
           // var headerColor = AppConfiguration["color:header"];
           // var footerColor = AppConfiguration["color:footer"];
           // var header = AppConfiguration["text:header"];
           // var footer = AppConfiguration["text:footer"];
           // app.Run( async (context) =>
           //{
           //    await context.Response.WriteAsync($"<h1><font color=\"{headerColor}\">{header}</font></h1><br/><p><font color=\"{footerColor}\">{footer}</font></p>");
           //});

            // Обработка ошибок в HTTP
            app.UseStatusCodePages();
            // Расширенная версия метода, в котором можно определить сообщения показываемое пользователю
            //app.UseStatusCodePages("text/plain","Error. Status code : {0}");


            // Слой в котором указаны пути и их контент ( класс RoutingMiddleware )
            // app.UseMiddleware<RoutingMiddleware>();

            // Метод который ищет файлы с названием index\default  и выставляет их как корневые при запуске страницы. Можно поменять
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
