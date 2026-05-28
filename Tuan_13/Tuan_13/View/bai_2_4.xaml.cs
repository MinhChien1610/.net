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
using Tuan_13.ViewModels;

namespace Tuan_13.View
{
    /// <summary>
    /// Interaction logic for bai_2_4.xaml
    /// </summary>
    public partial class bai_2_4 : Window
    {
        public bai_2_4()
        {
            InitializeComponent();
            DataContext = new bai_2_4ViewModel();
        }
    }
}
