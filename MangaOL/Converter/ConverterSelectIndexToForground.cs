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
    public class ConverterSelectIndexToForground : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int.Parse(value.ToString()) == int.Parse(parameter.ToString())) ? Application.Current.Resources["ColorGridHeader"] : Application.Current.Resources["ColorMenuItem"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
