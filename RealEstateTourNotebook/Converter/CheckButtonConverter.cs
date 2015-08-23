using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Windows.UI;

namespace RealEstateTourNotebook.Converter
{
    public class CheckButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isCheck = (bool)value;
            if (isCheck)
                return new SolidColorBrush((System.Windows.Media.Color)Application.Current.Resources["PhoneAccentColor"]);
            else
                return ((SolidColorBrush)Application.Current.Resources["PhoneChromeBrush"]);
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
