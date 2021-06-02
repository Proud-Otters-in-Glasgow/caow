using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.ComponentModel;

namespace caow.Model
{
    class ProcessHandler
    {
        private Process[] processList;
        private PerformanceCounter cpuThing;
        private PerformanceCounter ramThing;
        private float totalRAM;
        private float useRAM;

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

        public float GetCPULoad() => cpuThing.NextValue();

        public string GetRAMUsage()
        {
            float RAMUsage = totalRAM-ramThing.NextValue();
            string returnString = "";
            if(RAMUsage > 1024)
            {
                RAMUsage /= 1024;
                if(RAMUsage > 1024)
                {
                    RAMUsage /= 1024;
                    returnString = RAMUsage.ToString(".0#") + " TB";
                }
                else
                    returnString = RAMUsage.ToString(".0#") + " GB";
            }
            else
                returnString = RAMUsage.ToString(".0#") + " MB";
            return returnString;
        }

        private float GetTotalRAM()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection collection = searcher.Get();
            float ram = 0;
            foreach (ManagementObject pc in collection)
                ram = (float)Convert.ToDouble(pc["TotalVisibleMemorySize"]);
            return ram/1024;
        }

        private string PrettyPrintProcess(Process process)
        {
            string returnString = process.Id.ToString();
            while (returnString.Length < 6)
                returnString += " ";
            returnString += "| " + process.ProcessName;
            return returnString;
        }

        public ProcessHandler()
        {
            cpuThing = new PerformanceCounter();
            cpuThing.CategoryName = "Processor";
            cpuThing.CounterName = "% Processor Time";
            cpuThing.InstanceName = "_Total";
            ramThing = new PerformanceCounter("Memory", "Available MBytes");
            totalRAM = GetTotalRAM();
        }
    }
}
