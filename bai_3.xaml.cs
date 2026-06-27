using System;
using System.Windows;
using System.Windows.Controls;

namespace Tuan_4
{
    public partial class bai_3 : Window
    {
        private UcSinhVien ucSinhVien;
        private TreeViewItem rootSinhVien;

        public bai_3()
        {
            InitializeComponent();

            rootSinhVien = new TreeViewItem();
            rootSinhVien.Header = "Danh sách sinh viên";
            rootSinhVien.IsExpanded = true;

            treeSinhVien.Items.Add(rootSinhVien);

            MainContent.Content = new ucLopHoc();
        }

        private void MenuClick_sinhvien(object sender, RoutedEventArgs e)
        {
            ucSinhVien = new UcSinhVien();
            ucSinhVien.LuuClicked += UcSinhVien_LuuClicked;
            ucSinhVien.XoaClicked += UcSinhVien_XoaClicked;

            MainContent.Content = ucSinhVien;
        }

        private void MenuClick_lophoc(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ucLopHoc();
        }

        private bool KiemTraTrungMaSinhVien(string maSV)
        {
            foreach (TreeViewItem item in rootSinhVien.Items)
            {
                string[] tach = item.Header.ToString().Split('-');
                if (tach.Length < 2) continue;

                string maDaCo = tach[0].Trim();

                if (maDaCo.Equals(maSV, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private void ThemSinhVien()
        {
            if (ucSinhVien == null) return;

            string maSV = ucSinhVien.GetMaSinhVien();
            string hoTen = ucSinhVien.GetHoTen();
            string ngaySinh = ucSinhVien.GetDate();
            string gioiTinh = ucSinhVien.GetGioiTinh();
            string soThich = ucSinhVien.GetHobby();
            string lop = ucSinhVien.GetClassName();

            if (string.IsNullOrWhiteSpace(maSV) || string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show("Mã sinh viên và họ tên không được để trống");
                return;
            }

            if (ngaySinh == null)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh hợp lệ");
                return;
            }

            if (gioiTinh == null)
            {
                MessageBox.Show("Vui lòng chọn giới tính");
                return;
            }

            if (KiemTraTrungMaSinhVien(maSV))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại");
                return;
            }

            TreeViewItem item = new TreeViewItem();
            item.Header = maSV + " - " + hoTen;
            item.Tag = ngaySinh + "|" + gioiTinh + "|" + soThich + "|" + lop;

            rootSinhVien.Items.Add(item);

            MessageBox.Show("Thêm sinh viên thành công");

            ucSinhVien.ClearForm();
            ucSinhVien.SetSaveEnabled(true);
        }

        private void XoaSinhVien()
        {
            TreeViewItem item = treeSinhVien.SelectedItem as TreeViewItem;

            if (item == null || item == rootSinhVien)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Bạn có muốn xóa sinh viên này không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                rootSinhVien.Items.Remove(item);

                if (ucSinhVien != null)
                {
                    ucSinhVien.ClearForm();
                    ucSinhVien.SetSaveEnabled(true);
                }
            }
        }

        private void UcSinhVien_LuuClicked(object sender, EventArgs e)
        {
            ThemSinhVien();
        }

        private void UcSinhVien_XoaClicked(object sender, EventArgs e)
        {
            XoaSinhVien();
        }

        private void btnClick_luu(object sender, RoutedEventArgs e)
        {
            ThemSinhVien();
        }

        private void btnClick_xoa(object sender, RoutedEventArgs e)
        {
            XoaSinhVien();
        }

        private void btnClick_thoat(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void treeSinhVien_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = treeSinhVien.SelectedItem as TreeViewItem;

            if (item == null || ucSinhVien == null) return;
            if (item == rootSinhVien) return;

            string[] tachHeader = item.Header.ToString().Split('-');
            if (tachHeader.Length < 2) return;

            string maSV = tachHeader[0].Trim();
            string hoTen = tachHeader[1].Trim();

            string[] tachTag = item.Tag.ToString().Split('|');
            if (tachTag.Length < 4) return;

            string ngaySinh = tachTag[0];
            string gioiTinh = tachTag[1];
            string soThich = tachTag[2];
            string lop = tachTag[3];

            ucSinhVien.SetData(maSV, hoTen, ngaySinh, gioiTinh, soThich, lop);
            ucSinhVien.SetSaveEnabled(false);
        }
    }
}