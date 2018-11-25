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
        private Queue<IMessage> _queueRequests = new Queue<IMessage>();
        private Thread _thread;
        private List<IProcess> _listProcesses;        
        private Random _random = new Random();
        private Thread _listener;        
        private State _state;
        private int _countWait = -1;
        private object locker = new object();

        public event Action<int> OnWait;
        public event Action<int> OnWork;
        public event Action<int, int> OnWaitChange;
        public event Action<int> OnDoAnother;

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
        public DateTime startWait { get; private set; }

        public Process(int number, List<IProcess> proces)
        {
            _listProcesses = proces;
            Number = number;            
        }
       
        private void Listen()
        {
            while (true)
            {
                if (_queueRequests.Count != 0)
                {
                    if (_state == State.spare)
                    {
                        lock (_queueRequests)
                        {
                            var msg = _queueRequests.Dequeue();
                            msg.Sender.GivePermission();
                        }
                    }
                    else if (_state == State.wait)
                    {
                        var sender = _queueRequests.Peek().Sender;
                        var t = sender.startWait.Ticks;
                        if (t == startWait.Ticks)
                        {
                            startWait = startWait.AddTicks(_random.Next());                 
                        }
                        else if (t < startWait.Ticks) {
                            _queueRequests.Dequeue().Sender.GivePermission();
                        }
                    }
                }
                Thread.Sleep(_random.Next(30, 60));
                //Thread.Sleep(20);
            }
        }

        private void WorkWithRes()
        {
            CountWait = 0;
            foreach (var proc in _listProcesses)
            {
                if (proc == this)
                    continue;
                proc.SendMessage(new Message(this));
            }
            _state = State.wait;
            startWait = DateTime.Now;
            OnWait?.Invoke(Number);
            int m = _listProcesses.Count - 1;
            while (CountWait < m)
            {
                Thread.Sleep(15);
            }
            _state = State.work;
            startWait = DateTime.MinValue;
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

        public void Start()
        {
            _thread = new Thread(() => 
            {
                _listener = new Thread(Listen);
                _listener.IsBackground = true;
                _listener.Start();
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

        public void Stop()
        {
            try
            {
                _thread?.Abort();
            }
            catch { }
        }

        public void SendMessage(IMessage msg)
        {
            lock (locker)
            {
                _queueRequests.Enqueue(msg);
            }
        }
        
        public void GivePermission()
        {
            lock (locker)
            {                
                ++CountWait;                
            }
        }
    }
}
