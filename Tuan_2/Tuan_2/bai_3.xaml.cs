using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tuan_2
{
    public partial class bai_3 : Window
    {
        private readonly List<Button> ds_ghechon = new List<Button>();
        private const int PRICE_A = 1000;
        private const int PRICE_B = 1500;
        private const int PRICE_C = 2000;

        public bai_3()
        {
            InitializeComponent();
            txt_Money.Text = "0";
        }

        private void Seat_Click(object sender, RoutedEventArgs e)
        {
            Button seat = sender as Button;
            if (seat == null) return;

            string state = seat.Tag?.ToString() ?? "Empty";

            if (state == "Sold")
            {
                MessageBox.Show("Vị trí này đã được bán, vui lòng chọn ghế khác!",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (state == "Empty")
            {
                seat.Tag = "Selecting";
                seat.Background = Brushes.Blue;
                seat.Foreground = Brushes.White;

                if (!ds_ghechon.Contains(seat))
                    ds_ghechon.Add(seat);
            }
            else 
            {
                seat.Tag = "Empty";
                seat.Background = Brushes.LightGray;
                seat.Foreground = Brushes.Black;

                ds_ghechon.Remove(seat);
            }
        }

        private void btnClick_Chon(object sender, RoutedEventArgs e)
        {
            if (ds_ghechon.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn ghế nào!",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int tong = 0;

            for (int i = 0; i < ds_ghechon.Count; i++)
            {
                Button seat = ds_ghechon[i];

                tong += GetPriceByRow(seat);

                seat.Tag = "Sold";
                seat.Background = Brushes.Yellow;
                seat.Foreground = Brushes.Black;
            }

            txt_Money.Text = tong.ToString();
            ds_ghechon.Clear();
        }

        private void btnClick_Huy(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ds_ghechon.Count; i++)
            {
                Button seat = ds_ghechon[i];

                seat.Tag = "Empty";
                seat.Background = Brushes.LightGray;
                seat.Foreground = Brushes.Black;
            }

            ds_ghechon.Clear();
            txt_Money.Text = "0";
        }

        private void btnClick_End(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Bạn có muốn thoát không?",
                "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (r == MessageBoxResult.Yes)
                Close();
        }

        private int GetPriceByRow(Button seat)
        {
            int row = Grid.GetRow(seat);

            if (row == 0) return PRICE_A; 
            if (row == 1) return PRICE_B; 
            return PRICE_C;               
        }
    }
}
