using System.ComponentModel;

namespace MerchPlusMAUI.Models
{
    public class DisplayPhotoItem : INotifyPropertyChanged
    {
        public int PromotionTypeId { get; set; }
        public string PromotionType { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public byte[] PhotoBytes { get; set; } = Array.Empty<byte>();
        
        private ImageSource _photoSource;
        public ImageSource PhotoSource 
        { 
            get => _photoSource;
            set
            {
                _photoSource = value;
                OnPropertyChanged(nameof(PhotoSource));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
