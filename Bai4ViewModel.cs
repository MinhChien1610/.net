using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Tuan_10.Helper;
using Tuan_10.Models;

namespace Tuan_10.ViewModels
{
    public class Bai4ViewModel : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();

        public ObservableCollection<SinhVien> ListSinhVien { get; set; }
        public ObservableCollection<Lop> ListLop { get; set; }

        private SinhVien _SelectedSinhVien;
        public SinhVien SelectedSinhVien
        {
            get => _SelectedSinhVien;
            set
            {
                _SelectedSinhVien = value;
                OnPropertyChanged(nameof(SelectedSinhVien));
            }
        }

        public RelayCommand LoadDataCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private bool isAdding = false;
        private bool isEditing = false;
        private string oldMaSinhVien = "";

        public Bai4ViewModel()
        {
            LoadComboBoxData();
            LoadSinhVienData();
            CommandSetup();
        }

        void LoadComboBoxData()
        {
            try
            {
                ListLop = new ObservableCollection<Lop>(db.Lop.ToList());
                OnPropertyChanged(nameof(ListLop));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp!\n" + ex.Message);
            }
        }

        void LoadSinhVienData()
        {
            try
            {
                ListSinhVien = new ObservableCollection<SinhVien>(db.SinhVien.ToList());
                OnPropertyChanged(nameof(ListSinhVien));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách sinh viên!\n" + ex.Message);
            }
        }

        void CommandSetup()
        {
            LoadDataCommand = new RelayCommand(_ =>
            {
                LoadComboBoxData();
                LoadSinhVienData();
            });

            AddCommand = new RelayCommand(_ =>
            {
                SelectedSinhVien = new SinhVien();
                isAdding = true;
                isEditing = false;
                oldMaSinhVien = "";
            });

            EditCommand = new RelayCommand(_ =>
            {
                if (SelectedSinhVien == null)
                {
                    MessageBox.Show("Vui lòng chọn sinh viên cần sửa!");
                    return;
                }

                isAdding = false;
                isEditing = true;
                oldMaSinhVien = SelectedSinhVien.MaSinhVien;
            });

            DeleteCommand = new RelayCommand(_ =>
            {
                if (SelectedSinhVien == null)
                {
                    MessageBox.Show("Vui lòng chọn sinh viên cần xóa!");
                    return;
                }

                var result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa sinh viên {SelectedSinhVien.HoTen} không?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result != MessageBoxResult.Yes) return;

                try
                {
                    var sv = db.SinhVien.FirstOrDefault(x => x.MaSinhVien == SelectedSinhVien.MaSinhVien);
                    if (sv != null)
                    {
                        db.SinhVien.Remove(sv);
                        db.SaveChanges();
                    }

                    LoadSinhVienData();
                    SelectedSinhVien = null;
                    MessageBox.Show("Xóa sinh viên thành công!");
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    if (ex.InnerException != null)
                        error += "\n" + ex.InnerException.Message;
                    if (ex.InnerException?.InnerException != null)
                        error += "\n" + ex.InnerException.InnerException.Message;

                    MessageBox.Show("Lỗi xóa sinh viên!\n" + error);
                }
            });

            SaveCommand = new RelayCommand(_ =>
            {
                if (SelectedSinhVien == null)
                {
                    MessageBox.Show("Chưa có dữ liệu để lưu!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(SelectedSinhVien.MaSinhVien) ||
                    string.IsNullOrWhiteSpace(SelectedSinhVien.HoTen) ||
                    string.IsNullOrWhiteSpace(SelectedSinhVien.GioiTinh) ||
                    string.IsNullOrWhiteSpace(SelectedSinhVien.MaLop))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                try
                {
                    if (isAdding)
                    {
                        var check = db.SinhVien.FirstOrDefault(x => x.MaSinhVien == SelectedSinhVien.MaSinhVien);
                        if (check != null)
                        {
                            MessageBox.Show("Mã sinh viên đã tồn tại!");
                            return;
                        }

                        SinhVien svMoi = new SinhVien
                        {
                            MaSinhVien = SelectedSinhVien.MaSinhVien,
                            HoTen = SelectedSinhVien.HoTen,
                            GioiTinh = SelectedSinhVien.GioiTinh,
                            NgaySinh = SelectedSinhVien.NgaySinh,
                            MaLop = SelectedSinhVien.MaLop
                        };

                        db.SinhVien.Add(svMoi);
                    }
                    else if (isEditing)
                    {
                        var sv = db.SinhVien.FirstOrDefault(x => x.MaSinhVien == oldMaSinhVien);
                        if (sv == null)
                        {
                            MessageBox.Show("Không tìm thấy sinh viên cần sửa!");
                            return;
                        }

                        if (oldMaSinhVien != SelectedSinhVien.MaSinhVien)
                        {
                            var check = db.SinhVien.FirstOrDefault(x => x.MaSinhVien == SelectedSinhVien.MaSinhVien);
                            if (check != null)
                            {
                                MessageBox.Show("Mã sinh viên mới đã tồn tại!");
                                return;
                            }
                        }

                        sv.MaSinhVien = SelectedSinhVien.MaSinhVien;
                        sv.HoTen = SelectedSinhVien.HoTen;
                        sv.GioiTinh = SelectedSinhVien.GioiTinh;
                        sv.NgaySinh = SelectedSinhVien.NgaySinh;
                        sv.MaLop = SelectedSinhVien.MaLop;
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn Thêm hoặc Sửa trước khi lưu!");
                        return;
                    }

                    db.SaveChanges();
                    LoadSinhVienData();

                    isAdding = false;
                    isEditing = false;
                    oldMaSinhVien = "";

                    MessageBox.Show("Lưu dữ liệu thành công!");
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    if (ex.InnerException != null)
                        error += "\n" + ex.InnerException.Message;
                    if (ex.InnerException?.InnerException != null)
                        error += "\n" + ex.InnerException.InnerException.Message;

                    MessageBox.Show("Lỗi lưu dữ liệu!\n" + error);
                }
            });

            CancelCommand = new RelayCommand(_ =>
            {
                SelectedSinhVien = null;
                isAdding = false;
                isEditing = false;
                oldMaSinhVien = "";
            });
        }
    }
}