using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VPFS.Utilities.Converters
{
    class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string type = parameter.ToString();

            if (type == "yn")
            {
                string input = (string)value;

                return (input == "Y");
            }
            else if (type == "number")
            {
                int input = (int)value;

                return (input == 1);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool input = (bool)value;
            string type = parameter.ToString();

            if (type == "yn")
            {
                if (input)
                {
                    return "Y";
                }
                else
                {
                    return "N";
                }
            }
            else if (type == "number")
            {
                if (input)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }

            return null;
        }
    }
}
