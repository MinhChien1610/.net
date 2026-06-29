using Tuan_6.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tuan_6.Model;

namespace Tuan_6.ViewModel
{
    public class AccountViewModel : BaseViewModel
    {
        public ObservableCollection<AccountModel> Accounts { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        private AccountModel _selectedAccount;
        public AccountModel SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                OnPropertyChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isAdding;
        public bool IsAdding
        {
            get => _isAdding;
            set
            {
                _isAdding = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AddButtonText));
                OnPropertyChanged(nameof(IsInputEnabled));
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EditButtonText));
                OnPropertyChanged(nameof(IsInputEnabled));
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsInputEnabled => IsAdding || IsEditing;
        public string AddButtonText => IsAdding ? "Hủy" : "+ Thêm";
        public string EditButtonText => IsEditing ? "Hủy Sửa" : "Sửa";
        public decimal TotalBalance => Accounts?.Sum(a => a.Balance) ?? 0;

        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public AccountViewModel()
        {
            Accounts = new ObservableCollection<AccountModel>
            {
                new AccountModel { STT=1, AccountNumber="AC001", CustomerName="Nguyễn Văn Tâm", Address="140 Lê Trọng Tấn", City="TP. Hồ Chí Minh", Balance=15000000 },
                new AccountModel { STT=2, AccountNumber="AC002", CustomerName="Trần Văn Bình", Address="140 Lê Trọng Tấn", City="Cần Thơ", Balance=1200000 },
                new AccountModel { STT=3, AccountNumber="AC003", CustomerName="Thanh Thức", Address="234/1 Nguyễn Ảnh Thủ", City="TP. Hồ Chí Minh", Balance=5000000 }
            };

            Cities = new ObservableCollection<string> { "TP. Hồ Chí Minh", "Hà Nội", "Đà Nẵng", "Cần Thơ", "Huế" };

            Accounts.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalBalance));

            foreach (var acc in Accounts)
                acc.PropertyChanged += Account_PropertyChanged;

            AddCommand = new RelayCommand(_ => AddOrCancel(), _ => !IsEditing);
            EditCommand = new RelayCommand(_ => EditOrCancel(), _ => SelectedAccount != null && !IsAdding);
            SaveCommand = new RelayCommand(_ => Save(), _ => IsAdding || IsEditing);
            DeleteCommand = new RelayCommand(_ => Delete(), _ => SelectedAccount != null && !IsAdding && !IsEditing);
        }

        private void Account_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AccountModel.Balance))
                OnPropertyChanged(nameof(TotalBalance));
        }

        private void AddOrCancel()
        {
            if (IsAdding) { ResetState(); return; }
            SelectedAccount = new AccountModel();
            IsAdding = true;
        }

        private void EditOrCancel()
        {
            if (IsEditing) { ResetState(); return; }
            IsEditing = true;
        }

        private void Save()
        {
            if (!Validate()) return;
            if (IsAdding)
            {
                SelectedAccount.STT = Accounts.Count + 1;
                Accounts.Add(SelectedAccount);
                SelectedAccount.PropertyChanged += Account_PropertyChanged;
            }
            ResetState();
            OnPropertyChanged(nameof(TotalBalance));
        }

        private void Delete()
        {
            if (SelectedAccount == null) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;

            SelectedAccount.PropertyChanged -= Account_PropertyChanged;
            Accounts.Remove(SelectedAccount);
            for (int i = 0; i < Accounts.Count; i++) Accounts[i].STT = i + 1;
            ResetState();
        }

        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(SelectedAccount.AccountNumber)) { MessageBox.Show("Số tài khoản trống"); return false; }
            if (string.IsNullOrWhiteSpace(SelectedAccount.CustomerName)) { MessageBox.Show("Tên khách hàng trống"); return false; }
            if (SelectedAccount.Balance < 0) { MessageBox.Show("Số tiền không hợp lệ"); return false; }
            if (Accounts.Any(a => a.AccountNumber == SelectedAccount.AccountNumber && a != SelectedAccount)) { MessageBox.Show("Số tài khoản đã tồn tại"); return false; }
            return true;
        }

        private void ResetState()
        {
            IsAdding = false;
            IsEditing = false;
            SelectedAccount = null;
        }
    }
}
