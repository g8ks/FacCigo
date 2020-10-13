using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace FacCigo.Converters
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal money = (decimal)value;

            return string.Format(new CultureInfo("fr-CD", false), "{0:C} {1}", money, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string money = (string)value;
            bool convt = decimal.TryParse(money, NumberStyles.Currency, new CultureInfo("fr-CD", false)
                .NumberFormat, out decimal back);
            return back;
        }
    }
}
