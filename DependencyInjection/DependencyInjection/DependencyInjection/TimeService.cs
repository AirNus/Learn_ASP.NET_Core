using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class TimeService
    {
        public string GetTime() => System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
    }
}
