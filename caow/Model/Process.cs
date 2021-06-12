using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Timers;

namespace caow.Model
{
    public class ProcessCounter : INotifyPropertyChanged
    {
        private PerformanceCounter counter;
        private Timer refreshTimer;
        private Process process;
        private void updateProcess(Object source, ElapsedEventArgs e)
        {
            process.Refresh();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CPUUsage)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RAMUsage)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Priority)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public float CPUUsage { get { return counter.NextValue(); }}
        public int ID { get { return process.Id; } }
        public long RAMUsage { get { return process.WorkingSet64; } }
        public string Name { get { return process.ProcessName; } }
        public ProcessPriorityClass Priority { get { return process.PriorityClass; } }
        public void Dispose() => counter.Dispose();

        public ProcessCounter(Process p)
        {
            process = p;
            counter = new PerformanceCounter("Process", "% Processor Time", p.ProcessName);
            counter.NextValue();
            refreshTimer = new Timer(1000);
            refreshTimer.Elapsed += updateProcess;
            refreshTimer.AutoReset = true;
            refreshTimer.Enabled = true;
        }
    }
    class ProcessHandler
    {
        private Process[] processList;
        
        public Process[] GetProcesses()
        {
            processList = Process.GetProcesses();
            return processList;
        }

        public int KillProcess(Process process)
        {
            try
            {
                process.Kill();
            }
            catch(Win32Exception)
            {
                return 1;
            }
            return 0;
        }
    }
}
