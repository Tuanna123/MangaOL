using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MangaOL.Converter
{
    public class ConverterItemsCountToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string t = (value.ToString() == "0") ?"Collapsed" : "Visibility";
            System.Diagnostics.Debug.WriteLine(value.ToString() +" - "+t);

            return (value.ToString() == "0") ? Visibility.Collapsed : Visibility.Visible;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
