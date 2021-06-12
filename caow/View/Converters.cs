using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;
using System.Diagnostics;

namespace caow.View
{
    public class ProcessRAMConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnString;
            long RAMUsage = ((long)value) / 1048576;
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
            {
                if (RAMUsage != 0)
                    returnString = RAMUsage.ToString(".0#") + " MB";
                else
                    returnString = "0 MB";
            }
            return returnString;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PriorityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ProcessPriorityClass p = (ProcessPriorityClass)value;
            switch(p)
            {
                case ProcessPriorityClass.AboveNormal:
                    return "Powyżej normalnego";
                case ProcessPriorityClass.BelowNormal:
                    return "Poniżej normalnego";
                case ProcessPriorityClass.High:
                    return "Wysoki";
                case ProcessPriorityClass.Idle:
                    return "Bezczynność";
                case ProcessPriorityClass.Normal:
                    return "Normalny";
                case ProcessPriorityClass.RealTime:
                    return "Czasu rzeczywistego";
            }
            return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}