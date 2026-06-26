using System;
using System.Windows;

namespace Tuan_3
{
    public partial class bai_5 : Window
    {
        public bai_5()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            cbPhongBan.Items.Add("Phòng Kế toán");
            cbPhongBan.Items.Add("Phòng Nhân sự");
            cbPhongBan.Items.Add("Phòng Kỹ thuật");
            cbPhongBan.Items.Add("Phòng Kinh doanh");
            cbPhongBan.SelectedIndex = 0;

            cbViTri.Items.Add("Nhân viên");
            cbViTri.Items.Add("Trưởng nhóm");
            cbViTri.Items.Add("Quản lý");
            cbViTri.Items.Add("Thực tập sinh");
            cbViTri.SelectedIndex = 0;
        }

        private string GetMaHoSo()
        {
            string ma = txtMaHoSo.Text.Trim();
            if (string.IsNullOrWhiteSpace(ma))
                return null;
            return ma;
        }

        private string GetHoTen()
        {
            string ten = txtHoTen.Text.Trim();
            if (string.IsNullOrWhiteSpace(ten))
                return null;
            return ten;
        }

        private string GetGioiTinh()
        {
            if (rdNam.IsChecked == true)
                return "Nam";
            if (rdNu.IsChecked == true)
                return "Nữ";
            return null;
        }

        private string GetPhongBan()
        {
            if (cbPhongBan.SelectedIndex >= 0)
                return cbPhongBan.Text;
            return null;
        }

        private string GetViTri()
        {
            if (cbViTri.SelectedIndex >= 0)
                return cbViTri.Text;
            return null;
        }

        private string GetLoaiHopDong()
        {
            if (rdThuViec.IsChecked == true)
                return "Thử việc";
            if (rdHopDongNam.IsChecked == true)
                return "Hợp đồng năm";
            if (rdChinhThuc.IsChecked == true)
                return "Chính thức";
            return null;
        }

     
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            string giayTo = txtGiayToKhac.Text.Trim();

            if (string.IsNullOrEmpty(giayTo))
            {
                MessageBox.Show("Vui lòng nhập tên giấy tờ!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            lstGiayTo.Items.Add(giayTo);
            txtGiayToKhac.Clear();
            txtGiayToKhac.Focus();
        }

        private void BtnTaiLai_Click(object sender, RoutedEventArgs e)
        {
            txtMaHoSo.Clear();
            txtHoTen.Clear();
            txtGhiChu.Clear();
            txtGiayToKhac.Clear();

            cbPhongBan.SelectedIndex = 0;
            cbViTri.SelectedIndex = 0;

            rdNam.IsChecked = false;
            rdNu.IsChecked = false;

            rdThuViec.IsChecked = false;
            rdHopDongNam.IsChecked = false;
            rdChinhThuc.IsChecked = false;

            chkBangCap.IsChecked = false;
            chkSucKhoe.IsChecked = false;
            chkTinHoc.IsChecked = false;
            chkNgoaiNgu.IsChecked = false;

            lstGiayTo.Items.Clear();
        }

        private void BtnThoat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát chương trình không?",
                "Xác nhận thoát",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                this.Close();
        }

        private void btnNopHoSo_Click(object sender, RoutedEventArgs e)
        {
            string maHoSo = GetMaHoSo();
            if (maHoSo == null)
            {
                MessageBox.Show("Vui lòng nhập mã hồ sơ!");
                txtMaHoSo.Focus();
                return;
            }

            string hoTen = GetHoTen();
            if (hoTen == null)
            {
                MessageBox.Show("Vui lòng nhập họ tên!");
                txtHoTen.Focus();
                return;
            }

            string gioiTinh = GetGioiTinh();
            if (gioiTinh == null)
            {
                MessageBox.Show("Vui lòng chọn giới tính!");
                return;
            }

            string phongBan = GetPhongBan();
            if (phongBan == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban!");
                return;
            }

            string viTri = GetViTri();
            if (viTri == null)
            {
                MessageBox.Show("Vui lòng chọn vị trí!");
                return;
            }

            string loaiHopDong = GetLoaiHopDong();
            if (loaiHopDong == null)
            {
                MessageBox.Show("Vui lòng chọn loại hợp đồng!");
                return;
            }

            MessageBox.Show(
                $"Nộp hồ sơ thành công!\n\n" +
                $"Mã hồ sơ: {maHoSo}\n" +
                $"Họ tên: {hoTen}\n" +
                $"Giới tính: {gioiTinh}\n" +
                $"Phòng ban: {phongBan}\n" +
                $"Vị trí: {viTri}\n" +
                $"Loại hợp đồng: {loaiHopDong}",
                "Thành công",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
