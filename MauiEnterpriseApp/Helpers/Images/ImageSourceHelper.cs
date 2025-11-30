namespace MauiEnterpriseApp.Helpers.Images
{
    /// <summary>
    /// Url, Base64 string veya byte[] değerlerinden ImageSource üreten genel yardımcı.
    /// Tüm uygulamada tekrar kullanılabilir.
    /// </summary>
    public static class ImageSourceHelper
    {
        /// <summary>
        /// Verilen imageData değerini (url/base64/byte[]) ImageSource'a çevirir.
        /// Eğer değer çözülemezse defaultImageName kullanılır (ör: "default_profile.png").
        /// </summary>
        public static ImageSource GetImageSourceFromData(object? imageData, string defaultImageName)
        {
            // 0) Hiç yoksa direkt default
            if (imageData is null)
                return ImageSource.FromFile(defaultImageName);

            // 1) byte[]
            if (imageData is byte[] bytes && bytes.Length > 0)
            {
                return ImageSource.FromStream(() => new MemoryStream(bytes));
            }

            // 2) string (URL veya Base64 olabilir)
            if (imageData is string s && !string.IsNullOrWhiteSpace(s))
            {
                s = s.Trim();

                // Önce URL mi bak
                if (IsValidUrl(s))
                {
                    try
                    {
                        return ImageSource.FromUri(new Uri(s));
                    }
                    catch
                    {
                        // URL bozuksa Base64 denemeye geçeceğiz
                    }
                }

                // Base64 gibi görünüyor mu?
                if (LooksLikeBase64(s))
                {
                    try
                    {
                        var decoded = Convert.FromBase64String(s);
                        if (decoded.Length > 0)
                        {
                            return ImageSource.FromStream(() => new MemoryStream(decoded));
                        }
                    }
                    catch
                    {
                        // Base64 parse gagaladı → default'a düş
                    }
                }
            }

            // Hiçbiri değilse default görsel
            return ImageSource.FromFile(defaultImageName);
        }

        private static bool IsValidUrl(string value)
        {
            if (!Uri.TryCreate(value, UriKind.Absolute, out var uri))
                return false;

            return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
        }

        private static bool LooksLikeBase64(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim();

            // Uzunluk 4'ün katı değilse çoğunlukla Base64 değil
            if (value.Length % 4 != 0)
                return false;

            // Sadece geçerli Base64 karakterleri içeriyor mu?
            foreach (var c in value)
            {
                if (!(char.IsLetterOrDigit(c) || c == '+' || c == '/' || c == '=' ||
                      c == '\r' || c == '\n'))
                {
                    return false;
                }
            }

            try
            {
                _ = Convert.FromBase64String(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
