using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Timers;
using System.Diagnostics;

namespace caow.ViewModel
{
    using BaseClass;
    using Model;
    using View;
    class ViewModelClass : ViewModelBase
    {
        ProcessHandler process = new ProcessHandler();
        Timer processListTimer = new Timer(1000);

        private ObservableCollection<Process> processList;
        public ObservableCollection<Process> ProcessList
        {
            get { processList = new ObservableCollection<Process>(process.GetProcesses()); return processList; }
        }

        private void UpdateProcessList(Object source, ElapsedEventArgs e)
        {
            TriggerPropertyChanged(nameof(ProcessList));
            TriggerPropertyChanged(nameof(CurrentCPULoad));
            TriggerPropertyChanged(nameof(CurrentRAMUsage));
            TriggerPropertyChanged(nameof(CPUReadingsHistory));
            TriggerPropertyChanged(nameof(RAMUsageHistory));
        }

        private Process selectedProcess;
        public Process SelectedProcess
        {
            get { return selectedProcess; }
            set
            {
                if (value != null)
                    selectedProcess = value;
            }
        }

        public float CurrentCPULoad
        {
            get { return process.GetCPULoad(); }
        }

        public string CurrentRAMUsage
        {
            get { return process.GetRAMUsage(); }
        }

        public ObservableCollection<int> CPUReadingsHistory
        {
            get { return new ObservableCollection<int>(process.GetCPUReadingHistory()); }
        }

        public ObservableCollection<int> RAMUsageHistory
        {
            get { return new ObservableCollection<int>(process.GetRAMUsageHistory()); }
        }

        private ICommand selectedProcessChanged = null;
        public ICommand SelectedProcessChanged
        {
            get
            {
                if (selectedProcessChanged == null)
                    selectedProcessChanged = new RelayCommand(
                    (arg) => {
                        if(selectedProcess != null)
                            for (int i = 0; i < processList.Count; i++)
                            {
                                if (processList[i].Id == selectedProcess.Id)
                                {
                                    SelectedProcess = processList[i];
                                    TriggerPropertyChanged(nameof(SelectedProcess));
                                    return;
                                }
                            }
                        selectedProcess = null;
                        TriggerPropertyChanged(nameof(SelectedProcess));
                    },
                    (arg) => true);
                return selectedProcessChanged;
            }
        }

        private ICommand killSelectedProcess = null;
        public ICommand KillSelectedProcess
        {
            get
            {
                if (killSelectedProcess == null)
                    killSelectedProcess = new RelayCommand(
                    (arg) => {
                        int err = process.KillProcess(SelectedProcess);
                        if(err != 0)
                            Alerter.ErrorAlert(err);
                    },
                    (arg) => true);
                return killSelectedProcess;
            }
        }

        private ICommand showProcessInfo = null;
        public ICommand ShowProcessInfo
        {
            get
            {
                if (showProcessInfo == null)
                    showProcessInfo = new RelayCommand(
                    (arg) => {
                        if (selectedProcess == null)
                        {
                            Alerter.ErrorAlert(2);
                            return;
                        }
                        var info = new ProcessInfoWindow(new ProcessCounter(selectedProcess));
                        info.ShowDialog();
                    },
                    (arg) => true);
                return showProcessInfo;
            }
        }

        public ViewModelClass()
        {
            processListTimer.Elapsed += UpdateProcessList;
            processListTimer.AutoReset = true;
            processListTimer.Enabled = true;
        }
    }
}
