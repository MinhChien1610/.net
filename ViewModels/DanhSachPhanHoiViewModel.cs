using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Tuan_7.Helper;
using Tuan_7.Models;

namespace Tuan_7.ViewModels
{
    public class DanhSachPhanHoiViewModel : BaseViewModel
    {
        private readonly ObservableCollection<PhanHoiModel> _khoPhanHoi;

        private ObservableCollection<PhanHoiModel> _danhSachPhanHoi;
        public ObservableCollection<PhanHoiModel> DanhSachPhanHoi
        {
            get => _danhSachPhanHoi;
            set
            {
                _danhSachPhanHoi = value;
                OnPropertyChanged();
                CapNhatThongKe();
            }
        }

        private PhanHoiModel _phanHoiDuocChon;
        public PhanHoiModel PhanHoiDuocChon
        {
            get => _phanHoiDuocChon;
            set
            {
                _phanHoiDuocChon = value;
                OnPropertyChanged();
            }
        }

        private int _tongPhanHoi;
        public int TongPhanHoi
        {
            get => _tongPhanHoi;
            set
            {
                _tongPhanHoi = value;
                OnPropertyChanged();
            }
        }

        private string _diemTrungBinh;
        public string DiemTrungBinh
        {
            get => _diemTrungBinh;
            set
            {
                _diemTrungBinh = value;
                OnPropertyChanged();
            }
        }

        private int _chuaXuLy;
        public int ChuaXuLy
        {
            get => _chuaXuLy;
            set
            {
                _chuaXuLy = value;
                OnPropertyChanged();
            }
        }

        public ICommand LamMoiCommand { get; set; }

        public DanhSachPhanHoiViewModel(ObservableCollection<PhanHoiModel> khoPhanHoi)
        {
            _khoPhanHoi = khoPhanHoi;
            DanhSachPhanHoi = new ObservableCollection<PhanHoiModel>();

            LamMoiCommand = new RelayCommand(LamMoi);

            RefreshDanhSach();
        }

        public void RefreshDanhSach()
        {
            DanhSachPhanHoi = new ObservableCollection<PhanHoiModel>(
                _khoPhanHoi.OrderByDescending(x => x.NgayRaw));
        }

        private void LamMoi(object obj)
        {
            RefreshDanhSach();
        }

        private void CapNhatThongKe()
        {
            TongPhanHoi = DanhSachPhanHoi.Count;

            if (TongPhanHoi == 0)
                DiemTrungBinh = "0.0";
            else
                DiemTrungBinh = DanhSachPhanHoi.Average(x => x.DiemTB).ToString("0.0");

            ChuaXuLy = DanhSachPhanHoi.Count(x => !x.DaXuLy);
        }
    }
}