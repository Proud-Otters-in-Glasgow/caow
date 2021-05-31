using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Timers;

namespace caow.ViewModel
{
    using BaseClass;
    using Model;
    class ViewModelClass : ViewModelBase
    {
        ProcessHandler process = new ProcessHandler();
        Timer processListTimer = new Timer(1000);

        public ObservableCollection<string> ProcessList
        {
            get { return new ObservableCollection<string>(process.GetProcesses()); }
        }

        private void UpdateProcessList(Object source, ElapsedEventArgs e)
        {
            TriggerPropertyChanged(nameof(ProcessList));
        }

        public ViewModelClass()
        {
            processListTimer.Elapsed += UpdateProcessList;
            processListTimer.AutoReset = true;
            processListTimer.Enabled = true;
        }
    }
}
