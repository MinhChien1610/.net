using System.Text;
using System.Windows;

namespace Tuan_3
{
    public partial class bai_6 : Window
    {
        public bai_6()
        {
            InitializeComponent();

            cb_lop.Items.Add("CNTT");
            cb_lop.Items.Add("Kế toán");
            cb_lop.Items.Add("Quản trị kinh doanh");
            cb_lop.Items.Add("Du lịch");

            cb_lop.SelectedIndex = 0;
        }

  
        private void btnClick_gui(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_ten.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!",
                    "Thiếu thông tin",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (cb_lop.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn lớp/khoa!",
                    "Thiếu thông tin",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            StringBuilder kq = new StringBuilder();

            kq.AppendLine("PHIẾU KHẢO SÁT SINH VIÊN");
            kq.AppendLine("------------------------------");
            kq.AppendLine("Họ tên: " + txt_ten.Text);
            kq.AppendLine("Lớp/Khoa: " + cb_lop.Text);

            string gioiTinh = rb_nam.IsChecked == true ? "Nam"
                               : rb_nu.IsChecked == true ? "Nữ"
                               : "Khác";
            kq.AppendLine("Giới tính: " + gioiTinh);

            string cau1 =
                rb_gd_ratTot.IsChecked == true ? "Rất tốt" :
                rb_gd_tot.IsChecked == true ? "Tốt" :
                rb_gd_tb.IsChecked == true ? "Trung bình" :
                rb_gd_kem.IsChecked == true ? "Kém" :
                "Chưa chọn";

            kq.AppendLine("1. Giảng dạy: " + cau1);

            string cau2 =
                rb_csvc_ratHL.IsChecked == true ? "Rất hài lòng" :
                rb_csvc_hl.IsChecked == true ? "Hài lòng" :
                rb_csvc_bt.IsChecked == true ? "Bình thường" :
                rb_csvc_khl.IsChecked == true ? "Không hài lòng" :
                "Chưa chọn";

            kq.AppendLine("2. Cơ sở vật chất: " + cau2);

            string cau3 =
                rb_online_co.IsChecked == true ? "Có" :
                rb_online_khong.IsChecked == true ? "Không" :
                rb_online_tuy.IsChecked == true ? "Tùy trường hợp" :
                "Chưa chọn";

            kq.AppendLine("3. Học online: " + cau3);

            kq.AppendLine("Đề xuất:");

            if (cb_giamTai.IsChecked == true)
                kq.AppendLine("- Giảm tải học phần");

            if (cb_thucHanh.IsChecked == true)
                kq.AppendLine("- Tăng cường thực hành");

            if (cb_wifi.IsChecked == true)
                kq.AppendLine("- Cải thiện Wifi");

            if (cb_giangVien.IsChecked == true)
                kq.AppendLine("- Giảng viên nhiệt tình hơn");

            if (!string.IsNullOrWhiteSpace(txt_khac.Text))
                kq.AppendLine("- Khác: " + txt_khac.Text);

            if (!string.IsNullOrWhiteSpace(txt_yKienThem.Text))
            {
                kq.AppendLine("Ý kiến thêm:");
                kq.AppendLine(txt_yKienThem.Text);
            }

            tb_ketQua.Text = kq.ToString();
            tb_ketQua.IsReadOnly = true;

            tab_main.SelectedIndex = 1;
        }

        private void btnClick_lamMoi(object sender, RoutedEventArgs e)
        {
            txt_ten.Clear();
            txt_khac.Clear();
            txt_yKienThem.Clear();
            tb_ketQua.Clear();

            cb_lop.SelectedIndex = -1;

            rb_nam.IsChecked = false;
            rb_nu.IsChecked = false;
            rb_khac.IsChecked = false;

            rb_gd_ratTot.IsChecked = false;
            rb_gd_tot.IsChecked = false;
            rb_gd_tb.IsChecked = false;
            rb_gd_kem.IsChecked = false;

            rb_csvc_ratHL.IsChecked = false;
            rb_csvc_hl.IsChecked = false;
            rb_csvc_bt.IsChecked = false;
            rb_csvc_khl.IsChecked = false;

            rb_online_co.IsChecked = false;
            rb_online_khong.IsChecked = false;
            rb_online_tuy.IsChecked = false;

            cb_giamTai.IsChecked = false;
            cb_thucHanh.IsChecked = false;
            cb_wifi.IsChecked = false;
            cb_giangVien.IsChecked = false;

            tab_main.SelectedIndex = 0;
        }

        private void btnClick_thoat(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Bạn có chắc muốn thoát?",
                "Xác nhận",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
