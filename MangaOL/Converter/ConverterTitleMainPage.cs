using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MangaOL.Converter
{
    public class ConverterTitleMainPage : IValueConverter
    {
        Dictionary<int, string> dicTitle = new Dictionary<int, string>()
        {
            {0,MangaCore.Comon.AppName},
            {1,"Truyện mới đọc"},
            {2,"Truyện yêu thích"},
            {3,"Chap đánh dấu"},
            {4,"Tải về"},
        };
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return dicTitle.FirstOrDefault(t => t.Key == int.Parse(value.ToString())).Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
