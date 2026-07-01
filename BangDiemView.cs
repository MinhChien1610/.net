using Tuan_10.Helper;

namespace Tuan_10.Models
{
    public class BangDiemView : BaseViewModel
    {
        private int _stt;
        public int STT
        {
            get => _stt;
            set
            {
                _stt = value;
                OnPropertyChanged(nameof(STT));
            }
        }

        private string _maMH;
        public string MaMH
        {
            get => _maMH;
            set
            {
                _maMH = value;
                OnPropertyChanged(nameof(MaMH));
            }
        }

        private string _tenMon;
        public string TenMon
        {
            get => _tenMon;
            set
            {
                _tenMon = value;
                OnPropertyChanged(nameof(TenMon));
            }
        }

        private int _soTC;
        public int SoTC
        {
            get => _soTC;
            set
            {
                _soTC = value;
                OnPropertyChanged(nameof(SoTC));
            }
        }

        private double _diemSo;
        public double DiemSo
        {
            get => _diemSo;
            set
            {
                _diemSo = value;
                OnPropertyChanged(nameof(DiemSo));
            }
        }

        private string _diemChu;
        public string DiemChu
        {
            get => _diemChu;
            set
            {
                _diemChu = value;
                OnPropertyChanged(nameof(DiemChu));
            }
        }
    }
}