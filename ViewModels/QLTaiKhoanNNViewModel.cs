using System.Windows;
using System.Windows.Input;
using Tuan_7.Helper;

namespace Tuan_7.ViewModels
{
    public class QLTaiKhoanNNViewModel : BaseViewModel
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

        public TaiKhoanViewModel TaiKhoanVM { get; set; }
        public GiaoDichViewModel GiaoDichVM { get; set; }
        public LichSuViewModel LichSuVM { get; set; }

        public ICommand QuanLyTaiKhoanCommand { get; set; }
        public ICommand GiaoDichCommand { get; set; }
        public ICommand LichSuGiaoDichCommand { get; set; }
        public ICommand ThoatCommand { get; set; }

        public QLTaiKhoanNNViewModel()
        {
            TaiKhoanVM = new TaiKhoanViewModel();
            GiaoDichVM = new GiaoDichViewModel(TaiKhoanVM);
            LichSuVM = new LichSuViewModel(TaiKhoanVM, GiaoDichVM);

            CurrentView = TaiKhoanVM;

            QuanLyTaiKhoanCommand = new RelayCommand(MoTaiKhoan);
            GiaoDichCommand = new RelayCommand(MoGiaoDich);
            LichSuGiaoDichCommand = new RelayCommand(MoLichSu);
            ThoatCommand = new RelayCommand(Thoat);
        }

        private void MoTaiKhoan(object obj)
        {
            CurrentView = TaiKhoanVM;
        }

        private void MoGiaoDich(object obj)
        {
            CurrentView = GiaoDichVM;
        }

        private void MoLichSu(object obj)
        {
            LichSuVM.RefreshDanhSach();
            CurrentView = LichSuVM;
        }

        private void Thoat(object obj)
        {
            Application.Current.Shutdown();
        }
    }
}