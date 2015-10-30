using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MangaOL.Converter
{
   public class ConverterIndexPivotToEnbale : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null)
                return true;
            else
            {
                if (parameter.ToString().Contains("|"))
                {
                    var arrParam = parameter.ToString().Split('|');
                    for (int i = 0; i < arrParam.Length; i++)
                    {
                        if (arrParam[i].ToString()==value.ToString())
                        {
                            return true;
                        }
                    }
                    return false;
                }
                else
                {
                    return value.ToString() == parameter.ToString();
                }
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
