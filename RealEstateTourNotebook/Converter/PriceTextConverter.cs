using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RealEstateTourNotebook.Converter
{
    public class PriceTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string price = value.ToString();
            if (int.Parse(price) == 0)
                return "0"; 
            string result = string.Empty;
            int j = 0;
            for (int i = price.Length - 1; i >= 0; i--)
            {
                if (j == 3)
                {
                    result = result.Insert(0, price[i].ToString() + " ");
                    j = 0;
                }
                else
                    result = result.Insert(0,price[i].ToString());
                j++;
            }
            
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string price = value.ToString();
            if (String.IsNullOrEmpty(price))
            {
                return 0; 
            }
            return int.Parse(price.Replace(" ", ""), System.Globalization.CultureInfo.InvariantCulture); 
        }
    }
}
