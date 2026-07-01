using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Tuan_10.Helper;
using Tuan_10.Models;

namespace Tuan_10.ViewModels
{
    public class Bai6ViewModel : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();

        public ObservableCollection<MonHoc> ListMonHoc { get; set; }
        public ObservableCollection<string> ListNamHoc { get; set; }
        public ObservableCollection<int> ListHocKy { get; set; }
        public ObservableCollection<SinhVienNhapDiemView> ListSinhVienNhapDiem { get; set; }

        private MonHoc _SelectedFilterMon;
        public MonHoc SelectedFilterMon
        {
            get => _SelectedFilterMon;
            set
            {
                _SelectedFilterMon = value;
                OnPropertyChanged(nameof(SelectedFilterMon));
            }
        }

        private string _SelectedFilterNam;
        public string SelectedFilterNam
        {
            get => _SelectedFilterNam;
            set
            {
                _SelectedFilterNam = value;
                OnPropertyChanged(nameof(SelectedFilterNam));
            }
        }

        private int? _SelectedFilterHK;
        public int? SelectedFilterHK
        {
            get => _SelectedFilterHK;
            set
            {
                _SelectedFilterHK = value;
                OnPropertyChanged(nameof(SelectedFilterHK));
            }
        }

        public RelayCommand LoadListCommand { get; set; }
        public RelayCommand SavePointsCommand { get; set; }

        public Bai6ViewModel()
        {
            LoadComboBoxData();
            LoadEmptyGrid();
            CommandSetup();
        }

        void LoadComboBoxData()
        {
            try
            {
                ListMonHoc = new ObservableCollection<MonHoc>(db.MonHoc.ToList());
                OnPropertyChanged(nameof(ListMonHoc));

                ListNamHoc = new ObservableCollection<string>
                {
                    "2022-2023",
                    "2023-2024",
                    "2024-2025",
                    "2025-2026"
                };
                OnPropertyChanged(nameof(ListNamHoc));

                ListHocKy = new ObservableCollection<int> { 1, 2 };
                OnPropertyChanged(nameof(ListHocKy));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu!\n" + ex.Message);
            }
        }

        void LoadEmptyGrid()
        {
            ListSinhVienNhapDiem = new ObservableCollection<SinhVienNhapDiemView>();
            OnPropertyChanged(nameof(ListSinhVienNhapDiem));
        }

        void CommandSetup()
        {
            LoadListCommand = new RelayCommand(_ =>
            {
                if (SelectedFilterMon == null || string.IsNullOrWhiteSpace(SelectedFilterNam) || SelectedFilterHK == null)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ Môn học, Năm học và Học kỳ!");
                    return;
                }

                try
                {
                    var ds = db.SinhVien.ToList();

                    ListSinhVienNhapDiem = new ObservableCollection<SinhVienNhapDiemView>();

                    foreach (var item in ds)
                    {
                        var ketQuaCu = db.KetQua.FirstOrDefault(x =>
                            x.MaSinhVien == item.MaSinhVien &&
                            x.MaMonHoc == SelectedFilterMon.MaMonHoc &&
                            x.NamHoc == SelectedFilterNam &&
                            x.HocKy == SelectedFilterHK.Value);

                        ListSinhVienNhapDiem.Add(new SinhVienNhapDiemView
                        {
                            MaSinhVien = item.MaSinhVien,
                            HoTen = item.HoTen,
                            MaLop = item.MaLop,
                            Diem = ketQuaCu != null ? ketQuaCu.Diem : null
                        });
                    }

                    OnPropertyChanged(nameof(ListSinhVienNhapDiem));

                    if (ListSinhVienNhapDiem.Count == 0)
                    {
                        MessageBox.Show("Không có sinh viên nào!");
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    if (ex.InnerException != null)
                        error += "\n" + ex.InnerException.Message;
                    if (ex.InnerException?.InnerException != null)
                        error += "\n" + ex.InnerException.InnerException.Message;

                    MessageBox.Show("Lỗi tải danh sách sinh viên!\n" + error);
                }
            });

            SavePointsCommand = new RelayCommand(_ =>
            {
                if (SelectedFilterMon == null || string.IsNullOrWhiteSpace(SelectedFilterNam) || SelectedFilterHK == null)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ Môn học, Năm học và Học kỳ!");
                    return;
                }

                if (ListSinhVienNhapDiem == null || ListSinhVienNhapDiem.Count == 0)
                {
                    MessageBox.Show("Chưa có danh sách sinh viên để lưu điểm!");
                    return;
                }

                foreach (var sv in ListSinhVienNhapDiem)
                {
                    if (sv.Diem == null)
                    {
                        MessageBox.Show($"Sinh viên {sv.HoTen} chưa nhập điểm!");
                        return;
                    }

                    if (sv.Diem < 0 || sv.Diem > 10)
                    {
                        MessageBox.Show($"Điểm của sinh viên {sv.HoTen} không hợp lệ! Điểm phải từ 0 đến 10.");
                        return;
                    }
                }

                try
                {
                    foreach (var sv in ListSinhVienNhapDiem)
                    {
                        var kq = db.KetQua.FirstOrDefault(x =>
                            x.MaSinhVien == sv.MaSinhVien &&
                            x.MaMonHoc == SelectedFilterMon.MaMonHoc &&
                            x.NamHoc == SelectedFilterNam &&
                            x.HocKy == SelectedFilterHK.Value);

                        if (kq == null)
                        {
                            KetQua newKQ = new KetQua
                            {
                                MaSinhVien = sv.MaSinhVien,
                                MaMonHoc = SelectedFilterMon.MaMonHoc,
                                NamHoc = SelectedFilterNam,
                                HocKy = SelectedFilterHK.Value,
                                Diem = sv.Diem
                            };

                            db.KetQua.Add(newKQ);
                        }
                        else
                        {
                            kq.Diem = sv.Diem;
                        }
                    }

                    db.SaveChanges();
                    MessageBox.Show("Lưu điểm thành công!");
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    if (ex.InnerException != null)
                        error += "\n" + ex.InnerException.Message;
                    if (ex.InnerException?.InnerException != null)
                        error += "\n" + ex.InnerException.InnerException.Message;

                    MessageBox.Show("Lỗi lưu điểm!\n" + error);
                }
            });
        }
    }
}