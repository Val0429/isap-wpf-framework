using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Library.Converters {
    public class NullVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var test = (value == null || (value as bool?) == false || (value as string) == "" || (value as int?) == 0);
            var tmpstr = parameter as string;
            var tmpcollapse = false;
            if (tmpstr != null) {
                if (tmpstr.IndexOf("reverse") >= 0) test = !test;
                if (tmpstr.IndexOf("collapse") >= 0) tmpcollapse = true;
            }
            if (parameter as string == "reverse") test = !test;
            return !test ? Visibility.Visible :
                tmpcollapse ? Visibility.Collapsed :
                Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
