using System;

namespace Tuan_6.ViewModels
{
    public class NhanVienViewModel : BaseViewModel
    {
        private string _maNhanVien;
        private string _hoTen;
        private string _diaChi;

        public string MaNhanVien
        {
            get => _maNhanVien;
            set
            {
                _maNhanVien = value;
                OnPropertyChanged();
            }
        }

        public string HoTen
        {
            get => _hoTen;
            set
            {
                _hoTen = value;
                OnPropertyChanged();
            }
        }

        public string DiaChi
        {
            get => _diaChi;
            set
            {
                _diaChi = value;
                OnPropertyChanged();
            }
        }
    }
}