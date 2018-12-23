using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeResourses
{
    interface IMessage
    {
        int SenderId { get; }
        uint Time { get; }
    }
}
