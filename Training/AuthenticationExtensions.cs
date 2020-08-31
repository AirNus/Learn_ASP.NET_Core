using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training
{
    public static class AuthenticationExtensions
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder, string tokenPattern)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>(tokenPattern);
        }
    }
}
