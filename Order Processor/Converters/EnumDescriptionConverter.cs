﻿using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Order_Processor.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Helpers.EnumHelper.GetDescription(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
