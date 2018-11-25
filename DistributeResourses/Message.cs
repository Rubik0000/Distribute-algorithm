using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeResourses
{
    class Message : IMessage
    {
        public IProcess Sender { get; private set; }

        public Message(IProcess sender)
        {
            Sender = sender;
        }
    }
}
