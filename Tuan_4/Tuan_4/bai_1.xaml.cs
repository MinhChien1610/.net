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

namespace Tuan_4
{
    /// <summary>
    /// Interaction logic for bai_1.xaml
    /// </summary>
    public partial class bai_1 : Window
    {
        public bai_1()
        {
            InitializeComponent();
        }

        private void MenuClick_Nhanvien(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ucNhanVien();
        }

        private void MenuClick_Phongban(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ucPhongBan();
        }

    }
}
