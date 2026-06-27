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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tuan_4
{
    /// <summary>
    /// Interaction logic for bai_2.xaml
    /// </summary>
    public partial class bai_2 : Window
    {
        public bai_2()
        {
            InitializeComponent();
            AddPhongBan("Giám đốc", "BGĐ");
            AddPhongBan("Kế hoạch", "PKH");
            AddPhongBan("Kế toán", "PKT");
        }

        private string GetName()
        {
            string maphong = txt_maphong.Text.Trim();

            string tenphong = txt_tenphong.Text.Trim();

            if (string.IsNullOrEmpty(maphong) || string.IsNullOrWhiteSpace(tenphong))
                return null;

            return tenphong + " - " + maphong;
        }

        private void Item_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;

            if (item == null) return;

            MessageBoxResult result = MessageBox.Show("Bạn có muốn xóa phòng ban này không?","Xác nhận",
                                      MessageBoxButton.YesNo,
                                      MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                treePhongBan.Items.Remove(item);
            }
        }

        private void treePhongBan_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = treePhongBan.SelectedItem as TreeViewItem;
            if (item == null) return;

            string name = item.Header.ToString();
            string[] tach = name.Split('-');

            if (tach.Length < 2) return;

            txt_phongbanchon.Text = tach[0].Trim();
            txt_maphongchon.Text = tach[1].Trim();
        }

        private void AddPhongBan(string ten, string ma)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = ten + " - " + ma;
            item.Margin = new Thickness(0, 5, 0, 0);
            item.MouseRightButtonUp += Item_MouseRightButtonUp;

            treePhongBan.Items.Add(item);
        }

        private bool KiemTraTrung(string tenMoi, string maMoi)
        {
            foreach (TreeViewItem item in treePhongBan.Items)
            {
                string data = item.Header.ToString();
                string[] tach = data.Split('-');

                if (tach.Length < 2) continue;

                string tenDaCo = tach[0].Trim();
                string maDaCo = tach[1].Trim();

                if (maDaCo.Equals(maMoi, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mã phòng đã tồn tại, không thể thêm");
                    txt_maphong.Focus();
                    txt_maphong.SelectAll();
                    return true;
                }

                if (tenDaCo.Equals(tenMoi, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Tên phòng ban đã tồn tại, không thể thêm");
                    txt_tenphong.Focus();
                    txt_tenphong.SelectAll();
                    return true;
                }
            }

            return false;
        }

        private void btnClick_themphong(object sender, RoutedEventArgs e)
        {
            string name = GetName();
            if (name == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            string[] tach = name.Split('-');
            if (tach.Length < 2) return;

            string ten = tach[0].Trim();
            string ma = tach[1].Trim();

            if (KiemTraTrung(ten, ma))
                return;

            AddPhongBan(ten, ma);

            txt_maphong.Clear();
            txt_tenphong.Clear();
            txt_maphong.Focus();
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát chương trình không?","Xác nhận thoát",
                                      MessageBoxButton.YesNo,
                                      MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
