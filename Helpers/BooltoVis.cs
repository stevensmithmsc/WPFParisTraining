using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WPFParisTraining.Helpers
{
    class BooltoVis : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility returnValue = Visibility.Collapsed;
            if (value is bool)
            {
                returnValue = (bool)value ? Visibility.Visible : Visibility.Hidden;
            }
            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // not sure I'll ever need this bit!
            bool returnValue = default(bool);
            if (value is Visibility)
            {
                if ((Visibility)value == Visibility.Visible) { returnValue = true; }
                else if ((Visibility)value == Visibility.Hidden) { returnValue = false; }
            }
            return returnValue;

        }
    }
}
