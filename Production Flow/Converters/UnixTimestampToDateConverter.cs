using System.Globalization;
using System.Windows.Data;

namespace Production_Flow.Converters
{
    public class UnixTimestampToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            DateTimeOffset.FromUnixTimeSeconds((long)value).Date.ToString("dd/MM/yyyy");
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}