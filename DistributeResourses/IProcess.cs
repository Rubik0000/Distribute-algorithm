using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeResourses
{
    interface IProcess
    {
        void SendMessage(IMessage msg);

        void GivePermission();

        void Start();

        void Stop();

        DateTime startWait { get; }
    }
}
