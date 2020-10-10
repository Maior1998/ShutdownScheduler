using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace ShutdownScheduler.Converters
{
    public class BoolInverterConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is bool casted)
            {
                return !casted;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is bool casted)
            {
                return !casted;
            }
            return value;
        }

        #endregion
    }
}
