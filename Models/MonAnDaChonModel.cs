using Tuan_7.Helper;

namespace Tuan_7.Models
{
    public class MonDaChonModel : BaseViewModel
    {
        private string _tenMon;
        public string TenMon
        {
            get => _tenMon;
            set { _tenMon = value; OnPropertyChanged(); }
        }

        private int _soLuong;
        public int SoLuong
        {
            get => _soLuong;
            set
            {
                _soLuong = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ThanhTien));
            }
        }

        private decimal _donGia;
        public decimal DonGia
        {
            get => _donGia;
            set
            {
                _donGia = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ThanhTien));
            }
        }

        public decimal ThanhTien => SoLuong * DonGia;
    }
}