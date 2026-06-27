using System;
using System.Windows;
using System.Windows.Controls;

namespace Tuan_4
{
    public partial class UcSinhVien : UserControl
    {
        public event EventHandler LuuClicked;
        public event EventHandler XoaClicked;

        public UcSinhVien()
        {
            InitializeComponent();
        }

        public string GetMaSinhVien()
        {
            return txt_masinhvien.Text.Trim();
        }

        public string GetHoTen()
        {
            return txt_hoten.Text.Trim();
        }

        public string GetDate()
        {
            DateTime date;

            if (dp_ngaysinh.SelectedDate != null)
                return dp_ngaysinh.SelectedDate.Value.ToString("dd/MM/yyyy");

            if (DateTime.TryParse(dp_ngaysinh.Text, out date))
                return date.ToString("dd/MM/yyyy");

            return null;
        }

        public string GetGioiTinh()
        {
            if (rb_nam.IsChecked == true) return "Nam";
            if (rb_nu.IsChecked == true) return "Nữ";
            return null;
        }

        public string GetHobby()
        {
            string result = "";

            if (cb_thethao.IsChecked == true) result += "Thể thao, ";
            if (cb_amnhac.IsChecked == true) result += "Âm nhạc, ";
            if (cb_dulich.IsChecked == true) result += "Du lịch, ";

            if (string.IsNullOrWhiteSpace(result))
                return "Chưa chọn sở thích";

            return result.TrimEnd(' ', ',');
        }

        public string GetClassName()
        {
            if (cbb_lophoc.SelectedIndex > 0)
                return cbb_lophoc.Text.Trim();

            return "Chưa chọn";
        }

        public void SetData(string maSV, string hoTen, string ngaySinh, string gioiTinh, string soThich, string lop)
        {
            txt_masinhvien.Text = maSV;
            txt_hoten.Text = hoTen;

            DateTime ngay;
            if (DateTime.TryParse(ngaySinh, out ngay))
                dp_ngaysinh.SelectedDate = ngay;
            else
                dp_ngaysinh.Text = ngaySinh;

            rb_nam.IsChecked = gioiTinh == "Nam";
            rb_nu.IsChecked = gioiTinh == "Nữ";

            cb_thethao.IsChecked = soThich.Contains("Thể thao");
            cb_amnhac.IsChecked = soThich.Contains("Âm nhạc");
            cb_dulich.IsChecked = soThich.Contains("Du lịch");

            cbb_lophoc.Text = lop;
        }

        public void ClearForm()
        {
            txt_masinhvien.Clear();
            txt_hoten.Clear();
            dp_ngaysinh.SelectedDate = null;
            rb_nam.IsChecked = false;
            rb_nu.IsChecked = false;
            cb_thethao.IsChecked = false;
            cb_amnhac.IsChecked = false;
            cb_dulich.IsChecked = false;
            cbb_lophoc.SelectedIndex = 0;
            txt_masinhvien.Focus();
        }

        public void SetSaveEnabled(bool value)
        {
            btn_luu.IsEnabled = value;
        }

        private void btnClick_luu(object sender, RoutedEventArgs e)
        {
            LuuClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnClick_xoa(object sender, RoutedEventArgs e)
        {
            XoaClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnClick_thoat(object sender, RoutedEventArgs e)
        {
            ClearForm();
            SetSaveEnabled(true);
        }
    }
}