using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RealEstateTourNotebook.Converter
{
    public class CompareVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                return String.IsNullOrEmpty(value.ToString()) ? Visibility.Collapsed : Visibility.Visible;
            }
            else if (value is int || value is double)
            {
                int number = 0;
                int.TryParse(value.ToString(), out number);
                return number == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            else if (value is IEnumerable<object>)
            {
                IEnumerable<object> listTemp = value as IEnumerable<object>;
                return (listTemp == null || listTemp.Count() == 0) ? Visibility.Collapsed : Visibility.Visible;
            }
            else if (value is byte[])
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
