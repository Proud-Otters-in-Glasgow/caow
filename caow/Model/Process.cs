using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
