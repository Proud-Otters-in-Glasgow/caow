﻿using System;
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

        public ViewModelClass()
        {
            processListTimer.Elapsed += UpdateProcessList;
            processListTimer.AutoReset = true;
            processListTimer.Enabled = true;
        }
    }
}
