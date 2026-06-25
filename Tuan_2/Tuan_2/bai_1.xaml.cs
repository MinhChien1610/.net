using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tuan_2
{
    public partial class bai_1 : Window
    {
        public bai_1()
        {
            InitializeComponent();
        }

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
                txtNameError.Text = "Vui lòng nhập họ tên";
            else
                txtNameError.Text = string.Empty;
        }

        private void txtYear_LostFocus(object sender, RoutedEventArgs e)
        {
            int year;
            int currentYear = DateTime.Now.Year;

            if (!int.TryParse(txtYear.Text, out year))
                txtYearError.Text = "Năm sinh phải là số";

            else
            {
                if (year < 0)
                    txtYearError.Text = "Năm sinh không là số âm!";

                else if (year > currentYear)
                    txtYearError.Text = "Năm sinh không \n lớn hơn năm hiện tại!";

                else
                    txtYearError.Text = string.Empty;
            }
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtName.Focus();
                return;
            }

            // Kiểm tra năm sinh
            if (!int.TryParse(txtYear.Text, out int year))
            {
                MessageBox.Show("Năm sinh phải là số!", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtYear.Focus();
                return;
            }

            int currentYear = DateTime.Now.Year;

            if (year < 0 || year > currentYear)
            {
                MessageBox.Show("Năm sinh không hợp lệ!", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtYear.Focus();
                return;
            }

            int age = currentYear - year;

            string message =
                "Họ tên: " + txtName.Text.Trim() + "\n" +
                "Tuổi: " + age;

            MessageBox.Show(message, "Thông tin",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            txtYear.Clear();
            txtName.Focus();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}
