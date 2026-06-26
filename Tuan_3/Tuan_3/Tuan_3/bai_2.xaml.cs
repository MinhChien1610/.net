using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
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
    /// Interaction logic for bai_2.xaml
    /// </summary>
    public partial class bai_2 : Window
    {
        public bai_2()
        {
            InitializeComponent();
        }

        private string GetName()
        {
            string name = txt_hoVaTen.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
                return null;
            return name;
        }

        private string GetJob()
        {
            string job = txt_ngheNghiep.Text.Trim();

            if(string.IsNullOrWhiteSpace(job))
                return null;
            return job;
        }

        private string GetGioiTinh()
        {
            if (rb_nam.IsChecked == true)
                return "Nam";

            if (rb_nu.IsChecked == true)
                return "Nữ";

            return null;
        }

        private string GetDate()
        {
            DateTime date;

            if (dp_date.SelectedDate != null)
                return dp_date.SelectedDate.Value.ToString("dd/MM/yyyy");

            if (DateTime.TryParse(dp_date.Text, out date))
                return date.ToString("dd/MM/yyyy");

            return null;
        }


        private string GetNational()
        {
            if(cbb_quocTich.SelectedIndex >=0)
                return cbb_quocTich.Text.Trim();

            return "Chưa chọn";
        }

        private string GetHobby()
        {
            string result = "";
            if (cb_docSach.IsChecked == true)
                result += "Đọc sách, ";

            if (cb_ngheNhac.IsChecked == true)
                result += "Nghe nhạc, ";

            if (cb_theThao.IsChecked == true)
                result += "Thể thao, ";

            if (cb_duLich.IsChecked == true)
                result += "Du lịch, ";

            if (string.IsNullOrWhiteSpace(result))
                return "Chưa chọn sở thích";

            return result.TrimEnd(' ', ',');
        }


        private string GetSkill()
        {
            if (lb_kyNang.SelectedItems.Count == 0)
                return "Chưa chọn kỹ năng";

            string result = "";

            foreach (ListBoxItem item in lb_kyNang.SelectedItems)
            {
                result += item.Content + ", ";
            }

            return result.TrimEnd(' ', ',');
        }


        private void btnClick_ThongTin(object sender, RoutedEventArgs e)
        {
            string name = GetName();
            if (name == null)
            {
                MessageBox.Show("Vui lòng nhập họ tên!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                txt_hoVaTen.Focus();
                return;
            }    
                


            string gioiTinh = GetGioiTinh();
            if (gioiTinh == null)
            {
                MessageBox.Show("Vui lòng chọn giới tính!",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                rb_nam.Focus();
                return;
            }    
                

            string date = GetDate();
            if (date == null)
            {
                MessageBox.Show("Ngày/tháng/năm sinh không hợp lệ",
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                dp_date.Focus();
                return;
            }
            
            string job = GetJob();
            string national = GetNational();
            string hobby = GetHobby();
            string skills = GetSkill();

            txt_hoVaTen_Text.Text = name;
            txt_gioiTinh_Text.Text = gioiTinh;
            txt_ngaySinh_Text.Text = date;
            txt_quocTich_Text.Text = national;
            txt_ngheNghiep_Text.Text = job;
            txt_soThich_Text.Text = hobby;
            txt_kyNang_Text.Text = skills;


            ti_xemThongTin.IsSelected = true;
        }

        private void btnClick_Thoat(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn thoát?",
                                                      "Xác nhận",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                this.Close();
        }
    }
}
