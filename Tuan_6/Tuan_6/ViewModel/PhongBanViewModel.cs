using System.Collections.ObjectModel;

namespace Tuan_6.ViewModels
{
    public class PhongBanViewModel : BaseViewModel
    {
        private string _tenPhongBan;

        public string TenPhongBan
        {
            get => _tenPhongBan;
            set
            {
                _tenPhongBan = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<NhanVienViewModel> DanhSachNhanVien { get; set; }

        public PhongBanViewModel()
        {
            DanhSachNhanVien = new ObservableCollection<NhanVienViewModel>();
        }
    }
}