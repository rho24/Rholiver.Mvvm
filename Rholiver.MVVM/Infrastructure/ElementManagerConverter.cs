using System;
using System.Globalization;
using System.Windows.Data;
using Rholiver.Mvvm.Navigation;

namespace Rholiver.Mvvm.Infrastructure
{
    public class ElementManagerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var manager = value as IElementManager;

            if(manager == null)
                return value;

            return manager.ElementValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}