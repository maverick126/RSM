using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;

namespace SQSAdmin_WpfCustomControlLibrary
{
    public class DateTimeToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime dateTime = DateTime.MinValue;
            DateTime.TryParse(value.ToString(), out dateTime);
            if (dateTime != DateTime.MinValue)
            {
                if (dateTime == new DateTime(1, 1, 1))
                    return string.Empty;
                else
                {
                    if (dateTime.Hour == 0 && dateTime.Minute == 0)
                        return dateTime.ToString("dd/MM/yyyy");
                    else
                        return dateTime.ToString("dd/MM/yyyy h:mm tt");
                }
            }
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
