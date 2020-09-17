using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Lesson6;

namespace Training.Lesson7
{
    public class PersonMiddleware
    {
        private readonly RequestDelegate _next;
        public Person Person { get; } 
        public PersonMiddleware(RequestDelegate next, IOptions<Person> options)
        {
            this._next = next;
            this.Person = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"<p>Name: {Person?.Name}</p>");
            stringBuilder.Append($"<p>Age: {Person?.Age}</p>");
            stringBuilder.Append("<p>Languages:</p><ul>");
            foreach (var language in Person?.Languages)
            {
                stringBuilder.Append($"<li><p>{language}</p></li>");
            }
            stringBuilder.Append("</ul>");
            stringBuilder.Append($"<p>Company: {Person?.Company.Tittle}</p>");
            await context.Response.WriteAsync($"{stringBuilder.ToString()}");

        }
    }
}
