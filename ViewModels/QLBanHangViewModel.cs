using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Tuan_7.Helper;
using Tuan_7.Models;

namespace Tuan_7.ViewModels
{
    public class QLBanHangViewModel : BaseViewModel
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private string _tieuDeTrang;
        public string TieuDeTrang
        {
            get => _tieuDeTrang;
            set { _tieuDeTrang = value; OnPropertyChanged(); }
        }

        private string _moTaTrang;
        public string MoTaTrang
        {
            get => _moTaTrang;
            set { _moTaTrang = value; OnPropertyChanged(); }
        }

        private string _noiDungChinh;
        public string NoiDungChinh
        {
            get => _noiDungChinh;
            set { _noiDungChinh = value; OnPropertyChanged(); }
        }

        private string _noiDungMoTa;
        public string NoiDungMoTa
        {
            get => _noiDungMoTa;
            set { _noiDungMoTa = value; OnPropertyChanged(); }
        }

        private DateTime? _ngayLap = DateTime.Now;
        public DateTime? NgayLap
        {
            get => _ngayLap;
            set
            {
                _ngayLap = value;
                OnPropertyChanged();

                LapHoaDonVM.NgayLap = value;
            }
        }

        public ObservableCollection<HoaDonModel> KhoHoaDon { get; set; }

        public LapHoaDonViewModel LapHoaDonVM { get; set; }
        public DanhSachHoaDonViewModel DanhSachHoaDonVM { get; set; }

        public ICommand LapHoaDonCommand { get; set; }
        public ICommand DanhSachHoaDonCommand { get; set; }
        public ICommand ThongKeCommand { get; set; }
        public ICommand ThoatCommand { get; set; }
        public ICommand HoaDonMoiCommand { get; set; }

        public QLBanHangViewModel()
        {
            KhoHoaDon = new ObservableCollection<HoaDonModel>();

            LapHoaDonVM = new LapHoaDonViewModel(KhoHoaDon);
            DanhSachHoaDonVM = new DanhSachHoaDonViewModel(KhoHoaDon);

            DanhSachHoaDonVM.RequestTaoHoaDonMoi = MoLapHoaDon;

            LapHoaDonCommand = new RelayCommand(_ => MoLapHoaDon());
            DanhSachHoaDonCommand = new RelayCommand(_ => MoDanhSachHoaDon());
            ThongKeCommand = new RelayCommand(_ => MoDanhSachHoaDon());
            ThoatCommand = new RelayCommand(_ => Application.Current.Shutdown());
            HoaDonMoiCommand = new RelayCommand(_ => MoLapHoaDonMoi());

            MoLapHoaDon();
        }

        private void MoLapHoaDon()
        {
            CurrentView = LapHoaDonVM;
            TieuDeTrang = "Lập hóa đơn";
            MoTaTrang = "Tạo hóa đơn mới cho khách hàng";
            NoiDungChinh = "Lập hóa đơn";
            NoiDungMoTa = "Nhập thông tin khách hàng, chọn bàn, chọn món và thực hiện thanh toán.";
            NgayLap = LapHoaDonVM.NgayLap;
        }

        private void MoDanhSachHoaDon()
        {
            DanhSachHoaDonVM.RefreshDanhSach();
            CurrentView = DanhSachHoaDonVM;
            TieuDeTrang = "Danh sách hóa đơn";
            MoTaTrang = "Theo dõi và lọc các hóa đơn đã thanh toán";
            NoiDungChinh = "Danh sách hóa đơn";
            NoiDungMoTa = "Xem lịch sử hóa đơn, lọc theo ngày và thống kê tổng doanh thu.";
        }

        private void MoLapHoaDonMoi()
        {
            LapHoaDonVM.HoaDonMoiCommand.Execute(null);
            MoLapHoaDon();
        }
    }
}