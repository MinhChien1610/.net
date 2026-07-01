using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Tuan_10.Helper;
using Tuan_10.Models;

namespace Tuan_10.ViewModels
{
    public class Bai3ViewModel : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();

        public ObservableCollection<Lop> DanhSachLop { get; set; }
        public ObservableCollection<Khoa> DanhSachKhoa { get; set; }

        private Lop _LopDangChon;
        public Lop LopDangChon
        {
            get => _LopDangChon;
            set
            {
                _LopDangChon = value;
                OnPropertyChanged(nameof(LopDangChon));
            }
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private bool isAdding = false;
        private bool isEditing = false;
        private string oldMaLop = "";

        public Bai3ViewModel()
        {
            LoadData();
            CommandSetup();
        }

        void LoadData()
        {
            try
            {
                DanhSachLop = new ObservableCollection<Lop>(db.Lop.ToList());
                DanhSachKhoa = new ObservableCollection<Khoa>(db.Khoa.ToList());

                OnPropertyChanged(nameof(DanhSachLop));
                OnPropertyChanged(nameof(DanhSachKhoa));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu!\n" + ex.Message);
            }
        }

        void CommandSetup()
        {
            AddCommand = new RelayCommand(_ =>
            {
                LopDangChon = new Lop();
                isAdding = true;
                isEditing = false;
                oldMaLop = "";
            });

            EditCommand = new RelayCommand(_ =>
            {
                if (LopDangChon == null)
                {
                    MessageBox.Show("Vui lòng chọn lớp cần sửa!");
                    return;
                }

                isAdding = false;
                isEditing = true;
                oldMaLop = LopDangChon.MaLop;
            });

            DeleteCommand = new RelayCommand(_ =>
            {
                if (LopDangChon == null)
                {
                    MessageBox.Show("Vui lòng chọn lớp cần xóa!");
                    return;
                }

                var result = MessageBox.Show("Bạn có chắc muốn xóa lớp này không?",
                    "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result != MessageBoxResult.Yes) return;

                try
                {
                    var lop = db.Lop.FirstOrDefault(x => x.MaLop == LopDangChon.MaLop);
                    if (lop != null)
                    {
                        db.Lop.Remove(lop);
                        db.SaveChanges();
                    }

                    LoadData();
                    LopDangChon = null;
                    MessageBox.Show("Xóa thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa lớp!\n" + ex.Message);
                }
            });

            SaveCommand = new RelayCommand(_ =>
            {
                if (LopDangChon == null)
                {
                    MessageBox.Show("Chưa có dữ liệu để lưu!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(LopDangChon.MaLop) ||
                    string.IsNullOrWhiteSpace(LopDangChon.MaKhoa))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                try
                {
                    if (isAdding)
                    {
                        var check = db.Lop.FirstOrDefault(x => x.MaLop == LopDangChon.MaLop);
                        if (check != null)
                        {
                            MessageBox.Show("Mã lớp đã tồn tại!");
                            return;
                        }

                        Lop lopMoi = new Lop
                        {
                            MaLop = LopDangChon.MaLop,
                            MaKhoa = LopDangChon.MaKhoa
                        };

                        db.Lop.Add(lopMoi);
                    }
                    else if (isEditing)
                    {
                        var lop = db.Lop.FirstOrDefault(x => x.MaLop == oldMaLop);
                        if (lop == null)
                        {
                            MessageBox.Show("Không tìm thấy lớp cần sửa!");
                            return;
                        }

                        if (oldMaLop != LopDangChon.MaLop)
                        {
                            var check = db.Lop.FirstOrDefault(x => x.MaLop == LopDangChon.MaLop);
                            if (check != null)
                            {
                                MessageBox.Show("Mã lớp mới đã tồn tại!");
                                return;
                            }
                        }

                        lop.MaLop = LopDangChon.MaLop;
                        lop.MaKhoa = LopDangChon.MaKhoa;
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn Thêm hoặc Sửa trước khi lưu!");
                        return;
                    }

                    db.SaveChanges();
                    LoadData();

                    isAdding = false;
                    isEditing = false;
                    oldMaLop = "";

                    MessageBox.Show("Lưu thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu dữ liệu!\n" + ex.Message);
                }
            });

            CancelCommand = new RelayCommand(_ =>
            {
                LopDangChon = null;
                isAdding = false;
                isEditing = false;
                oldMaLop = "";
            });
        }
    }
}