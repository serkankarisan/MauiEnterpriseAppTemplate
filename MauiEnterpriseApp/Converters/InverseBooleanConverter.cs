using System.Globalization;

namespace MauiEnterpriseApp.Converters
{
    /// <summary>
    /// bool değerini tersine çevirir.
    /// true -> false, false -> true.
    /// Null veya bool değilse false kabul eder.
    /// </summary>
    public class InverseBooleanConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return !b;
            }

            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return !b;
            }

            return false;
        }
    }
}
