using System;
using System.Windows;
using System.Windows.Controls;

namespace Tuan_2
{
    /// <summary>
    /// Interaction logic for bai_4.xaml
    /// </summary>
    public partial class bai_4 : Window
    {
        public bai_4()
        {
            InitializeComponent();
            UpDate();
            UpDate_Giai();
        }

        private void UpDate()
        {
            if (txt_hesoC == null) return;

            if (ptBac1.IsChecked == true)
            {
                txt_hesoC.IsEnabled = false;
                txt_hesoC.Clear();
            }
            else
            {
                txt_hesoC.IsEnabled = true;
            }
        }

        private void UpDate_Giai()
        {
            if (btn_Giai == null || txt_hesoA == null || txt_hesoB == null) 
                return;

            double temp;

            if (!double.TryParse(txt_hesoA.Text, out temp))
            {
                btn_Giai.IsEnabled = false;
                return;
            }

            if (!double.TryParse(txt_hesoB.Text, out temp))
            {
                btn_Giai.IsEnabled = false;
                return;
            }

            if (ptBac2.IsChecked == true)
            {
                if (!double.TryParse(txt_hesoC.Text, out temp))
                {
                    btn_Giai.IsEnabled = false;
                    return;
                }
            }

            btn_Giai.IsEnabled = true;
        }

        private void Check_LoaiPT(object sender, RoutedEventArgs e)
        {
            UpDate();
            if (txt_hesoKQ != null)
                txt_hesoKQ.Clear();

            UpDate_Giai();
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpDate_Giai();
        }

        private string GiaiPTBacNhat(double a, double b)
        {
            if (a == 0)
            {
                if (b == 0)
                    return "Phương trình có vô số nghiệm";
                else
                    return "Phương trình vô nghiệm";
            }
            else
            {
                double x = -b / a;
                return "Phương trình có nghiệm:\n x = " + x;
            }
        }


        private string GiaiPTBacHai(double a, double b, double c)
        {
            if (a == 0)
                return GiaiPTBacNhat(b, c);

            double delta = b * b - 4 * a * c;

            if (delta < 0)
            {
                return "Phương trình vô nghiệm";
            }
            else if (delta == 0)
            {
                double x = -b / (2 * a);
                return $"Phương trình có nghiệm kép x = {x}";
            }
            else
            {
                double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                return $"Phương trình có 2 nghiệm phân biệt:\n x1 = {x1:F2}\n x2 = {x2:F2}";
            }
        }


        private void btnClick_Giai(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txt_hesoA.Text, out double a) ||
                !double.TryParse(txt_hesoB.Text, out double b))
            {
                txt_hesoKQ.Text = "Vui lòng nhập a và b hợp lệ.";
                btn_Giai.IsEnabled = false;
                return;
            }

            if (ptBac1.IsChecked == true)
            {
                txt_hesoKQ.Text = GiaiPTBacNhat(a, b);
            }
            else
            {
                if (!double.TryParse(txt_hesoC.Text, out double c))
                {
                    txt_hesoKQ.Text = "Vui lòng nhập c hợp lệ.";
                    btn_Giai.IsEnabled = false;
                    return;
                }

                txt_hesoKQ.Text = GiaiPTBacHai(a, b, c);
            }

            btn_Giai.IsEnabled = false;
        }

        private void btnClick_Thoat(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?","Thông báo",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Close();
        }
    }
}
