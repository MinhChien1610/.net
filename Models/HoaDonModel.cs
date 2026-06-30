using System;
using System.Collections.ObjectModel;
using Tuan_7.Helper;

namespace Tuan_7.Models
{
    public class HoaDonModel : BaseViewModel
    {
        private string _maHoaDon;
        public string MaHoaDon
        {
            get => _maHoaDon;
            set { _maHoaDon = value; OnPropertyChanged(); }
        }

        private string _ngayGio;
        public string NgayGio
        {
            get => _ngayGio;
            set { _ngayGio = value; OnPropertyChanged(); }
        }

        private DateTime _ngayLapRaw;
        public DateTime NgayLapRaw
        {
            get => _ngayLapRaw;
            set { _ngayLapRaw = value; OnPropertyChanged(); }
        }

        private string _tenKhachHang;
        public string TenKhachHang
        {
            get => _tenKhachHang;
            set { _tenKhachHang = value; OnPropertyChanged(); }
        }

        private string _soDienThoai;
        public string SoDienThoai
        {
            get => _soDienThoai;
            set { _soDienThoai = value; OnPropertyChanged(); }
        }

        private string _ban;
        public string Ban
        {
            get => _ban;
            set { _ban = value; OnPropertyChanged(); }
        }

        private decimal _tamTinh;
        public decimal TamTinh
        {
            get => _tamTinh;
            set { _tamTinh = value; OnPropertyChanged(); }
        }

        private decimal _giamGia;
        public decimal GiamGia
        {
            get => _giamGia;
            set { _giamGia = value; OnPropertyChanged(); }
        }

        private decimal _thanhToan;
        public decimal ThanhToan
        {
            get => _thanhToan;
            set { _thanhToan = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MonDaChonModel> _chiTietMon = new ObservableCollection<MonDaChonModel>();
        public ObservableCollection<MonDaChonModel> ChiTietMon
        {
            get => _chiTietMon;
            set { _chiTietMon = value; OnPropertyChanged(); }
        }
    }
}