using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tuan_13.Helper;
using Tuan_13.Model;

namespace Tuan_13.ViewModels
{
    public class bai_2_4ViewModel : BaseViewModel
    {
        private readonly QLSINHVIENEntities db = new QLSINHVIENEntities();

        public ObservableCollection<MonHoc> ListMonHoc { get; set; }
        public ObservableCollection<string> ListNamHoc { get; set; }
        public ObservableCollection<int> ListHocKy { get; set; }
        public ObservableCollection<SinhVienNhapDiemItem> ListSinhVienNhapDiem { get; set; }

        public ICommand LoadListCommand { get; set; }
        public ICommand SavePointsCommand { get; set; }
        public ICommand PrintReportCommand { get; set; }

        public Action<MonHoc, string, int> PrintReportAction { get; set; }

        private MonHoc _selectedFilterMon;
        public MonHoc SelectedFilterMon
        {
            get { return _selectedFilterMon; }
            set
            {
                _selectedFilterMon = value;
                OnPropertyChanged();
            }
        }

        private string _selectedFilterNam;
        public string SelectedFilterNam
        {
            get { return _selectedFilterNam; }
            set
            {
                _selectedFilterNam = value;
                OnPropertyChanged();
            }
        }

        private int _selectedFilterHK;
        public int SelectedFilterHK
        {
            get { return _selectedFilterHK; }
            set
            {
                _selectedFilterHK = value;
                OnPropertyChanged();
            }
        }

        public bai_2_4ViewModel()
        {
            ListMonHoc = new ObservableCollection<MonHoc>(db.MonHoc.ToList());

            ListNamHoc = new ObservableCollection<string>(
                db.KetQua
                  .Select(kq => kq.NamHoc.Trim())
                  .Distinct()
                  .ToList()
            );

            ListHocKy = new ObservableCollection<int>(
                db.KetQua
                  .Select(kq => kq.HocKy)
                  .Distinct()
                  .ToList()
            );

            ListSinhVienNhapDiem = new ObservableCollection<SinhVienNhapDiemItem>();

            LoadListCommand = new RelayCommand((p) => LoadDanhSachSinhVien());
            SavePointsCommand = new RelayCommand((p) => LuuDiem());
            PrintReportCommand = new RelayCommand((p) => InBangDiem());
        }

        private void LoadDanhSachSinhVien()
        {
            if (!KiemTraBoLoc())
                return;

            ListSinhVienNhapDiem.Clear();

            var data = db.SinhVien
                .ToList()
                .Select(sv => new SinhVienNhapDiemItem
                {
                    MaSV = sv.MaSinhVien,
                    HoTen = sv.HoTen,
                    Lop = sv.MaLop,
                    MaMonHoc = SelectedFilterMon.MaMonHoc,

                    Diem = db.KetQua
                        .Where(kq =>
                            kq.MaSinhVien == sv.MaSinhVien &&
                            kq.MaMonHoc == SelectedFilterMon.MaMonHoc &&
                            kq.NamHoc.Trim() == SelectedFilterNam.Trim() &&
                            kq.HocKy == SelectedFilterHK)
                        .Select(kq => (double?)kq.Diem)
                        .FirstOrDefault()
                });

            foreach (var item in data)
            {
                ListSinhVienNhapDiem.Add(item);
            }
        }

        private void LuuDiem()
        {
            if (!KiemTraBoLoc())
                return;

            try
            {
                foreach (var sv in ListSinhVienNhapDiem)
                {
                    if (sv.Diem == null || sv.Diem < 0 || sv.Diem > 10)
                    {
                        MessageBox.Show("Điểm không hợp lệ: " + sv.MaSV);
                        return;
                    }

                    var existing = db.KetQua.FirstOrDefault(kq =>
                        kq.MaSinhVien == sv.MaSV &&
                        kq.MaMonHoc == SelectedFilterMon.MaMonHoc &&
                        kq.NamHoc.Trim() == SelectedFilterNam.Trim() &&
                        kq.HocKy == SelectedFilterHK
                    );

                    if (existing == null)
                    {
                        db.KetQua.Add(new KetQua
                        {
                            MaSinhVien = sv.MaSV,
                            MaMonHoc = SelectedFilterMon.MaMonHoc,
                            NamHoc = SelectedFilterNam.Trim(),
                            HocKy = SelectedFilterHK,
                            Diem = sv.Diem.Value
                        });
                    }
                    else
                    {
                        existing.Diem = sv.Diem.Value;
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Lưu điểm thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void InBangDiem()
        {
            if (!KiemTraBoLoc())
                return;

            if (PrintReportAction != null)
            {
                PrintReportAction(
                    SelectedFilterMon,
                    SelectedFilterNam.Trim(),
                    SelectedFilterHK
                );
            }
        }

        private bool KiemTraBoLoc()
        {
            if (SelectedFilterMon == null ||
                string.IsNullOrWhiteSpace(SelectedFilterNam) ||
                SelectedFilterHK == 0)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Môn học - Năm học - Học kỳ");
                return false;
            }

            return true;
        }
    }

    public class SinhVienNhapDiemItem : BaseViewModel
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string Lop { get; set; }
        public string MaMonHoc { get; set; }

        private double? _diem;
        public double? Diem
        {
            get { return _diem; }
            set
            {
                _diem = value;
                OnPropertyChanged();
            }
        }
    }
}