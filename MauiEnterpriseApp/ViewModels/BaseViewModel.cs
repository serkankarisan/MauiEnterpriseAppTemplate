using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiEnterpriseApp.ViewModels
{
    // Tüm ViewModel’lerin türeyeceği temel sınıf
    public partial class BaseViewModel : ObservableObject
    {
        // Sayfa başlığı vb. için
        [ObservableProperty]
        private string title;

        // Genel busy durumu (API çağrısı vs.)
        [ObservableProperty]
        private bool isBusy;

        // Hata mesajı göstermek için
        [ObservableProperty]
        private string errorMessage;

        // XAML tarafında sık kullanılan ters mantık
        public bool IsNotBusy => !IsBusy;

        // IsBusy değiştiğinde IsNotBusy için de PropertyChanged tetikleyelim
        partial void OnIsBusyChanged(bool value)
        {
            OnPropertyChanged(nameof(IsNotBusy));
        }
    }
}
