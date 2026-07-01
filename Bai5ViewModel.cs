using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Tuan_10.Helper;
using Tuan_10.Models;

namespace Tuan_10.ViewModels
{
    public class Bai5ViewModel : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();

        public ObservableCollection<MonHoc> ListMonHoc { get; set; }

        private MonHoc _SelectedMonHoc;
        public MonHoc SelectedMonHoc
        {
            get => _SelectedMonHoc;
            set
            {
                _SelectedMonHoc = value;
                OnPropertyChanged(nameof(SelectedMonHoc));
            }
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private bool isAdding = false;
        private bool isEditing = false;
        private string oldMaMon = "";

        public Bai5ViewModel()
        {
            LoadMonHocData();
            CommandSetup();
        }

        void LoadMonHocData()
        {
            try
            {
                ListMonHoc = new ObservableCollection<MonHoc>(db.MonHoc.ToList());
                OnPropertyChanged(nameof(ListMonHoc));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách môn học!\n" + ex.Message);
            }
        }

        void CommandSetup()
        {
            AddCommand = new RelayCommand(_ =>
            {
                SelectedMonHoc = new MonHoc();
                isAdding = true;
                isEditing = false;
                oldMaMon = "";
            });

            EditCommand = new RelayCommand(_ =>
            {
                if (SelectedMonHoc == null)
                {
                    MessageBox.Show("Vui lòng chọn môn học cần sửa!");
                    return;
                }

                isAdding = false;
                isEditing = true;
                oldMaMon = SelectedMonHoc.MaMonHoc;
            });

            DeleteCommand = new RelayCommand(_ =>
            {
                if (SelectedMonHoc == null)
                {
                    MessageBox.Show("Vui lòng chọn môn học cần xóa!");
                    return;
                }

                var result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa môn {SelectedMonHoc.TenMonHoc} không?",
                    "Xác nhận",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result != MessageBoxResult.Yes) return;

                try
                {
                    var mon = db.MonHoc.FirstOrDefault(x => x.MaMonHoc == SelectedMonHoc.MaMonHoc);
                    if (mon != null)
                    {
                        db.MonHoc.Remove(mon);
                        db.SaveChanges();
                    }

                    LoadMonHocData();
                    SelectedMonHoc = null;
                    MessageBox.Show("Xóa thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa môn học!\n" + ex.Message);
                }
            });

            SaveCommand = new RelayCommand(_ =>
            {
                if (SelectedMonHoc == null)
                {
                    MessageBox.Show("Chưa có dữ liệu để lưu!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(SelectedMonHoc.MaMonHoc) ||
                    string.IsNullOrWhiteSpace(SelectedMonHoc.TenMonHoc) ||
                    string.IsNullOrWhiteSpace(SelectedMonHoc.TinhChat))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                try
                {
                    if (isAdding)
                    {
                        var check = db.MonHoc.FirstOrDefault(x => x.MaMonHoc == SelectedMonHoc.MaMonHoc);
                        if (check != null)
                        {
                            MessageBox.Show("Mã môn học đã tồn tại!");
                            return;
                        }

                        MonHoc monMoi = new MonHoc
                        {
                            MaMonHoc = SelectedMonHoc.MaMonHoc,
                            TenMonHoc = SelectedMonHoc.TenMonHoc,
                            SoTC = SelectedMonHoc.SoTC,
                            TinhChat = SelectedMonHoc.TinhChat
                        };

                        db.MonHoc.Add(monMoi);
                    }
                    else if (isEditing)
                    {
                        var mon = db.MonHoc.FirstOrDefault(x => x.MaMonHoc == oldMaMon);
                        if (mon == null)
                        {
                            MessageBox.Show("Không tìm thấy môn học cần sửa!");
                            return;
                        }

                        if (oldMaMon != SelectedMonHoc.MaMonHoc)
                        {
                            var check = db.MonHoc.FirstOrDefault(x => x.MaMonHoc == SelectedMonHoc.MaMonHoc);
                            if (check != null)
                            {
                                MessageBox.Show("Mã môn học mới đã tồn tại!");
                                return;
                            }
                        }

                        mon.MaMonHoc = SelectedMonHoc.MaMonHoc;
                        mon.TenMonHoc = SelectedMonHoc.TenMonHoc;
                        mon.SoTC = SelectedMonHoc.SoTC;
                        mon.TinhChat = SelectedMonHoc.TinhChat;
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn Thêm hoặc Sửa trước khi lưu!");
                        return;
                    }

                    db.SaveChanges();
                    LoadMonHocData();

                    isAdding = false;
                    isEditing = false;
                    oldMaMon = "";

                    MessageBox.Show("Lưu thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu dữ liệu!\n" + ex.Message);
                }
            });

            CancelCommand = new RelayCommand(_ =>
            {
                SelectedMonHoc = null;
                isAdding = false;
                isEditing = false;
                oldMaMon = "";
            });
        }
    }
}