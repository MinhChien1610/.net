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
using Tuan_10.ViewModels;
using Tuan_10.Views;

namespace Tuan_10.Views
{
    /// <summary>
    /// Interaction logic for bai2.xaml
    /// </summary>
    public partial class bai2 : Window
    {
        public bai2()
        {
            InitializeComponent();
            DataContext = new Bai2ViewModel();
        }
    }
}
