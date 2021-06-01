using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace caow.Model
{
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

        private string PrettyPrintProcess(Process process)
        {
            string returnString = process.Id.ToString();
            while (returnString.Length < 6)
                returnString += " ";
            returnString += "| " + process.ProcessName;
            return returnString;
        }
    }
}
