using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeResourses
{
    interface IProcess
    {
        uint ListenerId { get; }

        void Start();

        void Stop();
    }
}
