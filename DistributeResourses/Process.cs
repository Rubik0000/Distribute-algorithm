using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DistributeResourses
{
    enum State
    {
        work,
        wait,
        spare
    }

    class Process : IProcess
    {
        static private readonly uint REQUEST_RESOURCE = 0x400 + 0x001;
        static private readonly uint PERMIT_RESOURCE = REQUEST_RESOURCE + 0x001;

        private Queue<IMessage> _queueRequests = new Queue<IMessage>();
        private Thread _thread;
        private List<IProcess> _listProcesses;        
        private Random _random = new Random();
        private Thread _listener;        
        private State _state;
        private int _countWait = -1;
        private object locker = new object();
        private uint startWait;

        public event Action<int> OnWait;
        public event Action<int> OnWork;
        public event Action<int, int> OnWaitChange;
        public event Action<int> OnDoAnother;

        private bool StartWork = false;
        protected int CountWait
        {
            get => _countWait;
            set
            {
                _countWait = value;
                OnWaitChange?.Invoke(Number, _countWait);
            }
        }

        public int Number { get; private set; }
        public uint ListenerId { get; private set; }

        public Process(int number, List<IProcess> proces)
        {
            _listProcesses = proces;
            Number = number;
            _thread = new Thread(() =>
            {                
                _listener = new Thread(Listen);
                _listener.IsBackground = true;
                _listener.Start();
                while (!StartWork)
                    Thread.Sleep(100);
                while (true)
                {
                    Thread.Sleep(_random.Next(10, 40));
                    if (_random.Next() % 2 == 0)
                    {
                        WorkWithRes();
                    }
                    else
                    {
                        DoAnotherWork();
                    }
                }
            });            
            _thread.IsBackground = true;
            _thread.Start();
        }
       
        private void Listen()
        {
            MSG mes;
            User32.PeekMessage(out mes, IntPtr.Zero, 0x400, 0x400, 0x0000);
            ListenerId = Kernel32.GetCurrentWin32ThreadId();
            while (!StartWork)
                Thread.Sleep(100);
            while (true)
            {
                MSG message;
                if (User32.PeekMessage(out message, new IntPtr(-1), 0, 0, 0x0001))
                {
                    if (message.message == PERMIT_RESOURCE)
                    {
                        ++CountWait;
                    }
                    else if (message.message == REQUEST_RESOURCE)
                    {
                        _queueRequests.Enqueue(new Message(message.wParam.ToInt32(), message.time));
                    }
                }                
                if (_queueRequests.Count != 0)
                {
                    if (_state == State.spare)
                    {
                        lock (_queueRequests)
                        {
                            var msg = _queueRequests.Dequeue();                            
                            User32.PostThreadMessage((uint)msg.SenderId, PERMIT_RESOURCE, IntPtr.Zero, IntPtr.Zero);                        
                        }
                    }
                    else if (_state == State.wait)
                    {
                        var msg = _queueRequests.Peek();
                        if (msg.Time < startWait)
                        {
                            if (_random.Next() % 2 == 0)
                                ++startWait;
                            else
                                --startWait;
                        }
                        if (msg.Time < startWait)
                        {
                            _queueRequests.Dequeue();
                            User32.PostThreadMessage((uint)msg.SenderId, PERMIT_RESOURCE, IntPtr.Zero, IntPtr.Zero);
                        }
                    }
                }
                Thread.Sleep(_random.Next(30, 60));
            }
        }        

        private void WorkWithRes()
        {
            CountWait = 0;
            foreach (var proc in _listProcesses)
            {
                if (proc == this)
                    continue;                
                User32.PostThreadMessage(proc.ListenerId, REQUEST_RESOURCE, new IntPtr(ListenerId), IntPtr.Zero);                
            }
            _state = State.wait;
            startWait = (uint)Kernel32.GetTickCount();
            OnWait?.Invoke(Number);
            int m = _listProcesses.Count - 1;
            while (CountWait < m)
            {
                Thread.Sleep(10);
            }
            _state = State.work;
            startWait = uint.MinValue;
            CountWait = -1;

            OnWork?.Invoke(Number);
            Thread.Sleep(_random.Next(2000, 4000));
            _state = State.spare;
        }

        private void DoAnotherWork()
        {
            CountWait = -1;
            _state = State.spare;
            OnDoAnother?.Invoke(Number);
            Thread.Sleep(_random.Next(2000, 4000));
        }

        public uint u;
        public void Start()
        {
            StartWork = true;            
        }

        public void Stop()
        {
            try
            {
                _thread?.Abort();
            }
            catch { }
        }       
    }
}
