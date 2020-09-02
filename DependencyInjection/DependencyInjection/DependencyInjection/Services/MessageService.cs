﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services
{
    public class MessageService : IMessageSender
    {
        private IMessageSender _sender;
        public MessageService(IMessageSender sender)
        {
            this._sender = sender;
        }
        public string Send()
        {
            return _sender.Send();
        }
    }
}
