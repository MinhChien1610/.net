using System.Windows;
using System.Windows.Input;
using Tuan_7.Helper;
using Tuan_7.Models;

namespace Tuan_7.ViewModels
{
    public class ThongTinKHViewModel : BaseViewModel
    {
        private readonly KhachHangModel _khachHang;

        public string TenKhachHang
        {
            get => _khachHang.TenKhachHang;
            set
            {
                _khachHang.TenKhachHang = value;
                OnPropertyChanged();
            }
        }

        public string SoDienThoai
        {
            get => _khachHang.SoDienThoai;
            set
            {
                _khachHang.SoDienThoai = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _khachHang.Email;
            set
            {
                _khachHang.Email = value;
                OnPropertyChanged();
            }
        }

        public ICommand LuuCommand { get; set; }
        public ICommand NhapLaiCommand { get; set; }

        public ThongTinKHViewModel(KhachHangModel khachHang)
        {
            _khachHang = khachHang;

            LuuCommand = new RelayCommand(LuuThongTin);
            NhapLaiCommand = new RelayCommand(NhapLai);
        }

        private void LuuThongTin(object obj)
        {
            MessageBox.Show("Lưu thông tin khách hàng thành công.");
        }

        private void NhapLai(object obj)
        {
            TenKhachHang = "";
            SoDienThoai = "";
            Email = "";
        }
    }
}