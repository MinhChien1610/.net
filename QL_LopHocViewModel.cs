using Tuan_6.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using Tuan_6.Model;

namespace Tuan_6.ViewModel
{
    public class QL_LopHocViewModel : BaseViewModel
    {
        public ObservableCollection<LopHoc> Classes { get; set; }
        public ObservableCollection<SinhVien> Students { get; set; }

        private List<SinhVien> _allStudents;

        private LopHoc _selectedClass;
        public LopHoc SelectedClass
        {
            get => _selectedClass;
            set
            {
                _selectedClass = value;
                OnPropertyChanged();
                RemoveClassCommand?.RaiseCanExecuteChanged();

                if (!_isLoadingSelectedStudent)
                {
                    FilterStudentsBySelectedClass();
                }
            }
        }

        private LopHoc _selectedClassInForm;
        public LopHoc SelectedClassInForm
        {
            get => _selectedClassInForm;
            set
            {
                _selectedClassInForm = value;
                OnPropertyChanged();
            }
        }

        private SinhVien _selectedStudent;
        public SinhVien SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();

                EditCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();

                if (_selectedStudent != null && !IsAdding)
                {
                    _isLoadingSelectedStudent = true;

                    StudentIdInput = _selectedStudent.StudentId;
                    FullNameInput = _selectedStudent.FullName;
                    AddressInput = _selectedStudent.Address;
                    Score1Input = _selectedStudent.Score1.ToString(CultureInfo.InvariantCulture);
                    Score2Input = _selectedStudent.Score2.ToString(CultureInfo.InvariantCulture);
                    Score3Input = _selectedStudent.Score3.ToString(CultureInfo.InvariantCulture);
                    SelectedClassInForm = Classes.FirstOrDefault(c => c.Name == _selectedStudent.ClassName);

                    _isLoadingSelectedStudent = false;
                }
            }
        }

        private string _newClassName;
        public string NewClassName
        {
            get => _newClassName;
            set
            {
                _newClassName = value;
                OnPropertyChanged();
                AddClassCommand?.RaiseCanExecuteChanged();
            }
        }

        private string _searchKeyword;
        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                OnPropertyChanged();
            }
        }

        private string _studentIdInput;
        public string StudentIdInput
        {
            get => _studentIdInput;
            set
            {
                _studentIdInput = value;
                OnPropertyChanged();
            }
        }

        private string _fullNameInput;
        public string FullNameInput
        {
            get => _fullNameInput;
            set
            {
                _fullNameInput = value;
                OnPropertyChanged();
            }
        }

        private string _addressInput;
        public string AddressInput
        {
            get => _addressInput;
            set
            {
                _addressInput = value;
                OnPropertyChanged();
            }
        }

        private string _score1Input;
        public string Score1Input
        {
            get => _score1Input;
            set
            {
                _score1Input = value;
                OnPropertyChanged();
            }
        }

        private string _score2Input;
        public string Score2Input
        {
            get => _score2Input;
            set
            {
                _score2Input = value;
                OnPropertyChanged();
            }
        }

        private string _score3Input;
        public string Score3Input
        {
            get => _score3Input;
            set
            {
                _score3Input = value;
                OnPropertyChanged();
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
                AddCommand?.RaiseCanExecuteChanged();
                EditCommand?.RaiseCanExecuteChanged();
                SaveCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();
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
                AddCommand?.RaiseCanExecuteChanged();
                EditCommand?.RaiseCanExecuteChanged();
                SaveCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();
            }
        }

        private bool _isLoadingSelectedStudent = false;

        public RelayCommand AddClassCommand { get; set; }
        public RelayCommand RemoveClassCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ShowAllCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand TinhDiemCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public QL_LopHocViewModel()
        {
            Classes = new ObservableCollection<LopHoc>
            {
                new LopHoc { Name = "DHKTPM18A" },
                new LopHoc { Name = "DHKTPM18B" },
                new LopHoc { Name = "DHKTPM18C" }
            };

            _allStudents = new List<SinhVien>
            {
                new SinhVien
                {
                    StudentId = "SV001",
                    FullName = "Nguyễn Văn An",
                    Address = "TP.HCM",
                    Score1 = 8,
                    Score2 = 7.5,
                    Score3 = 9,
                    ClassName = "DHKTPM18A"
                },
                new SinhVien
                {
                    StudentId = "SV002",
                    FullName = "Trần Thị Bình",
                    Address = "Đồng Nai",
                    Score1 = 7,
                    Score2 = 8,
                    Score3 = 8.5,
                    ClassName = "DHKTPM18A"
                },
                new SinhVien
                {
                    StudentId = "SV003",
                    FullName = "Lê Hoàng Nam",
                    Address = "Cần Thơ",
                    Score1 = 6.5,
                    Score2 = 7,
                    Score3 = 7.5,
                    ClassName = "DHKTPM18B"
                }
            };

            Students = new ObservableCollection<SinhVien>(_allStudents);

            AddClassCommand = new RelayCommand(_ => AddClass(), _ => !string.IsNullOrWhiteSpace(NewClassName));
            RemoveClassCommand = new RelayCommand(_ => RemoveClass(), _ => SelectedClass != null);

            SearchCommand = new RelayCommand(_ => SearchStudents());
            ShowAllCommand = new RelayCommand(_ => ShowAllStudents());

            AddCommand = new RelayCommand(_ => StartAdd(), _ => !IsEditing);
            TinhDiemCommand = new RelayCommand(_ => TinhDiemTB(), _ => CanTinhDiemTB());
            EditCommand = new RelayCommand(_ => StartEdit(), _ => SelectedStudent != null && !IsAdding);
            SaveCommand = new RelayCommand(_ => SaveStudent(), _ => IsAdding || IsEditing);
            DeleteCommand = new RelayCommand(_ => DeleteStudent(), _ => SelectedStudent != null && !IsAdding && !IsEditing);
        }

        private bool CanTinhDiemTB()
        {
            return !string.IsNullOrWhiteSpace(Score1Input) &&
                   !string.IsNullOrWhiteSpace(Score2Input) &&
                   !string.IsNullOrWhiteSpace(Score3Input);
        }

        private void AddClass()
        {
            string className = NewClassName?.Trim();

            if (string.IsNullOrWhiteSpace(className))
            {
                MessageBox.Show("Tên lớp không được để trống.");
                return;
            }

            if (Classes.Any(c => c.Name.Equals(className, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Lớp đã tồn tại.");
                return;
            }

            Classes.Add(new LopHoc { Name = className });
            NewClassName = string.Empty;
        }

        private void RemoveClass()
        {
            if (SelectedClass == null) return;

            bool hasStudent = _allStudents.Any(s => s.ClassName == SelectedClass.Name);
            if (hasStudent)
            {
                MessageBox.Show("Không thể xóa lớp vì lớp này đang có sinh viên.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa lớp này?", "Xác nhận", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            Classes.Remove(SelectedClass);
            SelectedClass = null;
        }

        private void SearchStudents()
        {
            IEnumerable<SinhVien> query = _allStudents;

            if (SelectedClass != null)
            {
                query = query.Where(s => s.ClassName == SelectedClass.Name);
            }

            if (!string.IsNullOrWhiteSpace(SearchKeyword))
            {
                string keyword = SearchKeyword.Trim().ToLower();
                query = query.Where(s =>
                    (!string.IsNullOrWhiteSpace(s.StudentId) && s.StudentId.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrWhiteSpace(s.FullName) && s.FullName.ToLower().Contains(keyword)));
            }

            Students = new ObservableCollection<SinhVien>(query);
            OnPropertyChanged(nameof(Students));
        }

        private void ShowAllStudents()
        {
            SearchKeyword = string.Empty;

            if (SelectedClass != null)
            {
                Students = new ObservableCollection<SinhVien>(
                    _allStudents.Where(s => s.ClassName == SelectedClass.Name));
            }
            else
            {
                Students = new ObservableCollection<SinhVien>(_allStudents);
            }

            OnPropertyChanged(nameof(Students));
        }

        private void FilterStudentsBySelectedClass()
        {
            if (SelectedClass == null)
            {
                Students = new ObservableCollection<SinhVien>(_allStudents);
            }
            else
            {
                Students = new ObservableCollection<SinhVien>(
                    _allStudents.Where(s => s.ClassName == SelectedClass.Name));
            }

            OnPropertyChanged(nameof(Students));
        }

        private void StartAdd()
        {
            if (IsAdding)
            {
                ResetForm();
                IsAdding = false;
                return;
            }

            ResetForm();
            SelectedStudent = null;
            IsAdding = true;
            IsEditing = false;
        }

        private void StartEdit()
        {
            if (SelectedStudent == null) return;

            if (IsEditing)
            {
                ResetForm();
                IsEditing = false;
                return;
            }

            IsEditing = true;
            IsAdding = false;
        }

        private void SaveStudent()
        {
            if (!ValidateInput(out double score1, out double score2, out double score3))
                return;

            if (IsAdding)
            {
                var newStudent = new SinhVien
                {
                    StudentId = StudentIdInput.Trim(),
                    FullName = FullNameInput.Trim(),
                    Address = AddressInput?.Trim(),
                    Score1 = score1,
                    Score2 = score2,
                    Score3 = score3,
                    ClassName = SelectedClassInForm?.Name
                };

                _allStudents.Add(newStudent);
            }
            else if (IsEditing && SelectedStudent != null)
            {
                SelectedStudent.StudentId = StudentIdInput.Trim();
                SelectedStudent.FullName = FullNameInput.Trim();
                SelectedStudent.Address = AddressInput?.Trim();
                SelectedStudent.Score1 = score1;
                SelectedStudent.Score2 = score2;
                SelectedStudent.Score3 = score3;
                SelectedStudent.ClassName = SelectedClassInForm?.Name;
            }

            IsAdding = false;
            IsEditing = false;
            RefreshStudentListAfterSave();
            ResetForm();
        }

        private void DeleteStudent()
        {
            if (SelectedStudent == null) return;

            if (MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            _allStudents.Remove(SelectedStudent);
            Students.Remove(SelectedStudent);
            SelectedStudent = null;
            ResetForm();
        }

        private void TinhDiemTB()
        {
            if (!double.TryParse(Score1Input, NumberStyles.Any, CultureInfo.InvariantCulture, out double score1) ||
                !double.TryParse(Score2Input, NumberStyles.Any, CultureInfo.InvariantCulture, out double score2) ||
                !double.TryParse(Score3Input, NumberStyles.Any, CultureInfo.InvariantCulture, out double score3))
            {
                MessageBox.Show("Vui lòng nhập điểm hợp lệ để tính điểm trung bình.");
                return;
            }

            if (score1 < 0 || score1 > 10 || score2 < 0 || score2 > 10 || score3 < 0 || score3 > 10)
            {
                MessageBox.Show("Điểm phải nằm trong khoảng từ 0 đến 10.");
                return;
            }

            double diemTB = Math.Round((score1 + score2 + score3) / 3.0, 2);

            MessageBox.Show($"Điểm trung bình của sinh viên là: {diemTB}",
                            "Kết quả",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }
        private bool ValidateInput(out double score1, out double score2, out double score3)
        {
            score1 = 0;
            score2 = 0;
            score3 = 0;

            if (string.IsNullOrWhiteSpace(StudentIdInput))
            {
                MessageBox.Show("Mã sinh viên không được để trống.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(FullNameInput))
            {
                MessageBox.Show("Họ tên không được để trống.");
                return false;
            }

            if (SelectedClassInForm == null)
            {
                MessageBox.Show("Bạn phải chọn lớp.");
                return false;
            }

            if (!double.TryParse(Score1Input, NumberStyles.Any, CultureInfo.InvariantCulture, out score1) ||
                !double.TryParse(Score2Input, NumberStyles.Any, CultureInfo.InvariantCulture, out score2) ||
                !double.TryParse(Score3Input, NumberStyles.Any, CultureInfo.InvariantCulture, out score3))
            {
                MessageBox.Show("Điểm phải là số hợp lệ.");
                return false;
            }

            if (score1 < 0 || score1 > 10 || score2 < 0 || score2 > 10 || score3 < 0 || score3 > 10)
            {
                MessageBox.Show("Điểm phải nằm trong khoảng 0 đến 10.");
                return false;
            }

            bool existed = _allStudents.Any(s =>
                s.StudentId.Equals(StudentIdInput.Trim(), StringComparison.OrdinalIgnoreCase) &&
                s != SelectedStudent);

            if (existed)
            {
                MessageBox.Show("Mã sinh viên đã tồn tại.");
                return false;
            }

            return true;
        }

        private void RefreshStudentListAfterSave()
        {
            if (SelectedClass != null)
            {
                Students = new ObservableCollection<SinhVien>(
                    _allStudents.Where(s => s.ClassName == SelectedClass.Name));
            }
            else
            {
                Students = new ObservableCollection<SinhVien>(_allStudents);
            }

            OnPropertyChanged(nameof(Students));
        }

        private void ResetForm()
        {
            StudentIdInput = string.Empty;
            FullNameInput = string.Empty;
            AddressInput = string.Empty;
            Score1Input = string.Empty;
            Score2Input = string.Empty;
            Score3Input = string.Empty;
            SelectedClassInForm = null;
        }
    }
}