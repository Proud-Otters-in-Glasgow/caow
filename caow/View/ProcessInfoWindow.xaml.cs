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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Timers;
using caow.Model;

namespace caow.View
{
    /// <summary>
    /// Logika interakcji dla klasy ProcessInfoWindow.xaml
    /// </summary>
    public partial class ProcessInfoWindow : Window
    {
        public static readonly DependencyProperty ObservedProcess = DependencyProperty.Register(nameof(process), typeof(ProcessCounter), typeof(ProcessInfoWindow), new FrameworkPropertyMetadata(null));
        public ProcessCounter process
        {
            get
            {
                return (ProcessCounter)GetValue(ObservedProcess);
            }
            set
            {
                SetValue(ObservedProcess, value);
            }
        }

        public ProcessInfoWindow(ProcessCounter p)
        {
            process = p;
            this.Title = "Szczegóły: " + p.Name + " (" + p.ID + ")";
            InitializeComponent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            process.Dispose();
        }
    }
}
