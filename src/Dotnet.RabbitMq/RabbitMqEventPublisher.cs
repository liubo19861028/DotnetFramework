using Dotnet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.RabbitMq
{
    public class RabbitMqEventPublisher : RabbitMqMessagePublisher, IEventPublisher
    {
        public RabbitMqEventPublisher(string uri, string exchangeName)
            : base(uri, exchangeName)
        { }
    }
}
