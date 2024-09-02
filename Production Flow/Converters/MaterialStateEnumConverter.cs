using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Production_Flow.Converters
{
    public class MaterialStateEnumConverter : IValueConverter
    {
        public string[] Items => Helpers.EnumHelper.GetDescriptionList(typeof(Types.MaterialStateType));
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Types.MaterialStateType Item)
                return Helpers.EnumHelper.GetDescription(Item);
            return value.ToString()!;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}