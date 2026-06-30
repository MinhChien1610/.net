using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tuan_7.Helper;
using Tuan_7.Models;

namespace Tuan_7.ViewModels
{
    public class KhaoSatMucDoViewModel : BaseViewModel
    {
        private readonly KhachHangModel _khachHang;
        private readonly ObservableCollection<PhanHoiModel> _khoPhanHoi;

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

        private bool _isRatHaiLong;
        public bool IsRatHaiLong
        {
            get => _isRatHaiLong;
            set
            {
                _isRatHaiLong = value;
                if (value)
                {
                    _isHaiLong = false;
                    _isBinhThuong = false;
                    _isKhongHaiLong = false;
                    OnPropertyChanged(nameof(IsHaiLong));
                    OnPropertyChanged(nameof(IsBinhThuong));
                    OnPropertyChanged(nameof(IsKhongHaiLong));
                }
                OnPropertyChanged();
            }
        }

        private bool _isHaiLong;
        public bool IsHaiLong
        {
            get => _isHaiLong;
            set
            {
                _isHaiLong = value;
                if (value)
                {
                    _isRatHaiLong = false;
                    _isBinhThuong = false;
                    _isKhongHaiLong = false;
                    OnPropertyChanged(nameof(IsRatHaiLong));
                    OnPropertyChanged(nameof(IsBinhThuong));
                    OnPropertyChanged(nameof(IsKhongHaiLong));
                }
                OnPropertyChanged();
            }
        }

        private bool _isBinhThuong;
        public bool IsBinhThuong
        {
            get => _isBinhThuong;
            set
            {
                _isBinhThuong = value;
                if (value)
                {
                    _isRatHaiLong = false;
                    _isHaiLong = false;
                    _isKhongHaiLong = false;
                    OnPropertyChanged(nameof(IsRatHaiLong));
                    OnPropertyChanged(nameof(IsHaiLong));
                    OnPropertyChanged(nameof(IsKhongHaiLong));
                }
                OnPropertyChanged();
            }
        }

        private bool _isKhongHaiLong;
        public bool IsKhongHaiLong
        {
            get => _isKhongHaiLong;
            set
            {
                _isKhongHaiLong = value;
                if (value)
                {
                    _isRatHaiLong = false;
                    _isHaiLong = false;
                    _isBinhThuong = false;
                    OnPropertyChanged(nameof(IsRatHaiLong));
                    OnPropertyChanged(nameof(IsHaiLong));
                    OnPropertyChanged(nameof(IsBinhThuong));
                }
                OnPropertyChanged();
            }
        }

        private string _gopYThem;
        public string GopYThem
        {
            get => _gopYThem;
            set
            {
                _gopYThem = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> DanhSachPhanHoi { get; set; }

        public ICommand GuiPhanHoiCommand { get; set; }
        public ICommand NhapLaiCommand { get; set; }

        public KhaoSatMucDoViewModel(KhachHangModel khachHang, ObservableCollection<PhanHoiModel> khoPhanHoi)
        {
            _khachHang = khachHang;
            _khoPhanHoi = khoPhanHoi;

            DanhSachPhanHoi = new ObservableCollection<string>();

            GuiPhanHoiCommand = new RelayCommand(GuiPhanHoi);
            NhapLaiCommand = new RelayCommand(NhapLai);
        }

        private void GuiPhanHoi(object obj)
        {
            string mucDo = LayMucDo();

            if (string.IsNullOrWhiteSpace(mucDo))
            {
                MessageBox.Show("Vui lòng chọn mức độ hài lòng.");
                return;
            }

            PhanHoiModel phanHoi = new PhanHoiModel
            {
                Id = _khoPhanHoi.Count + 1,
                TenKhachHang = string.IsNullOrWhiteSpace(TenKhachHang) ? "Khách hàng" : TenKhachHang,
                SoDienThoai = SoDienThoai,
                Email = Email,
                NgayRaw = DateTime.Now,
                Ngay = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                MucDoHaiLong = mucDo,
                GopY = GopYThem,
                DaXuLy = false
            };

            _khoPhanHoi.Insert(0, phanHoi);
            DanhSachPhanHoi.Insert(0, $"{phanHoi.TenKhachHang} - {phanHoi.MucDoHaiLong}" +
                                         (string.IsNullOrWhiteSpace(phanHoi.GopY) ? "" : $" - {phanHoi.GopY}"));

            MessageBox.Show("Gửi phản hồi thành công.");
            NhapLai(null);
        }

        private void NhapLai(object obj)
        {
            IsRatHaiLong = false;
            IsHaiLong = false;
            IsBinhThuong = false;
            IsKhongHaiLong = false;
            GopYThem = "";
        }

        private string LayMucDo()
        {
            if (IsRatHaiLong) return "Rất hài lòng";
            if (IsHaiLong) return "Hài lòng";
            if (IsBinhThuong) return "Bình thường";
            if (IsKhongHaiLong) return "Không hài lòng";
            return "";
        }
    }
}