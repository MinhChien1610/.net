using Tuan_10.Helper;

namespace Tuan_10.Models
{
    public class SinhVienNhapDiemView : BaseViewModel
    {
        private string _MaSinhVien;
        public string MaSinhVien
        {
            get => _MaSinhVien;
            set
            {
                _MaSinhVien = value;
                OnPropertyChanged(nameof(MaSinhVien));
            }
        }

        private string _HoTen;
        public string HoTen
        {
            get => _HoTen;
            set
            {
                _HoTen = value;
                OnPropertyChanged(nameof(HoTen));
            }
        }

        private string _MaLop;
        public string MaLop
        {
            get => _MaLop;
            set
            {
                _MaLop = value;
                OnPropertyChanged(nameof(MaLop));
            }
        }

        private double? _Diem;
        public double? Diem
        {
            get => _Diem;
            set
            {
                _Diem = value;
                OnPropertyChanged(nameof(Diem));
            }
        }
    }
}