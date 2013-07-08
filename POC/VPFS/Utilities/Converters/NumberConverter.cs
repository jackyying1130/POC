using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace VPFS.Utilities.Converters
{
    class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input;
            bool parse = false;
            decimal numVal = 0;

            if (value != null)
            {
                input = value.ToString();

                try
                {
                    parse = Decimal.TryParse(input.Replace("(", "").Replace(")", ""), out numVal);
                }
                catch (Exception ex)
                {
                    // do nothing 
                }

                if (parse) 
                { 
                    return "Right"; 
                }
            }
            return "Left";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
