using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Timers;

namespace caow.View
{
    /// <summary>
    /// Logika interakcji dla klasy ProcessInfoWindow.xaml
    /// </summary>
    public partial class ProcessInfoWindow : Window
    {
        public static readonly DependencyProperty ObservedProcess = DependencyProperty.Register(nameof(process), typeof(Process), typeof(ProcessInfoWindow), new FrameworkPropertyMetadata(null));
        //public static readonly DependencyProperty ObservedCPULoad = DependencyProperty.Register(nameof(CPULoad), typeof(int), typeof(ProcessInfoWindow), new FrameworkPropertyMetadata(null));
        Timer processListTimer = new Timer(500);
        private PerformanceCounter counter;
        public Process process
        {
            get
            {
                return (Process)GetValue(ObservedProcess);
            }
            set
            {
                SetValue(ObservedProcess, value);
            }
        }
        public float CPULoad
        {
            
            get
            {
                return counter.NextValue();
            }
            set
            {
                //SetValue(ObservedCPULoad, value);
            }
        }
        private void UpdateProcess(Object source, ElapsedEventArgs e)
        {
            ((Process)GetValue(ObservedProcess)).Refresh();
        }
        public ProcessInfoWindow(Process processOfInterest)
        {
            process = processOfInterest;
            counter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
            counter.NextValue();
            counter.NextValue();
            InitializeComponent();
            processListTimer.Elapsed += UpdateProcess;
            processListTimer.AutoReset = true;
            processListTimer.Enabled = true;
        }
    }
}
