using System.Windows;
using Tuan_13.Report;

namespace Tuan_13.View
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyReport rpt = new MyReport();
            myreport.ViewerCore.ReportSource = rpt;
        }
    }
}
