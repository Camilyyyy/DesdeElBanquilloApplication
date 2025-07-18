using System.Globalization;

namespace DEAMaui.Converters
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int i && i == 0)
                return string.Empty;
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value as string))
                return 0;
            if (int.TryParse(value as string, out int result))
                return result;
            return 0;
        }
    }
}