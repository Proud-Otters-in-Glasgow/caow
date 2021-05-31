using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace caow.ViewModel
{
    using BaseClass;
    using Model;
    public class ViewModelClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ProcessHandler process = new ProcessHandler();

        public ObservableCollection<string> ProcessList
        {
            get { return new ObservableCollection<string>(process.GetProcesses()); }
        }
    }
}
