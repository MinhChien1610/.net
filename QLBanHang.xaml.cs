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
using Tuan_7.ViewModels;

namespace Tuan_7.Views
{
    /// <summary>
    /// Interaction logic for QLBanHang.xaml
    /// </summary>
    public partial class QLBanHang : Window
    {
        public QLBanHang()
        {
            InitializeComponent();
            DataContext = new QLBanHangViewModel();
        }
    }
}
