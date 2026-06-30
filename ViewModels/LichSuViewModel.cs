using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Tuan_7.Helper;
using Tuan_7.Models;

namespace Tuan_7.ViewModels
{
    public class LichSuViewModel : BaseViewModel
    {
        private readonly TaiKhoanViewModel _taiKhoanVM;
        private readonly GiaoDichViewModel _giaoDichVM;

        public ObservableCollection<TaiKhoanModel> DanhSachTaiKhoan => _taiKhoanVM.DanhSach;

        public ObservableCollection<string> DanhSachLoaiGiaoDich { get; set; }

        private ObservableCollection<GiaoDichModel> _danhSachGiaoDich;
        public ObservableCollection<GiaoDichModel> DanhSachGiaoDich
        {
            get => _danhSachGiaoDich;
            set
            {
                _danhSachGiaoDich = value;
                OnPropertyChanged();
                CapNhatThongKe();
            }
        }

        private GiaoDichModel _giaoDichDuocChon;
        public GiaoDichModel GiaoDichDuocChon
        {
            get => _giaoDichDuocChon;
            set
            {
                _giaoDichDuocChon = value;
                OnPropertyChanged();
            }
        }

        private TaiKhoanModel _taiKhoanDuocChon;
        public TaiKhoanModel TaiKhoanDuocChon
        {
            get => _taiKhoanDuocChon;
            set
            {
                _taiKhoanDuocChon = value;
                OnPropertyChanged();
            }
        }

        private string _loaiGiaoDichDuocChon;
        public string LoaiGiaoDichDuocChon
        {
            get => _loaiGiaoDichDuocChon;
            set
            {
                _loaiGiaoDichDuocChon = value;
                OnPropertyChanged();
            }
        }

        private string _tuKhoa;
        public string TuKhoa
        {
            get => _tuKhoa;
            set
            {
                _tuKhoa = value;
                OnPropertyChanged();
            }
        }

        private string _tongThu;
        public string TongThu
        {
            get => _tongThu;
            set
            {
                _tongThu = value;
                OnPropertyChanged();
            }
        }

        private string _tongChi;
        public string TongChi
        {
            get => _tongChi;
            set
            {
                _tongChi = value;
                OnPropertyChanged();
            }
        }

        private int _soGiaoDich;
        public int SoGiaoDich
        {
            get => _soGiaoDich;
            set
            {
                _soGiaoDich = value;
                OnPropertyChanged();
            }
        }

        public ICommand LocCommand { get; set; }
        public ICommand HienThiTatCaCommand { get; set; }

        public LichSuViewModel(TaiKhoanViewModel taiKhoanVM, GiaoDichViewModel giaoDichVM)
        {
            _taiKhoanVM = taiKhoanVM;
            _giaoDichVM = giaoDichVM;

            DanhSachLoaiGiaoDich = new ObservableCollection<string>
            {
                "Tất cả",
                "Gửi tiền",
                "Rút tiền",
                "Chuyển khoản"
            };

            LoaiGiaoDichDuocChon = "Tất cả";
            TuKhoa = "";
            DanhSachGiaoDich = new ObservableCollection<GiaoDichModel>();

            LocCommand = new RelayCommand(LocDuLieu);
            HienThiTatCaCommand = new RelayCommand(HienThiTatCa);

            RefreshDanhSach();
        }

        public void RefreshDanhSach()
        {
            DanhSachGiaoDich = new ObservableCollection<GiaoDichModel>(
                _giaoDichVM.DanhSachGiaoDich.OrderByDescending(x => x.NgayTaoRaw));
        }

        private void LocDuLieu(object obj)
        {
            var query = _giaoDichVM.DanhSachGiaoDich.AsEnumerable();

            if (TaiKhoanDuocChon != null)
            {
                query = query.Where(x =>
                    x.TKNguon == TaiKhoanDuocChon.SoTK ||
                    x.TKDich == TaiKhoanDuocChon.SoTK);
            }

            if (!string.IsNullOrWhiteSpace(LoaiGiaoDichDuocChon) &&
                LoaiGiaoDichDuocChon != "Tất cả")
            {
                query = query.Where(x => x.Loai == LoaiGiaoDichDuocChon);
            }

            if (!string.IsNullOrWhiteSpace(TuKhoa))
            {
                string key = TuKhoa.Trim().ToLower();
                query = query.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.MaGD) && x.MaGD.ToLower().Contains(key)) ||
                    (!string.IsNullOrWhiteSpace(x.NoiDung) && x.NoiDung.ToLower().Contains(key)) ||
                    (!string.IsNullOrWhiteSpace(x.TKNguon) && x.TKNguon.ToLower().Contains(key)) ||
                    (!string.IsNullOrWhiteSpace(x.TKDich) && x.TKDich.ToLower().Contains(key)));
            }

            DanhSachGiaoDich = new ObservableCollection<GiaoDichModel>(
                query.OrderByDescending(x => x.NgayTaoRaw));
        }

        private void HienThiTatCa(object obj)
        {
            TaiKhoanDuocChon = null;
            LoaiGiaoDichDuocChon = "Tất cả";
            TuKhoa = "";
            RefreshDanhSach();
        }

        private void CapNhatThongKe()
        {
            decimal tongThu = DanhSachGiaoDich
                .Where(x => x.Loai == "Gửi tiền")
                .Sum(x => x.SoTien);

            decimal tongChi = DanhSachGiaoDich
                .Where(x => x.Loai == "Rút tiền" || x.Loai == "Chuyển khoản")
                .Sum(x => x.SoTien);

            TongThu = $"{tongThu:N0} VNĐ";
            TongChi = $"{tongChi:N0} VNĐ";
            SoGiaoDich = DanhSachGiaoDich.Count;
        }
    }
}