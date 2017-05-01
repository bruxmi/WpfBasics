using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WpfViewModelBasics.UI.Wrapper;

namespace WpfViewModelBasics.UI.Converter
{
    public class ValidationResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var level = (CustomErrorResult.ErrorLevel)value;
            if (level == CustomErrorResult.ErrorLevel.Error)
            {
                return System.Windows.Media.Brushes.Red;
            }
            if (level == CustomErrorResult.ErrorLevel.Warning)
            {
                return System.Windows.Media.Brushes.Orange;
            }
            return System.Windows.Media.Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
