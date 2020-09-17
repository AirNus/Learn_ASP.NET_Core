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
                //Подключаем JSON
                .AddJsonFile("//Lesson6//person.json");
            AppConfiguration = builder.Build();

            //// Считываем конфигурацию из текстового файла при помощи собственных классов Lesson5
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(System.IO.Directory.GetCurrentDirectory() + "//Lesson5");
            //builder.AddTextFile("config.txt");
            //AppConfiguration = builder.Build();

            ///
            //// Считываем стандартные конфигурации и конфигурации из JSON
            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("TrainyConf.json")
            //    .AddConfiguration(config);

            //AppConfiguration = builder.Build();
        }
        public IConfiguration AppConfiguration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Lesson7 Добавляем зависимость для класса Person
            services.Configure<Person>(AppConfiguration);
            // При помощи метода переопределяем одно из заданных в JSON файле параметре
            services.Configure<Person>(option => option.Name = "Zimbambe");

            //// Добавляем конфигурацию в контейнер для DI Lesson5
            //services.AddTransient<IConfiguration>(provider => AppConfiguration);
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
            // Расширенная версия метода, в кот9ором можно определить сообщения показываемое пользователю
            //app.UseStatusCodePages("text/plain","Error. Status code : {0}");


            // Слой в котором указаны пути и их контент ( класс RoutingMiddleware )
            // app.UseMiddleware<RoutingMiddleware>();

            // Метод который ищет файлы с названием index\default  и выставляет их как корневые при запуске страницы. Можно поменять
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
