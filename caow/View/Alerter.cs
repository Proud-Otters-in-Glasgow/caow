using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace caow.View
{
    class Alerter
    {
        private static string[] ErrorMessages = new string[]
        {
            "Brak dostępu!"
        };

        public static void ErrorAlert(int errcode)
        {
            MessageBox.Show(ErrorMessages[errcode-1], "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
