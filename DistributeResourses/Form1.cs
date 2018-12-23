using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace DistributeResourses
{
    public partial class Form1 : Form
    {
        static private readonly int MAX_PROC_COUNT = 5;

        List<IProcess> _processes;

        public Form1()
        {
            InitializeComponent();
        }
        
        private TextBox GetPanel(int num)
        {
            switch (num)
            {
                case 0:
                    return txtBxPrc1;            

                case 1:
                    return txtBxPrc2;

                case 2:
                    return txtBxPrc3;

                case 3:
                    return txtBxPrc4;

                case 4:
                    return txtBxPrc5;

                default:
                    return null;                    
            }
        }

        private void Wait(int num)
        {
            Invoke(new Action(() => 
            {
                GetPanel(num).BackColor = Color.Red;
            }));
        }

        private void Work(int num)
        {
            Invoke(new Action(() =>
            {
                GetPanel(num).BackColor = Color.Green;
            }));
        }

        private void CountWait(int num, int countWait)
        {
            Invoke(new Action(() =>
            {
                GetPanel(num).Text = countWait >= 0 ? countWait.ToString() : "";                
            }));
        }

        private void Another(int num)
        {
            Invoke(new Action(() =>
            {
                GetPanel(num).BackColor = Color.White;
            }));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _processes = new List<IProcess>();
            for (int i = 0; i < MAX_PROC_COUNT; ++i)
            {
                var t = new Process(i, _processes);
                _processes.Add(t);

                t.OnWait += Wait;
                t.OnWork += Work;
                t.OnWaitChange += CountWait;
                t.OnDoAnother += Another;
                Thread.Sleep(16);         
            }
            Thread.Sleep(500);
            foreach (var p in _processes)
            {
                p.Start();
            }
            btnStart.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            foreach (var p in _processes)
            {
                p.Stop();
            }
            for (int i = 0; i < MAX_PROC_COUNT; ++i)
            {
                GetPanel(i).BackColor = Color.White;
                GetPanel(i).Clear();
            }
            btnStart.Enabled = true;
        }
    }
}
