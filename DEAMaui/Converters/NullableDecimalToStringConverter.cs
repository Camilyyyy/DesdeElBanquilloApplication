// EN: Converters/NullableDecimalToStringConverter.cs

using System.Globalization;

namespace DEAMaui.Converters
{
    public class NullableDecimalToStringConverter : IValueConverter
    {
        // De ViewModel (decimal?) a Vista (string)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Si el valor es nulo, devuelve una cadena vacía
            if (value == null)
                return string.Empty;

            // Convierte el decimal a string usando el punto como separador
            return ((decimal)value).ToString(CultureInfo.InvariantCulture);
        }

        // De Vista (string) a ViewModel (decimal?)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value as string;

            // Si el texto está vacío, devuelve null
            if (string.IsNullOrWhiteSpace(strValue))
                return null;

            // Intenta convertir el texto a decimal, permitiendo un punto
            if (decimal.TryParse(strValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }

            // Si no se puede convertir, devuelve null
            return null;
        }
    }
}