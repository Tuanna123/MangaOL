using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MangaOL.Converter
{
    public class ConverterBoolToText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "BookmaskChaper":
                        return ((bool)value) ? "Bỏ dấu" : "Đánh dấu";
                    case "DownloadInView":
                        return ((bool)value) ? "Xóa" : "Tải chap";
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
