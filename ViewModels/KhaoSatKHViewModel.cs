using System.Windows;
using System.Windows.Input;
using Tuan_7.Helper;

namespace Tuan_7.ViewModels
{
    public class KhaoSatKHViewModel : BaseViewModel
    {
        private string _tieuDeTrang;
        public string TieuDeTrang
        {
            get => _tieuDeTrang;
            set
            {
                _tieuDeTrang = value;
                OnPropertyChanged();
            }
        }

        private string _moTaTrang;
        public string MoTaTrang
        {
            get => _moTaTrang;
            set
            {
                _moTaTrang = value;
                OnPropertyChanged();
            }
        }

        private string _noiDungHienThi;
        public string NoiDungHienThi
        {
            get => _noiDungHienThi;
            set
            {
                _noiDungHienThi = value;
                OnPropertyChanged();
            }
        }

        private string _noiDungMoTa;
        public string NoiDungMoTa
        {
            get => _noiDungMoTa;
            set
            {
                _noiDungMoTa = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenThongTinKHCommand { get; set; }
        public ICommand OpenKhaoSatCommand { get; set; }
        public ICommand OpenDanhSachPhanHoiCommand { get; set; }
        public ICommand ThemPhanHoiMoiCommand { get; set; }

        public KhaoSatKHViewModel()
        {
            TieuDeTrang = "Khảo sát khách hàng";
            MoTaTrang = "Quản lý thông tin, khảo sát và phản hồi của khách hàng";
            NoiDungHienThi = "Thông tin khách hàng";
            NoiDungMoTa = "Đây là khu vực hiển thị thông tin khách hàng. Bạn có thể tiếp tục phát triển phần này bằng UserControl hoặc Form nhập liệu riêng.";

            OpenThongTinKHCommand = new RelayCommand(ExecuteOpenThongTinKH);
            OpenKhaoSatCommand = new RelayCommand(ExecuteOpenKhaoSat);
            OpenDanhSachPhanHoiCommand = new RelayCommand(ExecuteOpenDanhSachPhanHoi);
            ThemPhanHoiMoiCommand = new RelayCommand(ExecuteThemPhanHoiMoi);
        }

        private void ExecuteOpenThongTinKH(object obj)
        {
            NoiDungHienThi = "Thông tin khách hàng";
            NoiDungMoTa = "Hiển thị các thông tin như họ tên, số điện thoại, email, địa chỉ và lịch sử tương tác của khách hàng.";
        }

        private void ExecuteOpenKhaoSat(object obj)
        {
            NoiDungHienThi = "Khảo sát / Góp ý";
            NoiDungMoTa = "Hiển thị biểu mẫu khảo sát hoặc góp ý của khách hàng. Bạn có thể thêm các trường như mức độ hài lòng, nội dung góp ý và đánh giá dịch vụ.";
        }

        private void ExecuteOpenDanhSachPhanHoi(object obj)
        {
            NoiDungHienThi = "Danh sách phản hồi";
            NoiDungMoTa = "Hiển thị danh sách tất cả các phản hồi đã gửi. Sau này bạn có thể bind DataGrid hoặc ListView vào đây để quản lý dữ liệu dễ hơn.";
        }

        private void ExecuteThemPhanHoiMoi(object obj)
        {
            MessageBox.Show("Chức năng thêm phản hồi mới", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}