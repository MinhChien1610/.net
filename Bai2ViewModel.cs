using Tuan_10.Helper;
using System.Windows.Input;
using Tuan_10.Views;

namespace Tuan_10.ViewModels
{
    public class Bai2ViewModel : BaseViewModel
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand ShowKhoaCommand { get; set; }
        public ICommand ShowLopCommand { get; set; }
        public ICommand ShowMonHocCommand { get; set; }
        public ICommand ShowSinhVienCommand { get; set; }

        public ICommand ShowNhapDiemCommand { get; set; }
        public ICommand ShowDiemSVCommand { get; set; }
        public ICommand ShowDiemLopCommand { get; set; }

        public Bai2ViewModel()
        {
            ShowKhoaCommand = new RelayCommand(_ =>
            {
                CurrentView = new bai1();
            });

            ShowLopCommand = new RelayCommand(_ =>
            {
                CurrentView = new bai3();
            });

            ShowMonHocCommand = new RelayCommand(_ =>
            {
                CurrentView = new bai5();
            });

            ShowSinhVienCommand = new RelayCommand(_ =>
            {
                CurrentView = new bai4();
            });

            ShowNhapDiemCommand = new RelayCommand(_ =>
            {
                CurrentView = new bai6();
            });

            ShowDiemSVCommand = new RelayCommand(_ =>
            {
                CurrentView = new bai7();
            });

            ShowDiemLopCommand = new RelayCommand(_ =>
            {
                // CurrentView = new bai8();
            });

        }
    }
}