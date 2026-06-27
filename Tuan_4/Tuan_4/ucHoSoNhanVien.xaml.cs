using System;
using System.Windows;
using System.Windows.Controls;

namespace Tuan_4
{
    public partial class ucHoSoNhanVien : UserControl
    {
        public event EventHandler ThemClicked;
        public event EventHandler SuaClicked;
        public event EventHandler XoaClicked;
        public event EventHandler LamMoiClicked;

        public ucHoSoNhanVien()
        {
            InitializeComponent();
        }

        public string GetMaSo()
        {
            return txt_maso.Text.Trim();
        }

        public string GetHoTen()
        {
            return txt_hoten.Text.Trim();
        }

        public string GetDiaChi()
        {
            return txt_diachi.Text.Trim();
        }

        public string GetDienThoai()
        {
            return txt_dienthoai.Text.Trim();
        }

        public string GetPhongBan()
        {
            return txt_phongban.Text.Trim();
        }

        public void SetPhongBan(string tenPhong)
        {
            txt_phongban.Text = tenPhong;
        }

        public void SetData(string maSo, string hoTen, string diaChi, string dienThoai, string phongBan)
        {
            txt_maso.Text = maSo;
            txt_hoten.Text = hoTen;
            txt_diachi.Text = diaChi;
            txt_dienthoai.Text = dienThoai;
            txt_phongban.Text = phongBan;
        }

        public void ClearForm()
        {
            txt_maso.Clear();
            txt_hoten.Clear();
            txt_diachi.Clear();
            txt_dienthoai.Clear();
            txt_maso.Focus();
        }

        public void SetTrangThaiThemMoi()
        {
            txt_maso.IsReadOnly = false;
            btn_them.IsEnabled = true;
            btn_sua.IsEnabled = false;
            btn_xoa.IsEnabled = false;
        }

        public void SetTrangThaiDangChon()
        {
            txt_maso.IsReadOnly = true;
            btn_them.IsEnabled = false;
            btn_sua.IsEnabled = true;
            btn_xoa.IsEnabled = true;
        }

        private void btnClick_them(object sender, RoutedEventArgs e)
        {
            ThemClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnClick_sua(object sender, RoutedEventArgs e)
        {
            SuaClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnClick_xoa(object sender, RoutedEventArgs e)
        {
            XoaClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnClick_lammoi(object sender, RoutedEventArgs e)
        {
            LamMoiClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}