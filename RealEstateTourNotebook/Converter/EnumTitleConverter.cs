using RealEstateTourNotebook.ViewModel;
using RealEstateTourNotebook.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Windows.UI;

namespace RealEstateTourNotebook.Converter
{
    public class EnumTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is ObservableCollection<EstateType>)
            {
                List<string> EnumList = new List<string>();
                foreach (Enum item in value as ObservableCollection<EstateType>)
                {
                    EnumList.Add(Utils.Utility.Display((Enum)item));
                }
                return EnumList;
            }
            else if (value is ObservableCollection<AppType>)
            {
                List<string> EnumList = new List<string>();
                foreach (Enum item in value as ObservableCollection<AppType>)
                {
                    EnumList.Add(Utils.Utility.Display((Enum)item));
                }
                return EnumList;
            }
            else if (value is ObservableCollection<Language>)
            {
                List<string> EnumList = new List<string>();
                foreach (Enum item in value as ObservableCollection<Language>)
                {
                    EnumList.Add(Utils.Utility.Display((Enum)item));
                }
                return EnumList;
            }
            else if (value is Mode)
            {
                return Utils.Utility.Display((Enum)value).ToUpper();
            }
            else
            {
                if(parameter==null)
                    return Utils.Utility.Display((Enum)value);
                else
                    return Utils.Utility.Display((Enum)value).ToUpper();
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
