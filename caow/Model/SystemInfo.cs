using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;

namespace caow.Model
{
    class SystemInfo
    {
        private float totalRAM;
        private PerformanceCounter cpuThing;
        private PerformanceCounter ramThing;
        private List<int> cpuReadings;
        private List<int> ramReadings;

        public float GetCPULoad()
        {
            float x = cpuThing.NextValue();
            cpuReadings.Add(Convert.ToInt32(x));
            cpuReadings.RemoveAt(0);
            return x;
        }

        public int[] GetCPUReadingHistory() => cpuReadings.ToArray();

        public int[] GetRAMUsageHistory() => ramReadings.ToArray();

        public string GetRAMUsage()
        {
            float RAMUsage = totalRAM - ramThing.NextValue();
            ramReadings.Add(Convert.ToInt32((RAMUsage / totalRAM) * 100.0));
            ramReadings.RemoveAt(0);
            string returnString = "";
            if (RAMUsage > 1024)
            {
                RAMUsage /= 1024;
                if (RAMUsage > 1024)
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
            return ram / 1024;
        }

        public SystemInfo()
        {
            cpuThing = new PerformanceCounter();
            cpuThing.CategoryName = "Processor";
            cpuThing.CounterName = "% Processor Time";
            cpuThing.InstanceName = "_Total";
            ramThing = new PerformanceCounter("Memory", "Available MBytes");
            totalRAM = GetTotalRAM();
            cpuReadings = new List<int>();
            ramReadings = new List<int>();
            for (int i = 0; i < 30; i++)
            {
                cpuReadings.Add(0);
                ramReadings.Add(0);
            }
        }
    }
}
