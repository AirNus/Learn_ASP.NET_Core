using MVC_pattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_pattern
{
    public class SampleData
    {
        public static void Initializze(MobileContext context)
        {
            if(!context.Phones.Any())
            {
                context.Phones.AddRange(
                    new Phone
                    {
                        Name = "Honor 9 Lite",
                        Company = "Huawei",
                        Price = 15000
                    },
                    new Phone
                    {
                        Name = "Iphone 10",
                        Company = "Apple",
                        Price = 87000
                    },
                    new Phone
                    {
                        Name = "Zeon Empress",
                        Company = "Vertex",
                        Price = 3700
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
