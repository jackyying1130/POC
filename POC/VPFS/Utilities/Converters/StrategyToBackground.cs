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
    class StrategyToBackground : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            string manageapproach = value[1].ToString();
            bool selected = (bool)value[0];

            if (selected)
            {
                if (manageapproach == "CLASSIC")
                {
                    return Brushes.CornflowerBlue;
                }
                else if (manageapproach == "THEMATIC")
                {
                    return Brushes.OrangeRed;
                }
                else 
                {
                    return Brushes.DarkOrchid;
                }
            }

            return Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
