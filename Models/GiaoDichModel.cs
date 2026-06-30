using System;
using Tuan_7.Helper;

namespace Tuan_7.Models
{
    public class GiaoDichModel : BaseViewModel
    {
        private string _maGD;
        public string MaGD
        {
            get => _maGD;
            set { _maGD = value; OnPropertyChanged(); }
        }

        private string _ngay;
        public string Ngay
        {
            get => _ngay;
            set { _ngay = value; OnPropertyChanged(); }
        }

        private string _loai;
        public string Loai
        {
            get => _loai;
            set { _loai = value; OnPropertyChanged(); }
        }

        private string _tkNguon;
        public string TKNguon
        {
            get => _tkNguon;
            set { _tkNguon = value; OnPropertyChanged(); }
        }

        private string _tkDich;
        public string TKDich
        {
            get => _tkDich;
            set { _tkDich = value; OnPropertyChanged(); }
        }

        private decimal _soTien;
        public decimal SoTien
        {
            get => _soTien;
            set { _soTien = value; OnPropertyChanged(); }
        }

        private string _noiDung;
        public string NoiDung
        {
            get => _noiDung;
            set { _noiDung = value; OnPropertyChanged(); }
        }

        private decimal _phiGiaoDich;
        public decimal PhiGiaoDich
        {
            get => _phiGiaoDich;
            set { _phiGiaoDich = value; OnPropertyChanged(); }
        }

        public DateTime NgayTaoRaw { get; set; } = DateTime.Now;
    }
}