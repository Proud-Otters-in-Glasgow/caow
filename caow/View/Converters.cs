using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

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
                    if(RAMUsage != 0)
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
}
