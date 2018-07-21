using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App7.Helpers
{
    public static class Utils
    {
        private static Regex digitsOnly = new Regex(@"[^\d]");

        public static string CleanPhone(this string phone)
        {
            return digitsOnly.Replace(phone, string.Empty);
        }
    }

    public class ConverterUsuario : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ObservableGroupCollection<S, T> : ObservableCollection<T>
    {
        private readonly S _key;

        public ObservableGroupCollection(IGrouping<S, T> group)
            : base(group)
        {
            _key = group.Key;
        }

        public S Key
        {
            get { return _key; }
        }
    }
}
