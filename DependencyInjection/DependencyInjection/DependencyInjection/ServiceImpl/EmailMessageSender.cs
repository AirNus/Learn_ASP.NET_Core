using DependencyInjection.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.ServiceImpl
{
    public class EmailMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Sent by email";
        }
    }
}
