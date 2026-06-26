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
    /// Interaction logic for bai_3.xaml
    /// </summary>
    public partial class bai_3 : Window
    {
        public bai_3()
        {
            InitializeComponent();
        }

        private string GetName()
        {
            string name = txt_name.Text.Trim();

            if (string.IsNullOrEmpty(name))
                return null;

            return name;
        }

        private string GetPhone()
        {
            string phone = txt_phone.Text.Trim();

            if (string.IsNullOrEmpty(phone)) 
                return null;

            return phone;
        }

        private string GetTable()
        {
            if (ccb_table.SelectedIndex >= 0)
                return ccb_table.Text.Trim();

            return "Chưa chọn";
        }

        private string GetFood()
        {
            if (lb_food.SelectedItems.Count == 0)
                return "Chưa chọn món";

            List<string> foods = new List<string>();

            foreach (ListBoxItem item in lb_food.SelectedItems)
                foods.Add(item.Content.ToString());

            return string.Join(Environment.NewLine, foods);
        }

        private void btnClick_xacNhan(object sender, RoutedEventArgs args)
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

            string table = GetTable();

            txt_infomation.Text =
                $"Khách hàng: {name}{Environment.NewLine}" +
                $"SĐT: {phone}{Environment.NewLine}" +
                $"Bàn: {table}";

            lb_listFood.Items.Clear();

            foreach (ListBoxItem item in lb_food.SelectedItems)
            {
              lb_listFood.Items.Add(item.Content.ToString());
            }
        }

        private void btnClick_deleteFood(object sender, RoutedEventArgs e)
        {
            if (lb_listFood.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            if (lb_listFood.Items.Count == 1)
            {
                var result = MessageBox.Show(
                    "Đây là món cuối cùng. Bạn có chắc muốn xóa?",
                    "Xác nhận",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                    return;
            }
            lb_listFood.Items.Remove(lb_listFood.SelectedItem);
        }

        private void btnClick_orderFood(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_infomation.Text) ||
                lb_listFood.Items.Count == 0)
            {
                MessageBox.Show("Vui lòng xác nhận thông tin trước!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Đặt món thành công!",
                            "Thông báo",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }
    }
}
