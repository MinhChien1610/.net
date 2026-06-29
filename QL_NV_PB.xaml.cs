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
using Tuan_6.ViewModels;

namespace Tuan_6.View
{
    /// <summary>
    /// Interaction logic for QL_NV_PB.xaml
    /// </summary>
    public partial class QL_NV_PB : Window
    {
        public QL_NV_PB()
        {
            InitializeComponent();
            DataContext = new QL_NV_PBViewModel();
        }
    }
}
