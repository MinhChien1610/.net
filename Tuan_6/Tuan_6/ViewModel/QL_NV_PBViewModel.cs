using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace Tuan_6.ViewModels
{
    public class QL_NV_PBViewModel : BaseViewModel
    {
        private ObservableCollection<PhongBanViewModel> _departments;
        private PhongBanViewModel _selectedDepartment;
        private NhanVienViewModel _selectedEmployee;

        private string _newDepartmentName;
        private string _employeeIdInput;
        private string _fullNameInput;
        private string _addressInput;

        private bool _isInputEnabled;
        private bool _isEmployeeIdEnabled;

        private bool _isAdding;
        private bool _isEditing;

        private NhanVienViewModel _editingEmployee;
        private PhongBanViewModel _editingDepartment;

        public ObservableCollection<PhongBanViewModel> Departments
        {
            get => _departments;
            set
            {
                _departments = value;
                OnPropertyChanged();
            }
        }

        public PhongBanViewModel SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EmployeeCountInSelectedDepartment));

                AddCommand?.RaiseCanExecuteChanged();
                EditCommand?.RaiseCanExecuteChanged();
                SaveCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();
                RemoveDepartmentCommand?.RaiseCanExecuteChanged();
            }
        }

        public NhanVienViewModel SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();

                if (_selectedEmployee != null)
                {
                    EmployeeIdInput = _selectedEmployee.MaNhanVien;
                    FullNameInput = _selectedEmployee.HoTen;
                    AddressInput = _selectedEmployee.DiaChi;
                }

                EditCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();
            }
        }

        public string NewDepartmentName
        {
            get => _newDepartmentName;
            set
            {
                _newDepartmentName = value;
                OnPropertyChanged();
                AddDepartmentCommand?.RaiseCanExecuteChanged();
            }
        }

        public string EmployeeIdInput
        {
            get => _employeeIdInput;
            set
            {
                _employeeIdInput = value;
                OnPropertyChanged();
                SaveCommand?.RaiseCanExecuteChanged();
            }
        }

        public string FullNameInput
        {
            get => _fullNameInput;
            set
            {
                _fullNameInput = value;
                OnPropertyChanged();
                SaveCommand?.RaiseCanExecuteChanged();
            }
        }

        public string AddressInput
        {
            get => _addressInput;
            set
            {
                _addressInput = value;
                OnPropertyChanged();
                SaveCommand?.RaiseCanExecuteChanged();
            }
        }

        public bool IsInputEnabled
        {
            get => _isInputEnabled;
            set
            {
                _isInputEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsEmployeeIdEnabled
        {
            get => _isEmployeeIdEnabled;
            set
            {
                _isEmployeeIdEnabled = value;
                OnPropertyChanged();
            }
        }

        public int TotalEmployeeCount => Departments?.Sum(pb => pb.DanhSachNhanVien.Count) ?? 0;

        public int EmployeeCountInSelectedDepartment => SelectedDepartment?.DanhSachNhanVien.Count ?? 0;

        public RelayCommand AddDepartmentCommand { get; set; }
        public RelayCommand RemoveDepartmentCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public QL_NV_PBViewModel()
        {
            Departments = new ObservableCollection<PhongBanViewModel>();
            Departments.CollectionChanged += Departments_CollectionChanged;

            AddDepartmentCommand = new RelayCommand(_ => AddDepartment(), _ => CanAddDepartment());
            RemoveDepartmentCommand = new RelayCommand(_ => RemoveDepartment(), _ => CanRemoveDepartment());

            AddCommand = new RelayCommand(_ => AddEmployee(), _ => CanAddEmployee());
            EditCommand = new RelayCommand(_ => EditEmployee(), _ => CanEditEmployee());
            SaveCommand = new RelayCommand(_ => SaveEmployee(), _ => CanSaveEmployee());
            DeleteCommand = new RelayCommand(_ => DeleteEmployee(), _ => CanDeleteEmployee());

            LoadSampleData();
            ResetInput();
        }

        private void LoadSampleData()
        {
            var pb1 = new PhongBanViewModel { TenPhongBan = "Phòng Kế toán" };
            pb1.DanhSachNhanVien.Add(new NhanVienViewModel { MaNhanVien = "NV001", HoTen = "Nguyễn Văn An", DiaChi = "TP.HCM" });
            pb1.DanhSachNhanVien.Add(new NhanVienViewModel { MaNhanVien = "NV002", HoTen = "Trần Thị Bình", DiaChi = "Bình Dương" });

            var pb2 = new PhongBanViewModel { TenPhongBan = "Phòng Nhân sự" };
            pb2.DanhSachNhanVien.Add(new NhanVienViewModel { MaNhanVien = "NV003", HoTen = "Lê Hoàng Nam", DiaChi = "Đồng Nai" });

            var pb3 = new PhongBanViewModel { TenPhongBan = "Phòng Kỹ thuật" };
            pb3.DanhSachNhanVien.Add(new NhanVienViewModel { MaNhanVien = "NV004", HoTen = "Phạm Minh Khoa", DiaChi = "Long An" });
            pb3.DanhSachNhanVien.Add(new NhanVienViewModel { MaNhanVien = "NV005", HoTen = "Võ Ngọc Lan", DiaChi = "TP.HCM" });

            HookDepartment(pb1);
            HookDepartment(pb2);
            HookDepartment(pb3);

            Departments.Add(pb1);
            Departments.Add(pb2);
            Departments.Add(pb3);

            SelectedDepartment = Departments.FirstOrDefault();
            RefreshCounts();
        }

        private void Departments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (PhongBanViewModel pb in e.NewItems)
                    HookDepartment(pb);
            }

            if (e.OldItems != null)
            {
                foreach (PhongBanViewModel pb in e.OldItems)
                    UnhookDepartment(pb);
            }

            RefreshCounts();
        }

        private void HookDepartment(PhongBanViewModel pb)
        {
            if (pb != null)
                pb.DanhSachNhanVien.CollectionChanged += Employees_CollectionChanged;
        }

        private void UnhookDepartment(PhongBanViewModel pb)
        {
            if (pb != null)
                pb.DanhSachNhanVien.CollectionChanged -= Employees_CollectionChanged;
        }

        private void Employees_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshCounts();
        }

        private void RefreshCounts()
        {
            OnPropertyChanged(nameof(TotalEmployeeCount));
            OnPropertyChanged(nameof(EmployeeCountInSelectedDepartment));
        }

        private void AddDepartment()
        {
            string tenPB = (NewDepartmentName ?? "").Trim();

            if (string.IsNullOrWhiteSpace(tenPB))
            {
                MessageBox.Show("Vui lòng nhập tên phòng ban.");
                return;
            }

            if (Departments.Any(pb => pb.TenPhongBan.Equals(tenPB, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Phòng ban đã tồn tại.");
                return;
            }

            var phongBanMoi = new PhongBanViewModel
            {
                TenPhongBan = tenPB
            };

            HookDepartment(phongBanMoi);
            Departments.Add(phongBanMoi);
            SelectedDepartment = phongBanMoi;
            NewDepartmentName = "";

            MessageBox.Show("Thêm phòng ban thành công.");
        }

        private bool CanAddDepartment()
        {
            return !string.IsNullOrWhiteSpace(NewDepartmentName);
        }

        private void RemoveDepartment()
        {
            if (SelectedDepartment == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban cần xóa.");
                return;
            }

            if (SelectedDepartment.DanhSachNhanVien.Count > 0)
            {
                MessageBox.Show("Không thể xóa phòng ban đang có nhân viên.");
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa phòng ban \"{SelectedDepartment.TenPhongBan}\" không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Departments.Remove(SelectedDepartment);
                SelectedDepartment = Departments.FirstOrDefault();
                MessageBox.Show("Xóa phòng ban thành công.");
            }
        }

        private bool CanRemoveDepartment()
        {
            return SelectedDepartment != null;
        }

        private void AddEmployee()
        {
            if (SelectedDepartment == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban trước khi thêm nhân viên.");
                return;
            }

            _isAdding = true;
            _isEditing = false;
            _editingEmployee = null;
            _editingDepartment = null;

            EmployeeIdInput = "";
            FullNameInput = "";
            AddressInput = "";

            IsInputEnabled = true;
            IsEmployeeIdEnabled = true;
        }

        private bool CanAddEmployee()
        {
            return Departments != null && Departments.Count > 0;
        }

        private void EditEmployee()
        {
            if (SelectedEmployee == null || SelectedDepartment == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa.");
                return;
            }

            _isAdding = false;
            _isEditing = true;
            _editingEmployee = SelectedEmployee;
            _editingDepartment = SelectedDepartment;

            EmployeeIdInput = _editingEmployee.MaNhanVien;
            FullNameInput = _editingEmployee.HoTen;
            AddressInput = _editingEmployee.DiaChi;

            IsInputEnabled = true;
            IsEmployeeIdEnabled = false;
        }

        private bool CanEditEmployee()
        {
            return SelectedEmployee != null && SelectedDepartment != null;
        }

        private void SaveEmployee()
        {
            if (SelectedDepartment == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban.");
                return;
            }

            string maNV = (EmployeeIdInput ?? "").Trim();
            string hoTen = (FullNameInput ?? "").Trim();
            string diaChi = (AddressInput ?? "").Trim();

            if (string.IsNullOrWhiteSpace(maNV) ||
                string.IsNullOrWhiteSpace(hoTen) ||
                string.IsNullOrWhiteSpace(diaChi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên.");
                return;
            }

            if (_isAdding)
            {
                bool maDaTonTai = Departments
                    .SelectMany(pb => pb.DanhSachNhanVien)
                    .Any(nv => nv.MaNhanVien.Equals(maNV, StringComparison.OrdinalIgnoreCase));

                if (maDaTonTai)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại.");
                    return;
                }

                var nhanVienMoi = new NhanVienViewModel
                {
                    MaNhanVien = maNV,
                    HoTen = hoTen,
                    DiaChi = diaChi
                };

                SelectedDepartment.DanhSachNhanVien.Add(nhanVienMoi);
                SelectedEmployee = nhanVienMoi;

                MessageBox.Show("Thêm nhân viên thành công.");
            }
            else if (_isEditing && _editingEmployee != null)
            {
                _editingEmployee.HoTen = hoTen;
                _editingEmployee.DiaChi = diaChi;

                if (_editingDepartment != SelectedDepartment)
                {
                    _editingDepartment.DanhSachNhanVien.Remove(_editingEmployee);
                    SelectedDepartment.DanhSachNhanVien.Add(_editingEmployee);
                }

                SelectedEmployee = _editingEmployee;

                MessageBox.Show("Cập nhật nhân viên thành công.");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Thêm hoặc Sửa trước khi lưu.");
                return;
            }

            _isAdding = false;
            _isEditing = false;
            IsInputEnabled = false;
            IsEmployeeIdEnabled = false;

            RefreshCounts();
        }

        private bool CanSaveEmployee()
        {
            if (!IsInputEnabled) return false;

            return !string.IsNullOrWhiteSpace(EmployeeIdInput)
                   && !string.IsNullOrWhiteSpace(FullNameInput)
                   && !string.IsNullOrWhiteSpace(AddressInput)
                   && SelectedDepartment != null;
        }

        private void DeleteEmployee()
        {
            if (SelectedEmployee == null || SelectedDepartment == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.");
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa nhân viên \"{SelectedEmployee.HoTen}\" không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                SelectedDepartment.DanhSachNhanVien.Remove(SelectedEmployee);
                SelectedEmployee = null;

                ResetInput();
                RefreshCounts();

                MessageBox.Show("Xóa nhân viên thành công.");
            }
        }

        private bool CanDeleteEmployee()
        {
            return SelectedEmployee != null && SelectedDepartment != null;
        }

        private void ResetInput()
        {
            EmployeeIdInput = "";
            FullNameInput = "";
            AddressInput = "";

            IsInputEnabled = false;
            IsEmployeeIdEnabled = false;

            _isAdding = false;
            _isEditing = false;
            _editingEmployee = null;
            _editingDepartment = null;
        }
    }
}