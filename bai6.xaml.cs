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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tuan_10.ViewModels;

namespace Tuan_10.Views
{
    /// <summary>
    /// Interaction logic for bai6.xaml
    /// </summary>
    public partial class bai6 : UserControl
    {
        public bai6()
        {
            InitializeComponent();
            DataContext = new Bai6ViewModel();
        }
    }
}
