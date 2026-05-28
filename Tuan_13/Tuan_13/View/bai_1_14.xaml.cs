using System.Windows;
using Tuan_13.Report;
using Tuan_13.ViewModels;

namespace Tuan_13.View
{
    public partial class bai_1_14 : Window
    {
        private bai_1_14ViewModel vm;

        public bai_1_14()
        {
            InitializeComponent();

            vm = new bai_1_14ViewModel();
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(vm.SelectedMaLop))
            {
                MessageBox.Show("Vui lòng chọn lớp!");
                return;
            }

            DanhsachSVtheoLop rpt = new DanhsachSVtheoLop();

            rpt.SetDatabaseLogon("sa", "123", @"A108PC16\CSSQL08", "QLSINHVIEN");

            rpt.SetParameterValue("LocMaLop", vm.SelectedMaLop);

            myreport.ViewerCore.ReportSource = null;
            myreport.ViewerCore.ReportSource = rpt;
        }
    }
}