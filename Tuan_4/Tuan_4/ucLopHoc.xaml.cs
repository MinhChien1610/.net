using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tuan_4
{
    /// <summary>
    /// Interaction logic for ucLopHoc.xaml
    /// </summary>
    public partial class ucLopHoc : UserControl
    {
        public ucLopHoc()
        {
            InitializeComponent();
        }

        private void btnClick_luu(object sender, RoutedEventArgs e)
        {
            string maLop = txt_malop.Text.Trim();
            string tenLop = txt_tenlop.Text.Trim();
            string siSo = txt_siso.Text.Trim();
            string giangVien = txt_giangvien.Text.Trim();

            if (string.IsNullOrWhiteSpace(maLop) ||
                string.IsNullOrWhiteSpace(tenLop) ||
                string.IsNullOrWhiteSpace(siSo) ||
                string.IsNullOrWhiteSpace(giangVien))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin lớp học");
                return;
            }

            MessageBox.Show(
                "Mã lớp: " + maLop +
                "\nTên lớp: " + tenLop +
                "\nSĩ số: " + siSo +
                "\nGiảng viên cố vấn: " + giangVien,
                "Thông tin lớp học");
        }

        private void btnClick_xoa(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Bạn có muốn xóa dữ liệu không?",
                "Xác nhận",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                txt_malop.Clear();
                txt_tenlop.Clear();
                txt_siso.Clear();
                txt_giangvien.Clear();
                txt_malop.Focus();
            }
        }

        private void btnClick_lammoi(object sender, RoutedEventArgs e)
        {
            txt_malop.Clear();
            txt_tenlop.Clear();
            txt_siso.Clear();
            txt_giangvien.Clear();
            txt_malop.Focus();
        }
    }
}
