
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Training
{
    public class AuthenticationMiddleware
    {
        private RequestDelegate _next;
        private string pattern;

        public AuthenticationMiddleware(RequestDelegate next, string pattern)
        {
            this._next = next;
            this.pattern = pattern;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if(token != pattern)
            {
                context.Response.StatusCode = 403;
            }
            else
            {
                await _next.Invoke(context);
            } 
        }
    }
}
