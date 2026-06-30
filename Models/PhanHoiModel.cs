using System;
using Tuan_7.Helper;

namespace Tuan_7.Models
{
    public class PhanHoiModel : BaseViewModel
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _tenKhachHang;
        public string TenKhachHang
        {
            get => _tenKhachHang;
            set
            {
                _tenKhachHang = value;
                OnPropertyChanged();
            }
        }

        private string _soDienThoai;
        public string SoDienThoai
        {
            get => _soDienThoai;
            set
            {
                _soDienThoai = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _ngay;
        public string Ngay
        {
            get => _ngay;
            set
            {
                _ngay = value;
                OnPropertyChanged();
            }
        }

        private DateTime _ngayRaw;
        public DateTime NgayRaw
        {
            get => _ngayRaw;
            set
            {
                _ngayRaw = value;
                OnPropertyChanged();
            }
        }

        private string _mucDoHaiLong;
        public string MucDoHaiLong
        {
            get => _mucDoHaiLong;
            set
            {
                _mucDoHaiLong = value;
                OnPropertyChanged();
            }
        }

        private string _gopY;
        public string GopY
        {
            get => _gopY;
            set
            {
                _gopY = value;
                OnPropertyChanged();
            }
        }

        private bool _daXuLy;
        public bool DaXuLy
        {
            get => _daXuLy;
            set
            {
                _daXuLy = value;
                OnPropertyChanged();
            }
        }

        public double DiemTB
        {
            get
            {
                switch (MucDoHaiLong)
                {
                    case "Rất hài lòng": return 10;
                    case "Hài lòng": return 8;
                    case "Bình thường": return 6;
                    case "Không hài lòng": return 4;
                    default: return 0;
                }
            }
        }
    }
}