using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using SharpSenses;

namespace Hadouken.Converters {
    public class ColorConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null || !(bool)value) {
                return new SolidColorBrush(Colors.Red);
            }
            return new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
