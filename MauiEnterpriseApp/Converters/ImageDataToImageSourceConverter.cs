using MauiEnterpriseApp.Helpers.Images;
using System.Globalization;

namespace MauiEnterpriseApp.Converters
{
    /// <summary>
    /// object imageData (url/base64/byte[]) -> ImageSource dönüştüren converter.
    /// </summary>
    public class ImageDataToImageSourceConverter : IValueConverter
    {
        public string DefaultImageName { get; set; } = "no_image.png";

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return ImageSourceHelper.GetImageSourceFromData(value, DefaultImageName);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ImageDataToImageSourceConverter ConvertBack desteklemez.");
        }
    }
}
