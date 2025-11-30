namespace MauiEnterpriseApp.Models.Profile
{
    public class UserProfile
    {
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }

        public string? Department { get; set; }
        public string? Position { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? JoinedAt { get; set; }

        /// <summary>
        /// Profil fotoğrafı için tek alan.
        /// - string (URL veya Base64)
        /// - byte[] (ham image)
        /// olabilir.
        /// </summary>
        public object? ProfileImageData { get; set; }
    }
}
