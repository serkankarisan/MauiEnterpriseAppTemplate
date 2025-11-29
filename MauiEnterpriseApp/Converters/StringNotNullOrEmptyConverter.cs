using System.Globalization;

namespace MauiEnterpriseApp.Converters
{
    public class StringNotNullOrEmptyConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string str)
                return !string.IsNullOrWhiteSpace(str);

            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // İki yönlü binding kullanmadığımız için implement etmiyoruz.
            throw new NotImplementedException();
        }
    }
}
