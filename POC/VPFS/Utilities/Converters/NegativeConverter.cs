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
    class NegativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input;
            bool parse = false;
            bool negative = false;
            decimal numVal = 0;

            if (value != null)
            {
                input = value.ToString();

                try
                {
                    if (input.StartsWith("(", StringComparison.Ordinal) && input.StartsWith("(", StringComparison.Ordinal))
                    {
                        parse = Decimal.TryParse(input.Replace("(", "").Replace(")", ""), out numVal);
                    }
                }
                catch (Exception ex)
                {
                    // do nothing 
                }
                finally
                {
                    if (parse)
                    {
                        negative = true;
                    }
                }

                if (negative) 
                { 
                    return Brushes.Red; 
                }
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
