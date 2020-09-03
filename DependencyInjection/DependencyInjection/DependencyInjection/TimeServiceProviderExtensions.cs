using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public static class TimeServiceProviderExtensions
    {
        public static void AddTimeService(this IServiceCollection services)
        {
            services.AddTransient<TimeService>();
        }
    }
}
