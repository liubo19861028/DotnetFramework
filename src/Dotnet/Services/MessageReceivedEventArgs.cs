using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Services
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(object message)
        {
            this.Message = message;
        }

        public object Message { get; set; }
    }
}
