using System;
using System.Windows;
using System.Windows.Controls;

namespace Tuan_4
{
    public partial class bai_4 : Window
    {
        private ucHoSoNhanVien ucNhanVien;
        private TreeViewItem phong1;
        private TreeViewItem phong2;
        private TreeViewItem nhanVienDangChon;

        public bai_4()
        {
            InitializeComponent();

            ucNhanVien = new ucHoSoNhanVien();
            ucNhanVien.ThemClicked += UcNhanVien_ThemClicked;
            ucNhanVien.SuaClicked += UcNhanVien_SuaClicked;
            ucNhanVien.XoaClicked += UcNhanVien_XoaClicked;
            ucNhanVien.LamMoiClicked += UcNhanVien_LamMoiClicked;

            MainContent.Content = ucNhanVien;

            KhoiTaoDuLieu();
            ucNhanVien.SetTrangThaiThemMoi();
        }

        private void KhoiTaoDuLieu()
        {
            phong1 = new TreeViewItem();
            phong1.Header = "Kế toán";
            phong1.IsExpanded = true;

            TreeViewItem nv1 = new TreeViewItem();
            nv1.Header = "NV01 - Nguyễn Văn A";
            nv1.Tag = "123 Nguyễn Trãi|0909000001|Kế toán";
            phong1.Items.Add(nv1);

            phong2 = new TreeViewItem();
            phong2.Header = "Nhân sự";
            phong2.IsExpanded = true;

            TreeViewItem nv2 = new TreeViewItem();
            nv2.Header = "NV02 - Trần Thị B";
            nv2.Tag = "45 Lê Lợi|0909000002|Nhân sự";
            phong2.Items.Add(nv2);

            treephongban.Items.Add(phong1);
            treephongban.Items.Add(phong2);

            ucNhanVien.SetPhongBan(phong1.Header.ToString());
        }

        private void MenuClick_Thoat(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool KiemTraThongTinRong()
        {
            return string.IsNullOrWhiteSpace(ucNhanVien.GetMaSo()) ||
                   string.IsNullOrWhiteSpace(ucNhanVien.GetHoTen()) ||
                   string.IsNullOrWhiteSpace(ucNhanVien.GetDiaChi()) ||
                   string.IsNullOrWhiteSpace(ucNhanVien.GetDienThoai()) ||
                   string.IsNullOrWhiteSpace(ucNhanVien.GetPhongBan());
        }

        private bool KiemTraTrungMaNhanVien(string maMoi)
        {
            foreach (TreeViewItem phong in treephongban.Items)
            {
                foreach (TreeViewItem nv in phong.Items)
                {
                    string[] tach = nv.Header.ToString().Split('-');
                    if (tach.Length < 2) continue;

                    string maCu = tach[0].Trim();
                    if (maCu.Equals(maMoi, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            return false;
        }

        private TreeViewItem TimPhongBanTheoTen(string tenPhong)
        {
            foreach (TreeViewItem phong in treephongban.Items)
            {
                if (phong.Header.ToString().Equals(tenPhong, StringComparison.OrdinalIgnoreCase))
                    return phong;
            }
            return null;
        }

        private void UcNhanVien_ThemClicked(object sender, EventArgs e)
        {
            if (KiemTraThongTinRong())
            {
                MessageBox.Show("Toàn bộ thông tin nhân viên không được để trống");
                return;
            }

            string ma = ucNhanVien.GetMaSo();
            string ten = ucNhanVien.GetHoTen();
            string diaChi = ucNhanVien.GetDiaChi();
            string dienThoai = ucNhanVien.GetDienThoai();
            string phongBan = ucNhanVien.GetPhongBan();

            if (KiemTraTrungMaNhanVien(ma))
            {
                MessageBox.Show("Không được trùng mã nhân viên");
                return;
            }

            TreeViewItem phong = TimPhongBanTheoTen(phongBan);
            if (phong == null)
            {
                MessageBox.Show("Phòng ban không tồn tại trên TreeView");
                return;
            }

            TreeViewItem nv = new TreeViewItem();
            nv.Header = ma + " - " + ten;
            nv.Tag = diaChi + "|" + dienThoai + "|" + phongBan;

            phong.Items.Add(nv);
            phong.IsExpanded = true;

            MessageBox.Show("Thêm nhân viên thành công");

            nhanVienDangChon = null;
            ucNhanVien.ClearForm();
            ucNhanVien.SetPhongBan(phongBan);
            ucNhanVien.SetTrangThaiThemMoi();
        }

        private void UcNhanVien_SuaClicked(object sender, EventArgs e)
        {
            if (nhanVienDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa");
                return;
            }

            if (KiemTraThongTinRong())
            {
                MessageBox.Show("Toàn bộ thông tin nhân viên không được để trống");
                return;
            }

            string ma = ucNhanVien.GetMaSo();
            string ten = ucNhanVien.GetHoTen();
            string diaChi = ucNhanVien.GetDiaChi();
            string dienThoai = ucNhanVien.GetDienThoai();
            string phongBan = ucNhanVien.GetPhongBan();

            nhanVienDangChon.Header = ma + " - " + ten;
            nhanVienDangChon.Tag = diaChi + "|" + dienThoai + "|" + phongBan;

            MessageBox.Show("Cập nhật nhân viên thành công");
        }

        private void UcNhanVien_XoaClicked(object sender, EventArgs e)
        {
            if (nhanVienDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Bạn có muốn xóa nhân viên này không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                TreeViewItem phong = nhanVienDangChon.Parent as TreeViewItem;
                if (phong != null)
                {
                    phong.Items.Remove(nhanVienDangChon);

                    nhanVienDangChon = null;
                    ucNhanVien.ClearForm();
                    ucNhanVien.SetPhongBan(phong1.Header.ToString());
                    ucNhanVien.SetTrangThaiThemMoi();
                    phong1.IsSelected = true;
                }
            }
        }

        private void UcNhanVien_LamMoiClicked(object sender, EventArgs e)
        {
            nhanVienDangChon = null;
            ucNhanVien.ClearForm();

            TreeViewItem phongDauTien = treephongban.Items[0] as TreeViewItem;
            if (phongDauTien != null)
            {
                ucNhanVien.SetPhongBan(phongDauTien.Header.ToString());
            }

            ucNhanVien.SetTrangThaiThemMoi();
        }

        private void treephongban_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem item = treephongban.SelectedItem as TreeViewItem;
            if (item == null) return;

            if (item.Parent == treephongban)
            {
                ucNhanVien.SetPhongBan(item.Header.ToString());
                nhanVienDangChon = null;
                return;
            }

            TreeViewItem phong = item.Parent as TreeViewItem;
            if (phong == null) return;

            string[] tachHeader = item.Header.ToString().Split('-');
            if (tachHeader.Length < 2) return;

            string ma = tachHeader[0].Trim();
            string ten = tachHeader[1].Trim();

            string[] tachTag = item.Tag.ToString().Split('|');
            if (tachTag.Length < 3) return;

            string diaChi = tachTag[0];
            string dienThoai = tachTag[1];
            string phongBan = tachTag[2];

            ucNhanVien.SetData(ma, ten, diaChi, dienThoai, phongBan);
            ucNhanVien.SetTrangThaiDangChon();

            nhanVienDangChon = item;
        }

        private void btnClick_ThemPhongBan(object sender, RoutedEventArgs e)
        {
            string tenPhong = txt_themphongban.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenPhong))
            {
                MessageBox.Show("Tên phòng ban không được để trống");
                return;
            }

            foreach (TreeViewItem phong in treephongban.Items)
            {
                if (phong.Header.ToString().Equals(tenPhong, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Không được trùng tên phòng ban đã có trên TreeView");
                    return;
                }
            }

            TreeViewItem phongMoi = new TreeViewItem();
            phongMoi.Header = tenPhong;
            phongMoi.IsExpanded = true;

            treephongban.Items.Add(phongMoi);

            MessageBox.Show("Thêm phòng ban thành công");
            txt_themphongban.Clear();
        }

        private void btnClick_XoaPhongBan(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = treephongban.SelectedItem as TreeViewItem;

            if (item == null || item.Parent != treephongban)
            {
                MessageBox.Show("Người dùng phải chọn phòng ban trên TreeView");
                return;
            }

            if (item.Items.Count > 0)
            {
                MessageBox.Show("Không thể xóa phòng ban khi vẫn còn nhân viên");
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Bạn có muốn xóa phòng ban này không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                treephongban.Items.Remove(item);
            }
        }
    }
}