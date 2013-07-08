using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace VPFS.Utilities.Converters
{
    class OrderResultToBackground : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            string bsflag = value[0].ToString();
            string errtype = "";

            if (value[1] != null)
            {
                errtype = value[1].ToString();
            }

            if (errtype == "E")
            {
                return Brushes.Crimson;
            }
            else if (errtype == "C")
            {
                return Brushes.OrangeRed;
            }
            else if (bsflag == "B")
            {
                return Brushes.PowderBlue;
            }
            else if (bsflag == "S")
            {
                return Brushes.LightPink;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
