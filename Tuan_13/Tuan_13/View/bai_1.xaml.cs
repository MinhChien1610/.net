using System.Windows;
using Tuan_13.Report;

namespace Tuan_13.View
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DanhsachSVtheoLop rpt = new DanhsachSVtheoLop();
            myreport.ViewerCore.ReportSource = rpt;
        }
    }
}
