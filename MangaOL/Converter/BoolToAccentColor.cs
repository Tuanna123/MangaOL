using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MangaOL.Converter
{
    public class BoolToAccentColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter !=null)
            {
                switch (parameter.ToString())
                {
                    case "BackgroudChaper":
                        return ((bool)value) ? Application.Current.Resources["ItemAppBar"] : Application.Current.Resources["BackgroundItem"];
                    default:
                        break;
                }
            }
            return ((bool)value) ? Application.Current.Resources["ColorGridHeader"] : Application.Current.Resources["ForegroundItem"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

       
    }
}
