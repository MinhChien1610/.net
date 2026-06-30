using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Tuan_7.Helper;
using Tuan_7.Models;

namespace Tuan_7.ViewModels
{
    public class DanhSachHoaDonViewModel : BaseViewModel
    {
        private readonly ObservableCollection<HoaDonModel> _khoHoaDonGoc;

        private DateTime? _ngayChon = DateTime.Now;
        public DateTime? NgayChon
        {
            get => _ngayChon;
            set
            {
                _ngayChon = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tuNgay = DateTime.Now.Date;
        public DateTime? TuNgay
        {
            get => _tuNgay;
            set
            {
                _tuNgay = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _denNgay = DateTime.Now.Date;
        public DateTime? DenNgay
        {
            get => _denNgay;
            set
            {
                _denNgay = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<HoaDonModel> _danhSachHoaDon;
        public ObservableCollection<HoaDonModel> DanhSachHoaDon
        {
            get => _danhSachHoaDon;
            set
            {
                _danhSachHoaDon = value;
                OnPropertyChanged();
                CapNhatThongKe();
            }
        }

        private HoaDonModel _hoaDonDuocChon;
        public HoaDonModel HoaDonDuocChon
        {
            get => _hoaDonDuocChon;
            set
            {
                _hoaDonDuocChon = value;
                OnPropertyChanged();
            }
        }

        private string _tongSoHoaDonText;
        public string TongSoHoaDonText
        {
            get => _tongSoHoaDonText;
            set
            {
                _tongSoHoaDonText = value;
                OnPropertyChanged();
            }
        }

        private string _tongDoanhThuText;
        public string TongDoanhThuText
        {
            get => _tongDoanhThuText;
            set
            {
                _tongDoanhThuText = value;
                OnPropertyChanged();
            }
        }

        public ICommand LocCommand { get; set; }
        public ICommand TaoHoaDonMoiCommand { get; set; }

        public Action RequestTaoHoaDonMoi { get; set; }

        public DanhSachHoaDonViewModel(ObservableCollection<HoaDonModel> khoHoaDon)
        {
            _khoHoaDonGoc = khoHoaDon;
            DanhSachHoaDon = new ObservableCollection<HoaDonModel>(_khoHoaDonGoc.OrderByDescending(x => x.NgayLapRaw));

            LocCommand = new RelayCommand(LocHoaDon);
            TaoHoaDonMoiCommand = new RelayCommand(TaoHoaDonMoi);

            CapNhatThongKe();
        }

        public void RefreshDanhSach()
        {
            DanhSachHoaDon = new ObservableCollection<HoaDonModel>(
                _khoHoaDonGoc.OrderByDescending(x => x.NgayLapRaw));
        }

        private void LocHoaDon(object obj)
        {
            var query = _khoHoaDonGoc.AsEnumerable();

            if (TuNgay.HasValue)
            {
                DateTime tu = TuNgay.Value.Date;
                query = query.Where(x => x.NgayLapRaw.Date >= tu);
            }

            if (DenNgay.HasValue)
            {
                DateTime den = DenNgay.Value.Date;
                query = query.Where(x => x.NgayLapRaw.Date <= den);
            }

            DanhSachHoaDon = new ObservableCollection<HoaDonModel>(
                query.OrderByDescending(x => x.NgayLapRaw));
        }

        private void TaoHoaDonMoi(object obj)
        {
            RequestTaoHoaDonMoi?.Invoke();
        }

        private void CapNhatThongKe()
        {
            TongSoHoaDonText = $"Tổng số hóa đơn: {DanhSachHoaDon.Count}";
            TongDoanhThuText = $"Tổng doanh thu: {DanhSachHoaDon.Sum(x => x.ThanhToan):N0} đ";
        }
    }
}