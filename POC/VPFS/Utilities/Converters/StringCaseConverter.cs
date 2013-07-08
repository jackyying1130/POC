using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VPFS.Utilities.Converters
{
    class StringCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = (string)value;
            string type = parameter.ToString();

            if (type == "upper")
            {
                return input.ToUpper();
            }
            else if (type == "lower")
            {
                return input.ToLower();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string input = (string)value;
            string type = parameter.ToString();

            if (type == "upper")
            {
                return input.ToUpper();
            }
            else if (type == "lower")
            {
                return input.ToLower();
            }

            return input;
             
        }
    }
}
