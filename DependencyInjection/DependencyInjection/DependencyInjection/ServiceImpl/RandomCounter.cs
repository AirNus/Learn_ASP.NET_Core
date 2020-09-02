using DependencyInjection.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.ServiceImpl
{
    public class RandomCounter : ICounter
    {
        static Random random = new Random();
        private int _value; 
        
        public RandomCounter()
        {
            _value = random.Next(0, 10000);
        }
        
        public int Value
        {
            get { return _value; }
        }
    }
}
