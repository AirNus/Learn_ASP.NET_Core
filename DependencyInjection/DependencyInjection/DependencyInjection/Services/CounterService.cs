using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services
{
    public class CounterService
    {
        protected internal ICounter _counter { get; }
        public CounterService(ICounter counter)
        {
            this._counter = counter;
        }

    }
}
