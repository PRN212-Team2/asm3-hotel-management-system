using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace PresentationLayer.Helpers
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InvertedBooleanToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            return !boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
