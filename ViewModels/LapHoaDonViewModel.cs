using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tuan_7.Helper;
using Tuan_7.Models;

namespace Tuan_7.ViewModels
{
    public class LapHoaDonViewModel : BaseViewModel
    {
        public ObservableCollection<HoaDonModel> KhoHoaDon { get; set; }

        private DateTime? _ngayLap = DateTime.Now;
        public DateTime? NgayLap
        {
            get => _ngayLap;
            set
            {
                _ngayLap = value;
                OnPropertyChanged();
            }
        }

        private string _maHoaDon;
        public string MaHoaDon
        {
            get => _maHoaDon;
            set { _maHoaDon = value; OnPropertyChanged(); }
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

        private bool _laSinhVien;
        public bool LaSinhVien
        {
            get => _laSinhVien;
            set
            {
                _laSinhVien = value;
                OnPropertyChanged();
                CapNhatThanhToan();
            }
        }

        private bool _isBan01;
        public bool IsBan01
        {
            get => _isBan01;
            set
            {
                _isBan01 = value;
                if (value)
                {
                    _isBan02 = false;
                    _isBan03 = false;
                    _isBan04 = false;
                    OnPropertyChanged(nameof(IsBan02));
                    OnPropertyChanged(nameof(IsBan03));
                    OnPropertyChanged(nameof(IsBan04));
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(BanDangChon));
            }
        }

        private bool _isBan02;
        public bool IsBan02
        {
            get => _isBan02;
            set
            {
                _isBan02 = value;
                if (value)
                {
                    _isBan01 = false;
                    _isBan03 = false;
                    _isBan04 = false;
                    OnPropertyChanged(nameof(IsBan01));
                    OnPropertyChanged(nameof(IsBan03));
                    OnPropertyChanged(nameof(IsBan04));
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(BanDangChon));
            }
        }

        private bool _isBan03;
        public bool IsBan03
        {
            get => _isBan03;
            set
            {
                _isBan03 = value;
                if (value)
                {
                    _isBan01 = false;
                    _isBan02 = false;
                    _isBan04 = false;
                    OnPropertyChanged(nameof(IsBan01));
                    OnPropertyChanged(nameof(IsBan02));
                    OnPropertyChanged(nameof(IsBan04));
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(BanDangChon));
            }
        }

        private bool _isBan04;
        public bool IsBan04
        {
            get => _isBan04;
            set
            {
                _isBan04 = value;
                if (value)
                {
                    _isBan01 = false;
                    _isBan02 = false;
                    _isBan03 = false;
                    OnPropertyChanged(nameof(IsBan01));
                    OnPropertyChanged(nameof(IsBan02));
                    OnPropertyChanged(nameof(IsBan03));
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(BanDangChon));
            }
        }

        public string BanDangChon
        {
            get
            {
                if (IsBan01) return "Bàn 01";
                if (IsBan02) return "Bàn 02";
                if (IsBan03) return "Bàn 03";
                if (IsBan04) return "Bàn 04";
                return "Chưa chọn";
            }
        }

        // Drink selection
        private bool _chonCafeDen;
        public bool ChonCafeDen
        {
            get => _chonCafeDen;
            set { _chonCafeDen = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private string _soLuongCafeDen = "0";
        public string SoLuongCafeDen
        {
            get => _soLuongCafeDen;
            set { _soLuongCafeDen = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private bool _chonCafeDa;
        public bool ChonCafeDa
        {
            get => _chonCafeDa;
            set { _chonCafeDa = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private string _soLuongCafeDa = "0";
        public string SoLuongCafeDa
        {
            get => _soLuongCafeDa;
            set { _soLuongCafeDa = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private bool _chonCafeSua;
        public bool ChonCafeSua
        {
            get => _chonCafeSua;
            set { _chonCafeSua = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private string _soLuongCafeSua = "0";
        public string SoLuongCafeSua
        {
            get => _soLuongCafeSua;
            set { _soLuongCafeSua = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private bool _chonCafeSuaDa;
        public bool ChonCafeSuaDa
        {
            get => _chonCafeSuaDa;
            set { _chonCafeSuaDa = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private string _soLuongCafeSuaDa = "0";
        public string SoLuongCafeSuaDa
        {
            get => _soLuongCafeSuaDa;
            set { _soLuongCafeSuaDa = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private bool _chonCafeKem;
        public bool ChonCafeKem
        {
            get => _chonCafeKem;
            set { _chonCafeKem = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private string _soLuongCafeKem = "0";
        public string SoLuongCafeKem
        {
            get => _soLuongCafeKem;
            set { _soLuongCafeKem = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        // Food selection
        private bool _chonBanhMiTrung;
        public bool ChonBanhMiTrung
        {
            get => _chonBanhMiTrung;
            set { _chonBanhMiTrung = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private string _soLuongBanhMiTrung = "0";
        public string SoLuongBanhMiTrung
        {
            get => _soLuongBanhMiTrung;
            set { _soLuongBanhMiTrung = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private bool _chonBanhMiCa;
        public bool ChonBanhMiCa
        {
            get => _chonBanhMiCa;
            set { _chonBanhMiCa = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private string _soLuongBanhMiCa = "0";
        public string SoLuongBanhMiCa
        {
            get => _soLuongBanhMiCa;
            set { _soLuongBanhMiCa = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private bool _chonMyTomTrung;
        public bool ChonMyTomTrung
        {
            get => _chonMyTomTrung;
            set { _chonMyTomTrung = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private string _soLuongMyTomTrung = "0";
        public string SoLuongMyTomTrung
        {
            get => _soLuongMyTomTrung;
            set { _soLuongMyTomTrung = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private bool _chonMyXaoBo;
        public bool ChonMyXaoBo
        {
            get => _chonMyXaoBo;
            set { _chonMyXaoBo = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private string _soLuongMyXaoBo = "0";
        public string SoLuongMyXaoBo
        {
            get => _soLuongMyXaoBo;
            set { _soLuongMyXaoBo = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private bool _chonMyCayBo;
        public bool ChonMyCayBo
        {
            get => _chonMyCayBo;
            set { _chonMyCayBo = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        private string _soLuongMyCayBo = "0";
        public string SoLuongMyCayBo
        {
            get => _soLuongMyCayBo;
            set { _soLuongMyCayBo = value; OnPropertyChanged(); CapNhatThanhToan(); }
        }

        public ObservableCollection<MonDaChonModel> DanhSachMonDaChon { get; set; }

        private decimal _tamTinh;
        public decimal TamTinh
        {
            get => _tamTinh;
            set
            {
                _tamTinh = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TamTinhText));
            }
        }

        private decimal _giamGia;
        public decimal GiamGia
        {
            get => _giamGia;
            set
            {
                _giamGia = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GiamGiaText));
            }
        }

        private decimal _tongThanhToan;
        public decimal TongThanhToan
        {
            get => _tongThanhToan;
            set
            {
                _tongThanhToan = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TongThanhToanText));
            }
        }

        public string TamTinhText => $"{TamTinh:N0} đ";
        public string GiamGiaText => $"{GiamGia:N0} đ";
        public string TongThanhToanText => $"{TongThanhToan:N0} đ";

        public ICommand TinhTienCommand { get; set; }
        public ICommand NhapLaiCommand { get; set; }
        public ICommand ThanhToanCommand { get; set; }
        public ICommand HoaDonMoiCommand { get; set; }

        public LapHoaDonViewModel(ObservableCollection<HoaDonModel> khoHoaDon)
        {
            KhoHoaDon = khoHoaDon;
            DanhSachMonDaChon = new ObservableCollection<MonDaChonModel>();

            TinhTienCommand = new RelayCommand(TinhTien);
            NhapLaiCommand = new RelayCommand(NhapLai);
            ThanhToanCommand = new RelayCommand(ThanhToan);
            HoaDonMoiCommand = new RelayCommand(TaoHoaDonMoi);

            TaoHoaDonMoi(null);
        }

        private void TaoHoaDonMoi(object obj)
        {
            MaHoaDon = $"HD{DateTime.Now:yyyyMMddHHmmss}";
            NgayLap = DateTime.Now;
            TenKhachHang = "";
            SoDienThoai = "";
            LaSinhVien = false;

            IsBan01 = false;
            IsBan02 = false;
            IsBan03 = false;
            IsBan04 = false;

            ChonCafeDen = false; SoLuongCafeDen = "0";
            ChonCafeDa = false; SoLuongCafeDa = "0";
            ChonCafeSua = false; SoLuongCafeSua = "0";
            ChonCafeSuaDa = false; SoLuongCafeSuaDa = "0";
            ChonCafeKem = false; SoLuongCafeKem = "0";

            ChonBanhMiTrung = false; SoLuongBanhMiTrung = "0";
            ChonBanhMiCa = false; SoLuongBanhMiCa = "0";
            ChonMyTomTrung = false; SoLuongMyTomTrung = "0";
            ChonMyXaoBo = false; SoLuongMyXaoBo = "0";
            ChonMyCayBo = false; SoLuongMyCayBo = "0";

            DanhSachMonDaChon.Clear();
            TamTinh = 0;
            GiamGia = 0;
            TongThanhToan = 0;

            OnPropertyChanged(nameof(BanDangChon));
        }

        private void TinhTien(object obj)
        {
            TaoDanhSachMon();
            CapNhatThanhToan();
        }

        private void NhapLai(object obj)
        {
            TaoHoaDonMoi(null);
        }

        private void ThanhToan(object obj)
        {
            TaoDanhSachMon();
            CapNhatThanhToan();

            if (DanhSachMonDaChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 món.");
                return;
            }

            if (BanDangChon == "Chưa chọn")
            {
                MessageBox.Show("Vui lòng chọn bàn.");
                return;
            }

            HoaDonModel hoaDon = new HoaDonModel
            {
                MaHoaDon = MaHoaDon,
                NgayGio = (NgayLap ?? DateTime.Now).ToString("dd/MM/yyyy HH:mm"),
                NgayLapRaw = NgayLap ?? DateTime.Now,
                TenKhachHang = string.IsNullOrWhiteSpace(TenKhachHang) ? "Khách lẻ" : TenKhachHang,
                SoDienThoai = SoDienThoai,
                Ban = BanDangChon,
                TamTinh = TamTinh,
                GiamGia = GiamGia,
                ThanhToan = TongThanhToan,
                ChiTietMon = new ObservableCollection<MonDaChonModel>(
                    DanhSachMonDaChon.Select(x => new MonDaChonModel
                    {
                        TenMon = x.TenMon,
                        SoLuong = x.SoLuong,
                        DonGia = x.DonGia
                    }))
            };

            KhoHoaDon.Insert(0, hoaDon);
            MessageBox.Show("Thanh toán thành công.");
            TaoHoaDonMoi(null);
        }

        private void TaoDanhSachMon()
        {
            DanhSachMonDaChon.Clear();

            ThemMonNeuHopLe(ChonCafeDen, "Cafe đen", 20000, SoLuongCafeDen);
            ThemMonNeuHopLe(ChonCafeDa, "Cafe đá", 25000, SoLuongCafeDa);
            ThemMonNeuHopLe(ChonCafeSua, "Cafe sữa", 25000, SoLuongCafeSua);
            ThemMonNeuHopLe(ChonCafeSuaDa, "Cafe sữa đá", 30000, SoLuongCafeSuaDa);
            ThemMonNeuHopLe(ChonCafeKem, "Cafe kem", 30000, SoLuongCafeKem);

            ThemMonNeuHopLe(ChonBanhMiTrung, "Bánh mì trứng", 15000, SoLuongBanhMiTrung);
            ThemMonNeuHopLe(ChonBanhMiCa, "Bánh mì cá", 15000, SoLuongBanhMiCa);
            ThemMonNeuHopLe(ChonMyTomTrung, "Mỳ tôm trứng", 20000, SoLuongMyTomTrung);
            ThemMonNeuHopLe(ChonMyXaoBo, "Mỳ xào bò", 30000, SoLuongMyXaoBo);
            ThemMonNeuHopLe(ChonMyCayBo, "Mỳ cay bò", 50000, SoLuongMyCayBo);
        }

        private void ThemMonNeuHopLe(bool duocChon, string tenMon, decimal donGia, string soLuongText)
        {
            if (!duocChon) return;

            if (!int.TryParse(soLuongText, out int soLuong) || soLuong <= 0)
                soLuong = 1;

            DanhSachMonDaChon.Add(new MonDaChonModel
            {
                TenMon = tenMon,
                SoLuong = soLuong,
                DonGia = donGia
            });
        }

        private void CapNhatThanhToan()
        {
            TaoDanhSachMonCore();

            TamTinh = DanhSachMonDaChon.Sum(x => x.ThanhTien);
            GiamGia = LaSinhVien ? TamTinh * 0.2m : 0;
            TongThanhToan = TamTinh - GiamGia;
        }

        private void TaoDanhSachMonCore()
        {
            DanhSachMonDaChon.Clear();

            ThemMonNeuHopLe(ChonCafeDen, "Cafe đen", 20000, SoLuongCafeDen);
            ThemMonNeuHopLe(ChonCafeDa, "Cafe đá", 25000, SoLuongCafeDa);
            ThemMonNeuHopLe(ChonCafeSua, "Cafe sữa", 25000, SoLuongCafeSua);
            ThemMonNeuHopLe(ChonCafeSuaDa, "Cafe sữa đá", 30000, SoLuongCafeSuaDa);
            ThemMonNeuHopLe(ChonCafeKem, "Cafe kem", 30000, SoLuongCafeKem);

            ThemMonNeuHopLe(ChonBanhMiTrung, "Bánh mì trứng", 15000, SoLuongBanhMiTrung);
            ThemMonNeuHopLe(ChonBanhMiCa, "Bánh mì cá", 15000, SoLuongBanhMiCa);
            ThemMonNeuHopLe(ChonMyTomTrung, "Mỳ tôm trứng", 20000, SoLuongMyTomTrung);
            ThemMonNeuHopLe(ChonMyXaoBo, "Mỳ xào bò", 30000, SoLuongMyXaoBo);
            ThemMonNeuHopLe(ChonMyCayBo, "Mỳ cay bò", 50000, SoLuongMyCayBo);
        }
    }
}