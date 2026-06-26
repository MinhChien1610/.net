using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for bai_4.xaml
    /// </summary>
    public partial class bai_4 : Window
    {
        public bai_4()
        {
            InitializeComponent();
        }

        private string GetName()
        {
            string name = txt_name.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
                return null;

            return name;
        }

        private string GetPhone()
        {
            string phone = txt_phone.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
                return null;

            string pattern = @"^0\d{9}$";

            if (!Regex.IsMatch(phone, pattern))
                return null;

            return phone;
        }


        private string GetDrink()
        {
            if(cbb_drink.SelectedIndex >=0)
                return cbb_drink.Text.Trim();

            return "Chưa chọn";
        }

        private string GetSize()
        {
            if (rb_sizeM.IsChecked == true)
                return "M (500ml)";

            if (rb_sizeL.IsChecked == true)
                return "L (700ml) + 5.000đ";

            return null;
        }


        private string GetTopping()
        {
            List<string> toppings = new List<string>();

            if (cb_tranChau.IsChecked == true)
                toppings.Add("Trân châu");

            if (cb_pudding.IsChecked == true)
                toppings.Add("Pudding");

            if (cb_thachTraiCay.IsChecked == true)
                toppings.Add("Thạch trái cây");

            if (cb_kemCheese.IsChecked == true)
                toppings.Add("Kem cheese");

            if (cb_thachDua.IsChecked == true)
                toppings.Add("Thạch dừa");

            if (toppings.Count == 0)
                return "Không topping";

            return string.Join(", ", toppings);
        }

        private double TinhTienSize()
        {
            if (GetSize() == "M (500ml)")
                return 35000;
            return 40000;
        }


        private double TinhTienTopping()
        {
            double sum = 0.0;

            if (cb_tranChau.IsChecked == true)
                sum += 7000;

            if (cb_pudding.IsChecked == true)
                sum += 7000;

            if (cb_thachTraiCay.IsChecked == true)
                sum += 6000;

            if (cb_kemCheese.IsChecked == true)
                sum += 10000;

            if (cb_thachDua.IsChecked == true)
                sum += 8000;

            return sum;
        }

        private double TongTien()
        {
            return TinhTienSize() + TinhTienTopping();
        }

        private string CreateOrderItem()
        {
            string drink = GetDrink();
            string size = GetSize();
            string topping = GetTopping();
            string note = txt_ghiChu.Text.Trim();

            if (size == null)
                return null;

            string item = $"{drink} - Size {size} - {topping}";

            if (!string.IsNullOrWhiteSpace(note))
                item += $"\nGhi chú: {note}";

            return item;
        }


        private void btnClick_themDon(object sender, RoutedEventArgs e)
        {
            string name = GetName();
            if (name == null)
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_name.Focus();
                return;
            }

            string phone = GetPhone();
            if (phone == null)
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Thông báo",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                txt_phone.Focus();
                return;
            }

            string item = CreateOrderItem();
            if(item == null)
            {
                MessageBox.Show("Vui lòng chọn size!");
                return;
            }

            int stt = lb_donHang.Items.Count + 1;
            lb_donHang.Items.Add($"{stt}. {item}");
            ThanhTien();
            txt_ghiChu.Clear();
        }

        private void btnClick_xoaDon(object sender, RoutedEventArgs e)
        {
            lb_donHang.Items.Clear();
            tb_tongTien.Text = "0 đ";
        }


        private void btnClick_inHoaDon(object sender, RoutedEventArgs e)
        {
            if (lb_donHang.Items.Count == 0)
            {
                MessageBox.Show("Bạn chưa thêm món nào!");
                return;
            }

            tb_name.Text = txt_name.Text;
            tb_phone.Text = txt_phone.Text;

            lb_chiTiet.Items.Clear();
            foreach (var item in lb_donHang.Items)
            {
                lb_chiTiet.Items.Add(item);
            }

            ThanhTien();
        }


        private void btnClick_huyDon(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn hủy toàn bộ đơn hàng?",
                "Xác nhận",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                txt_name.Clear();
                txt_phone.Clear();
                txt_ghiChu.Clear();

                lb_donHang.Items.Clear();
                lb_chiTiet.Items.Clear();

                tb_tongTien.Text = "0 đ";
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ti_xemThongTin.IsSelected)
            {
                tb_name.Text = txt_name.Text;
                tb_phone.Text = txt_phone.Text;

                lb_chiTiet.Items.Clear();
                foreach (var item in lb_donHang.Items)
                {
                    lb_chiTiet.Items.Add(item);
                }

                ThanhTien();
            }
        }
        private void btnClick_datHang(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Đặt hàng thành công! Cảm ơn quý khách.");

            txt_name.Clear();
            txt_phone.Clear();
            txt_ghiChu.Clear();

            rb_sizeM.IsChecked = false;
            rb_sizeL.IsChecked = false;

            cb_tranChau.IsChecked = false;
            cb_pudding.IsChecked = false;
            cb_thachTraiCay.IsChecked = false;
            cb_kemCheese.IsChecked = false;
            cb_thachDua.IsChecked = false;

            cbb_drink.SelectedIndex = -1;

            lb_donHang.Items.Clear();
            lb_chiTiet.Items.Clear();

            tb_tongTien.Text = "0 đ";
        }
        private void ThanhTien()
        {
            tb_tongTien.Text = TongTien().ToString("N0") + " đ";
        }
    }
}
