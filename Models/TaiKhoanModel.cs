using Tuan_7.Helper;

namespace Tuan_7.Models
{
    public class TaiKhoanModel : BaseViewModel
    {
        private int _stt;
        public int STT
        {
            get => _stt;
            set { _stt = value; OnPropertyChanged(); }
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

        private decimal _soDu;
        public decimal SoDu
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

        public override string ToString()
        {
            return $"{SoTK} - {ChuTK}";
        }
    }
}