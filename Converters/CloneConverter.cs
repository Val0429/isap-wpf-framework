using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Library.Converters {
    public class CloneConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
            //((Resource)value).Clone
            //Color c = (Color)value;
            //var l = 0.2126 * c.ScR + 0.7152 * c.ScG + 0.0722 * c.ScB;
            //return l < 0.5 ? ColorConverter.ConvertFromString("White") : ColorConverter.ConvertFromString("Black");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
