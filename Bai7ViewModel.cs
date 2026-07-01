using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Tuan_10.Helper;
using Tuan_10.Models;

namespace Tuan_10.ViewModels
{
    public class Bai7ViewModel : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();

        // =========================
        // NHẬP MÃ SINH VIÊN
        // =========================
        private string _maSVInput;
        public string MaSVInput
        {
            get => _maSVInput;
            set
            {
                _maSVInput = value;
                OnPropertyChanged(nameof(MaSVInput));
            }
        }

        private string _studentName;
        public string StudentName
        {
            get => _studentName;
            set
            {
                _studentName = value;
                OnPropertyChanged(nameof(StudentName));
            }
        }

        private string _className;
        public string ClassName
        {
            get => _className;
            set
            {
                _className = value;
                OnPropertyChanged(nameof(ClassName));
            }
        }

        // =========================
        // DANH SÁCH NĂM HỌC - HỌC KỲ
        // =========================
        public ObservableCollection<string> ListNamHoc { get; set; }
        public ObservableCollection<int> ListHocKy { get; set; }

        private string _selectedNamHoc;
        public string SelectedNamHoc
        {
            get => _selectedNamHoc;
            set
            {
                _selectedNamHoc = value;
                OnPropertyChanged(nameof(SelectedNamHoc));
            }
        }

        private int? _selectedHocKy;
        public int? SelectedHocKy
        {
            get => _selectedHocKy;
            set
            {
                _selectedHocKy = value;
                OnPropertyChanged(nameof(SelectedHocKy));
            }
        }

        // =========================
        // DANH SÁCH BẢNG ĐIỂM
        // =========================
        private ObservableCollection<BangDiemView> _listDiem;
        public ObservableCollection<BangDiemView> ListDiem
        {
            get => _listDiem;
            set
            {
                _listDiem = value;
                OnPropertyChanged(nameof(ListDiem));
            }
        }

        // =========================
        // THỐNG KÊ
        // =========================
        private int _tongTinChi;
        public int TongTinChi
        {
            get => _tongTinChi;
            set
            {
                _tongTinChi = value;
                OnPropertyChanged(nameof(TongTinChi));
            }
        }

        private double _gpa;
        public double GPA
        {
            get => _gpa;
            set
            {
                _gpa = value;
                OnPropertyChanged(nameof(GPA));
            }
        }

        private string _xepLoai;
        public string XepLoai
        {
            get => _xepLoai;
            set
            {
                _xepLoai = value;
                OnPropertyChanged(nameof(XepLoai));
            }
        }

        // =========================
        // COMMAND
        // =========================
        public ICommand SearchCommand { get; set; }
        public ICommand ViewGradeCommand { get; set; }

        public Bai7ViewModel()
        {
            ListNamHoc = new ObservableCollection<string>
            {
                "2022-2023",
                "2023-2024",
                "2024-2025",
                "2025-2026"
            };

            ListHocKy = new ObservableCollection<int> { 1, 2 };

            ListDiem = new ObservableCollection<BangDiemView>();

            StudentName = "";
            ClassName = "";
            TongTinChi = 0;
            GPA = 0;
            XepLoai = "";

            SearchCommand = new RelayCommand(_ => TimSinhVien());
            ViewGradeCommand = new RelayCommand(_ => XemBangDiem());
        }

        // =========================
        // TÌM SINH VIÊN
        // =========================
        private void TimSinhVien()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(MaSVInput))
                {
                    MessageBox.Show("Vui lòng nhập mã sinh viên!");
                    return;
                }

                string maSV = MaSVInput.Trim();

                var sinhVien = db.SinhVien.FirstOrDefault(x => x.MaSinhVien.Trim() == maSV);

                if (sinhVien == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên!");
                    StudentName = "";
                    ClassName = "";
                    ListDiem.Clear();
                    TongTinChi = 0;
                    GPA = 0;
                    XepLoai = "";
                    return;
                }

                StudentName = sinhVien.HoTen;
                ClassName = sinhVien.MaLop;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                if (ex.InnerException != null)
                    error += "\n" + ex.InnerException.Message;
                if (ex.InnerException?.InnerException != null)
                    error += "\n" + ex.InnerException.InnerException.Message;

                MessageBox.Show("Lỗi tìm sinh viên!\n" + error);
            }
        }

        // =========================
        // XEM BẢNG ĐIỂM
        // =========================
        private void XemBangDiem()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(MaSVInput))
                {
                    MessageBox.Show("Vui lòng nhập mã sinh viên!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(StudentName))
                {
                    MessageBox.Show("Vui lòng tìm sinh viên trước!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(SelectedNamHoc) || SelectedHocKy == null)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ năm học và học kỳ!");
                    return;
                }

                string maSV = MaSVInput.Trim();
                string namHoc = SelectedNamHoc.Trim();
                int hocKy = SelectedHocKy.Value;

                var ds = (from kq in db.KetQua
                          join mh in db.MonHoc
                          on kq.MaMonHoc.Trim() equals mh.MaMonHoc.Trim()
                          where kq.MaSinhVien.Trim() == maSV
                                && kq.NamHoc.Trim() == namHoc
                                && kq.HocKy == hocKy
                          select new
                          {
                              mh.MaMonHoc,
                              mh.TenMonHoc,
                              mh.SoTC,
                              kq.Diem
                          }).ToList();

                ListDiem.Clear();

                if (ds.Count == 0)
                {
                    TongTinChi = 0;
                    GPA = 0;
                    XepLoai = "";
                    MessageBox.Show("Không có bảng điểm trong học kỳ đã chọn!");
                    return;
                }

                int stt = 1;
                foreach (var item in ds)
                {
                    ListDiem.Add(new BangDiemView
                    {
                        STT = stt++,
                        MaMH = item.MaMonHoc,
                        TenMon = item.TenMonHoc,
                        SoTC = item.SoTC ?? 0,
                        DiemSo = item.Diem ?? 0,
                        DiemChu = QuyDoiDiemChu(item.Diem ?? 0)
                    });
                }

                TongTinChi = ListDiem.Sum(x => x.SoTC);

                if (TongTinChi > 0)
                {
                    GPA = Math.Round(ListDiem.Sum(x => x.DiemSo * x.SoTC) / TongTinChi, 2);
                }
                else
                {
                    GPA = 0;
                }

                XepLoai = XepLoaiHocLuc(GPA);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                if (ex.InnerException != null)
                    error += "\n" + ex.InnerException.Message;
                if (ex.InnerException?.InnerException != null)
                    error += "\n" + ex.InnerException.InnerException.Message;

                MessageBox.Show("Lỗi xem bảng điểm!\n" + error);
            }
        }

        // =========================
        // QUY ĐỔI ĐIỂM CHỮ
        // =========================
        private string QuyDoiDiemChu(double diem)
        {
            if (diem >= 8.5) return "A";
            if (diem >= 8.0) return "B+";
            if (diem >= 7.0) return "B";
            if (diem >= 6.5) return "C+";
            if (diem >= 5.5) return "C";
            if (diem >= 5.0) return "D+";
            if (diem >= 4.0) return "D";
            return "F";
        }

        // =========================
        // XẾP LOẠI
        // =========================
        private string XepLoaiHocLuc(double gpa)
        {
            if (gpa >= 8.5) return "Xuất sắc";
            if (gpa >= 8.0) return "Giỏi";
            if (gpa >= 6.5) return "Khá";
            if (gpa >= 5.0) return "Trung bình";
            return "Yếu";
        }
    }
}