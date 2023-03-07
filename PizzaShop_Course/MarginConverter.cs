using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PizzaShop_Course
{
    public class MarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || !(values[0] is Thickness) || !(values[1] is double))
            {
                return DependencyProperty.UnsetValue;
            }

            var margin = (Thickness)values[0];
            var width = (double)values[1];
            return new Thickness(margin.Left, margin.Top, margin.Right, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
