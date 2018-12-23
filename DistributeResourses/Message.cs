using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeResourses
{
    class Message : IMessage
    {
        public int SenderId { get; }

        public uint Time { get; }

        public Message(int sender, uint time)
        {
            SenderId = sender;
            Time = time;
        }
    }
}
