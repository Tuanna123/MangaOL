using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MangaOL.Converter
{
    public class ConverterBoolToIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "Notication":
                        return ((bool)value) ? System.Windows.Application.Current.Resources["BellOutline"] as string : System.Windows.Application.Current.Resources["BellOffOutline"] as string;
                    case "BookmaskChaper":
                        return ((bool)value) ? System.Windows.Application.Current.Resources["File"] as string : System.Windows.Application.Current.Resources["FileOutline"] as string;
                    case "DownloadInView":
                        return ((bool)value) ? System.Windows.Application.Current.Resources["Delete"] as string : System.Windows.Application.Current.Resources["Download"] as string;
                    case "FavoriteManga":
                        return (!(bool)value) ? (System.Windows.Application.Current.Resources["HeartOutline"] as string) : (System.Windows.Application.Current.Resources["Heart"] as string);
                    case "Reading":
                        return (!(bool)value) ? (System.Windows.Application.Current.Resources["Happy"] as string) : (System.Windows.Application.Current.Resources["Eye"] as string);
                    default:
                        return null;
                }
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
