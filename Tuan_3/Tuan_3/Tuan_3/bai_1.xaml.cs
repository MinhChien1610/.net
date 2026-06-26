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
using System.Windows.Shapes;

namespace Tuan_3
{
    /// <summary>
    /// Interaction logic for bai_1.xaml
    /// </summary>
    public partial class bai_1 : Window
    {
        public bai_1()
        {
            InitializeComponent();
        }

        private string GetGioiTinh()
        {
            if (rb_nam.IsChecked == true)
                return "Nam";

            if (rb_nu.IsChecked == true)
                return "Nữ";

            return null;
        }

        private string GetName()
        {
            string hoDem = txt_hoDem.Text.Trim();
            string ten = txt_ten.Text.Trim();

            if (string.IsNullOrWhiteSpace(hoDem) || string.IsNullOrWhiteSpace(ten))
                return null;

            if (GetGioiTinh() == "Nam")
                return "Mr." + hoDem + " " + ten;

            else
                return "Mrs." + hoDem + " " + ten; 
        }
        

        private string Languages()
        {
            string result = "";

            if (cb_tiengAnh.IsChecked == true)
                result += "Tiếng Anh, ";

            if (cb_tiengTrung.IsChecked == true)
                result += "Tiếng Trung, ";

            if (string.IsNullOrEmpty(result))
                return "Chưa chọn ngôn ngữ";

            return result.TrimEnd(' ', ',');
        }

        private string GetCountryside()
        {
            if (cbb_queQuan.SelectedIndex >= 0)
                return cbb_queQuan.Text;
            return "Chưa chọn";
        }

        private void btnClick_thongTin(object sender, RoutedEventArgs e)
        {
            string name = GetName();

            if (name == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ họ tên", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_hoDem.Focus();
                return;
            }

            string gioiTinh = GetGioiTinh();

            if (gioiTinh == null)
            {
                MessageBox.Show("Vui lòng chọn giới tính", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                rb_nam.Focus();
                return;
            }

            string ngonNgu = Languages();
            string queQuan = GetCountryside();

            string showInfo = "Họ tên:"+ " " + name + "\n" + "Giới tính:" + " " + gioiTinh + "\n" + "Ngôn ngữ:" + " " + ngonNgu + "\n" + "Quê quán:" + " " + queQuan + "\n";

            MessageBox.Show(showInfo, "Thông tin", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnClick_nhapLai(object sender, RoutedEventArgs e)
        {
            txt_hoDem.Clear();
            txt_ten.Clear();

            rb_nam.IsChecked = false;
            rb_nu.IsChecked = false;

            cb_tiengAnh.IsChecked = false;
            cb_tiengTrung.IsChecked = false;

            txt_hoDem.Focus();
        }

    }
}
