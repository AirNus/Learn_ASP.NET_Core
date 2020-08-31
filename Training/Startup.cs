using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Training
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
            // Используем слой для обработки возникающих ошибок. Его стоит ставить в начало чтобы он корректно отработал
            //app.UseMiddleware<ErrorHandlingMiddleware>();
            // Используем слой для проверки ввода токена
            //app.UseToken("token");

            // Меняем статус проекта на Продакш
            env.EnvironmentName = "Production";

            // Если проект находится в статусе Разработка - показывать возникающие ошибки на странице
            if ( env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Иначе переводить пользователя по указанному пути где эта ошибка будет обрабатываться
            else
            {
                app.UseExceptionHandler("/error");
            }


            // Обработка ошибок в HTTP
            app.UseStatusCodePages();
            // Расширенная версия метода, в котором можно определить сообщения показываемое пользователю
            //app.UseStatusCodePages("text/plain","Error. Status code : {0}");


            // Слой в котором указаны пути и их контент ( класс RoutingMiddleware )
            app.UseMiddleware<RoutingMiddleware>();

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
