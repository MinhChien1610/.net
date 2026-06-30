using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tuan_7.Helper;
using Tuan_7.Models;

namespace Tuan_7.ViewModels
{
    public class TaiKhoanViewModel : BaseViewModel
    {
        public ObservableCollection<TaiKhoanModel> DanhSach { get; set; }
        public ObservableCollection<string> DanhSachLoaiTK { get; set; }
        public ObservableCollection<string> DanhSachTrangThai { get; set; }

        private TaiKhoanModel _selectedItem;
        public TaiKhoanModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();

                if (_selectedItem != null)
                {
                    SoTK = _selectedItem.SoTK;
                    ChuTK = _selectedItem.ChuTK;
                    LoaiTK = _selectedItem.LoaiTK;
                    SoDu = _selectedItem.SoDu.ToString();
                    TrangThai = _selectedItem.TrangThai;
                    GhiChu = _selectedItem.GhiChu;
                }
            }
        }

        private string _soTK;
        public string SoTK
        {
            get => _soTK;
            set { _soTK = value; OnPropertyChanged(); }
        }

        private string _chuTK;
        public string ChuTK
        {
            get => _chuTK;
            set { _chuTK = value; OnPropertyChanged(); }
        }

        private string _loaiTK;
        public string LoaiTK
        {
            get => _loaiTK;
            set { _loaiTK = value; OnPropertyChanged(); }
        }

        private string _soDu;
        public string SoDu
        {
            get => _soDu;
            set { _soDu = value; OnPropertyChanged(); }
        }

        private string _trangThai;
        public string TrangThai
        {
            get => _trangThai;
            set { _trangThai = value; OnPropertyChanged(); }
        }

        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set { _ghiChu = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private bool _dangThem;
        private bool _dangSua;

        public TaiKhoanViewModel()
        {
            DanhSach = new ObservableCollection<TaiKhoanModel>();
            DanhSachLoaiTK = new ObservableCollection<string>
            {
                "Thanh toán",
                "Tiết kiệm",
                "Doanh nghiệp"
            };
            DanhSachTrangThai = new ObservableCollection<string>
            {
                "Hoạt động",
                "Tạm khóa",
                "Đóng"
            };

            KhoiTaoDuLieuMau();
            KhoiTaoCommand();
            ResetForm();
        }

        private void KhoiTaoCommand()
        {
            AddCommand = new RelayCommand(ThemMoi);
            EditCommand = new RelayCommand(Sua);
            SaveCommand = new RelayCommand(Luu);
            DeleteCommand = new RelayCommand(Xoa);
        }

        private void ThemMoi(object obj)
        {
            ResetForm();
            _dangThem = true;
            _dangSua = false;
        }

        private void Sua(object obj)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa.");
                return;
            }

            _dangThem = false;
            _dangSua = true;
        }

        private void Luu(object obj)
        {
            if (!KiemTraHopLe())
                return;

            decimal soDuValue = decimal.Parse(SoDu);

            if (_dangThem)
            {
                if (DanhSach.Any(x => x.SoTK == SoTK))
                {
                    MessageBox.Show("Số tài khoản đã tồn tại.");
                    return;
                }

                DanhSach.Add(new TaiKhoanModel
                {
                    STT = DanhSach.Count + 1,
                    SoTK = SoTK,
                    ChuTK = ChuTK,
                    LoaiTK = LoaiTK,
                    SoDu = soDuValue,
                    TrangThai = TrangThai,
                    GhiChu = GhiChu
                });

                MessageBox.Show("Thêm tài khoản thành công.");
            }
            else if (_dangSua)
            {
                if (SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần sửa.");
                    return;
                }

                if (DanhSach.Any(x => x.SoTK == SoTK && x != SelectedItem))
                {
                    MessageBox.Show("Số tài khoản đã tồn tại.");
                    return;
                }

                SelectedItem.SoTK = SoTK;
                SelectedItem.ChuTK = ChuTK;
                SelectedItem.LoaiTK = LoaiTK;
                SelectedItem.SoDu = soDuValue;
                SelectedItem.TrangThai = TrangThai;
                SelectedItem.GhiChu = GhiChu;

                MessageBox.Show("Cập nhật tài khoản thành công.");
            }
            else
            {
                MessageBox.Show("Hãy bấm Thêm hoặc Sửa trước khi Lưu.");
                return;
            }

            CapNhatSTT();
            ResetForm();
        }

        private void Xoa(object obj)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DanhSach.Remove(SelectedItem);
                CapNhatSTT();
                ResetForm();
                MessageBox.Show("Xóa thành công.");
            }
        }

        private bool KiemTraHopLe()
        {
            if (string.IsNullOrWhiteSpace(SoTK) ||
                string.IsNullOrWhiteSpace(ChuTK) ||
                string.IsNullOrWhiteSpace(LoaiTK) ||
                string.IsNullOrWhiteSpace(TrangThai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return false;
            }

            if (!decimal.TryParse(SoDu, out decimal soDuValue) || soDuValue < 0)
            {
                MessageBox.Show("Số dư không hợp lệ.");
                return false;
            }

            return true;
        }

        private void KhoiTaoDuLieuMau()
        {
            DanhSach.Add(new TaiKhoanModel
            {
                STT = 1,
                SoTK = "100001",
                ChuTK = "Nguyễn Văn A",
                LoaiTK = "Thanh toán",
                SoDu = 15000000,
                TrangThai = "Hoạt động",
                GhiChu = "VIP"
            });

            DanhSach.Add(new TaiKhoanModel
            {
                STT = 2,
                SoTK = "100002",
                ChuTK = "Trần Thị B",
                LoaiTK = "Tiết kiệm",
                SoDu = 24000000,
                TrangThai = "Hoạt động",
                GhiChu = ""
            });

            DanhSach.Add(new TaiKhoanModel
            {
                STT = 3,
                SoTK = "100003",
                ChuTK = "Lê Văn C",
                LoaiTK = "Doanh nghiệp",
                SoDu = 12000000,
                TrangThai = "Tạm khóa",
                GhiChu = "Chờ xác minh"
            });
        }

        private void CapNhatSTT()
        {
            for (int i = 0; i < DanhSach.Count; i++)
            {
                DanhSach[i].STT = i + 1;
            }
        }

        private void ResetForm()
        {
            SelectedItem = null;
            SoTK = "";
            ChuTK = "";
            LoaiTK = DanhSachLoaiTK.FirstOrDefault();
            SoDu = "0";
            TrangThai = DanhSachTrangThai.FirstOrDefault();
            GhiChu = "";
            _dangThem = false;
            _dangSua = false;
        }
    }
}