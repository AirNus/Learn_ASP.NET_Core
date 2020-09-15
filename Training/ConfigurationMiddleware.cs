using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training
{
    public class ConfigurationMiddleware
    {
        private readonly RequestDelegate _next;

        public IConfiguration AppConfiguration { get; set; }
        
        public ConfigurationMiddleware(RequestDelegate next,IConfiguration config)
        {
            this._next = next;
            this.AppConfiguration = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync($"<h1><font color=\"{AppConfiguration["color:header"]}\">{AppConfiguration["text:header"]}</font></h1><br/><p><font color=\"{AppConfiguration["color:footer"]}\">{AppConfiguration["text:footer"]}</font></p>");

        }
    }
}
