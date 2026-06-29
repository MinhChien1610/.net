using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Tuan_6.ViewModels
{
    public class TaiKhoanNganHangViewModel : BaseViewModel
    {
        private int _stt;
        private string _soTK;
        private string _tenKhachHang;
        private string _diaChi;
        private string _thanhPho;
        private decimal _soTien;

        public int STT
        {
            get => _stt;
            set
            {
                _stt = value;
                OnPropertyChanged();
            }
        }

        public string SoTK
        {
            get => _soTK;
            set
            {
                _soTK = value;
                OnPropertyChanged();
            }
        }

        public string TenKhachHang
        {
            get => _tenKhachHang;
            set
            {
                _tenKhachHang = value;
                OnPropertyChanged();
            }
        }

        public string DiaChi
        {
            get => _diaChi;
            set
            {
                _diaChi = value;
                OnPropertyChanged();
            }
        }

        public string ThanhPho
        {
            get => _thanhPho;
            set
            {
                _thanhPho = value;
                OnPropertyChanged();
            }
        }

        public decimal SoTien
        {
            get => _soTien;
            set
            {
                _soTien = value;
                OnPropertyChanged();
            }
        }
    }

    public class QuanLiTKNHViewModel : BaseViewModel
    {
        private ObservableCollection<TaiKhoanNganHangViewModel> _danhSach;
        private TaiKhoanNganHangViewModel _selectedItem;

        private string _soTaiKhoan;
        private string _tenKhachHang;
        private string _diaChi;
        private string _thanhPho;
        private string _soTien;

        private bool _isAdding;
        private bool _isEditing;
        private TaiKhoanNganHangViewModel _editingItem;

        public ObservableCollection<TaiKhoanNganHangViewModel> DanhSach
        {
            get => _danhSach;
            set
            {
                _danhSach = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TongTien));
            }
        }

        public TaiKhoanNganHangViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();

                if (_selectedItem != null)
                {
                    SoTaiKhoan = _selectedItem.SoTK;
                    TenKhachHang = _selectedItem.TenKhachHang;
                    DiaChi = _selectedItem.DiaChi;
                    ThanhPho = _selectedItem.ThanhPho;
                    SoTien = _selectedItem.SoTien.ToString("0.##", CultureInfo.InvariantCulture);
                }

                SuaCommand?.RaiseCanExecuteChanged();
                XoaCommand?.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<string> ListThanhPho { get; set; }

        public string SoTaiKhoan
        {
            get => _soTaiKhoan;
            set
            {
                _soTaiKhoan = value;
                OnPropertyChanged();
                LuuCommand?.RaiseCanExecuteChanged();
            }
        }

        public string TenKhachHang
        {
            get => _tenKhachHang;
            set
            {
                _tenKhachHang = value;
                OnPropertyChanged();
                LuuCommand?.RaiseCanExecuteChanged();
            }
        }

        public string DiaChi
        {
            get => _diaChi;
            set
            {
                _diaChi = value;
                OnPropertyChanged();
                LuuCommand?.RaiseCanExecuteChanged();
            }
        }

        public string ThanhPho
        {
            get => _thanhPho;
            set
            {
                _thanhPho = value;
                OnPropertyChanged();
                LuuCommand?.RaiseCanExecuteChanged();
            }
        }

        public string SoTien
        {
            get => _soTien;
            set
            {
                _soTien = value;
                OnPropertyChanged();
                LuuCommand?.RaiseCanExecuteChanged();
            }
        }

        public decimal TongTien => DanhSach?.Sum(x => x.SoTien) ?? 0;

        public RelayCommand ThemCommand { get; set; }
        public RelayCommand LuuCommand { get; set; }
        public RelayCommand SuaCommand { get; set; }
        public RelayCommand XoaCommand { get; set; }

        public QuanLiTKNHViewModel()
        {
            DanhSach = new ObservableCollection<TaiKhoanNganHangViewModel>();
            DanhSach.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TongTien));

            ListThanhPho = new ObservableCollection<string>
            {
                "Hồ Chí Minh",
                "Hà Nội",
                "Đà Nẵng",
                "Cần Thơ",
                "Huế",
                "Nha Trang",
                "Biên Hòa",
                "Thủ Đức"
            };

            ThemCommand = new RelayCommand(_ => Them());
            LuuCommand = new RelayCommand(_ => Luu(), _ => CanLuu());
            SuaCommand = new RelayCommand(_ => Sua(), _ => CanSua());
            XoaCommand = new RelayCommand(_ => Xoa(), _ => CanXoa());

            LoadDuLieuMau();
            ResetForm();
        }

        private void LoadDuLieuMau()
        {
            DanhSach.Add(new TaiKhoanNganHangViewModel
            {
                STT = 1,
                SoTK = "100001",
                TenKhachHang = "Nguyễn Văn An",
                DiaChi = "Quận 12",
                ThanhPho = "Hồ Chí Minh",
                SoTien = 15000000
            });

            DanhSach.Add(new TaiKhoanNganHangViewModel
            {
                STT = 2,
                SoTK = "100002",
                TenKhachHang = "Trần Thị Bình",
                DiaChi = "Thủ Đức",
                ThanhPho = "Hồ Chí Minh",
                SoTien = 22000000
            });

            DanhSach.Add(new TaiKhoanNganHangViewModel
            {
                STT = 3,
                SoTK = "100003",
                TenKhachHang = "Lê Minh Khoa",
                DiaChi = "Hải Châu",
                ThanhPho = "Đà Nẵng",
                SoTien = 9800000
            });

            OnPropertyChanged(nameof(TongTien));
        }

        private void Them()
        {
            _isAdding = true;
            _isEditing = false;
            _editingItem = null;
            SelectedItem = null;
            ResetForm();
        }

        private void Sua()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa.");
                return;
            }

            _isAdding = false;
            _isEditing = true;
            _editingItem = SelectedItem;

            SoTaiKhoan = _editingItem.SoTK;
            TenKhachHang = _editingItem.TenKhachHang;
            DiaChi = _editingItem.DiaChi;
            ThanhPho = _editingItem.ThanhPho;
            SoTien = _editingItem.SoTien.ToString("0.##", CultureInfo.InvariantCulture);
        }

        private void Luu()
        {
            string soTK = (SoTaiKhoan ?? "").Trim();
            string tenKH = (TenKhachHang ?? "").Trim();
            string diaChi = (DiaChi ?? "").Trim();
            string thanhPho = (ThanhPho ?? "").Trim();
            string soTienText = (SoTien ?? "").Trim();

            if (string.IsNullOrWhiteSpace(soTK) ||
                string.IsNullOrWhiteSpace(tenKH) ||
                string.IsNullOrWhiteSpace(diaChi) ||
                string.IsNullOrWhiteSpace(thanhPho) ||
                string.IsNullOrWhiteSpace(soTienText))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (!decimal.TryParse(soTienText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal soTienValue))
            {
                if (!decimal.TryParse(soTienText, NumberStyles.Any, new CultureInfo("vi-VN"), out soTienValue))
                {
                    MessageBox.Show("Số tiền không hợp lệ.");
                    return;
                }
            }

            if (soTienValue < 0)
            {
                MessageBox.Show("Số tiền phải lớn hơn hoặc bằng 0.");
                return;
            }

            if (_isEditing && _editingItem != null)
            {
                bool trungSoTKKhiSua = DanhSach.Any(x =>
                    x != _editingItem &&
                    x.SoTK.Equals(soTK, StringComparison.OrdinalIgnoreCase));

                if (trungSoTKKhiSua)
                {
                    MessageBox.Show("Số tài khoản đã tồn tại.");
                    return;
                }

                _editingItem.SoTK = soTK;
                _editingItem.TenKhachHang = tenKH;
                _editingItem.DiaChi = diaChi;
                _editingItem.ThanhPho = thanhPho;
                _editingItem.SoTien = soTienValue;

                MessageBox.Show("Cập nhật tài khoản thành công.");
            }
            else
            {
                bool trungSoTK = DanhSach.Any(x =>
                    x.SoTK.Equals(soTK, StringComparison.OrdinalIgnoreCase));

                if (trungSoTK)
                {
                    MessageBox.Show("Số tài khoản đã tồn tại.");
                    return;
                }

                var item = new TaiKhoanNganHangViewModel
                {
                    STT = DanhSach.Count + 1,
                    SoTK = soTK,
                    TenKhachHang = tenKH,
                    DiaChi = diaChi,
                    ThanhPho = thanhPho,
                    SoTien = soTienValue
                };

                DanhSach.Add(item);
                SelectedItem = item;

                MessageBox.Show("Thêm tài khoản thành công.");
            }

            _isAdding = false;
            _isEditing = false;
            _editingItem = null;

            CapNhatSTT();
            OnPropertyChanged(nameof(TongTien));
            ResetForm();
        }

        private void Xoa()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa.");
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc muốn xóa tài khoản {SelectedItem.SoTK} không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DanhSach.Remove(SelectedItem);
                SelectedItem = null;

                CapNhatSTT();
                OnPropertyChanged(nameof(TongTien));
                ResetForm();

                MessageBox.Show("Xóa tài khoản thành công.");
            }
        }

        private bool CanLuu()
        {
            return !string.IsNullOrWhiteSpace(SoTaiKhoan)
                   && !string.IsNullOrWhiteSpace(TenKhachHang)
                   && !string.IsNullOrWhiteSpace(DiaChi)
                   && !string.IsNullOrWhiteSpace(ThanhPho)
                   && !string.IsNullOrWhiteSpace(SoTien);
        }

        private bool CanSua()
        {
            return SelectedItem != null;
        }

        private bool CanXoa()
        {
            return SelectedItem != null;
        }

        private void ResetForm()
        {
            SoTaiKhoan = string.Empty;
            TenKhachHang = string.Empty;
            DiaChi = string.Empty;
            ThanhPho = null;
            SoTien = string.Empty;
        }

        private void CapNhatSTT()
        {
            for (int i = 0; i < DanhSach.Count; i++)
            {
                DanhSach[i].STT = i + 1;
            }
        }
    }
}