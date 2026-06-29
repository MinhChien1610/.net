using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tuan_6.Model
{
    public class AccountModel : INotifyPropertyChanged
    {
        private int _stt;
        private string _accountNumber;
        private string _customerName;
        private string _address;
        private string _city;
        private decimal _balance;

        public int STT
        {
            get => _stt;
            set { _stt = value; OnPropertyChanged(); }
        }

        public string AccountNumber
        {
            get => _accountNumber;
            set { _accountNumber = value; OnPropertyChanged(); }
        }

        public string CustomerName
        {
            get => _customerName;
            set { _customerName = value; OnPropertyChanged(); }
        }

        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(); }
        }

        public string City
        {
            get => _city;
            set { _city = value; OnPropertyChanged(); }
        }

        public decimal Balance
        {
            get => _balance;
            set { _balance = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
