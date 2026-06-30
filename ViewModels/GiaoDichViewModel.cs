using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Tuan_7.Helper;
using Tuan_7.Models;

namespace Tuan_7.ViewModels
{
    public class GiaoDichViewModel : BaseViewModel
    {
        private readonly TaiKhoanViewModel _taiKhoanVM;

        public ObservableCollection<TaiKhoanModel> DanhSachTaiKhoan => _taiKhoanVM.DanhSach;
        public ObservableCollection<GiaoDichModel> DanhSachGiaoDich { get; set; }

        private TaiKhoanModel _taiKhoanNguon;
        public TaiKhoanModel TaiKhoanNguon
        {
            get => _taiKhoanNguon;
            set
            {
                _taiKhoanNguon = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SoDuHienTaiText));
                CapNhatTomTat();
            }
        }

        private TaiKhoanModel _taiKhoanDich;
        public TaiKhoanModel TaiKhoanDich
        {
            get => _taiKhoanDich;
            set
            {
                _taiKhoanDich = value;
                OnPropertyChanged();
                CapNhatTomTat();
            }
        }

        private string _soTien;
        public string SoTien
        {
            get => _soTien;
            set
            {
                _soTien = value;
                OnPropertyChanged();
                CapNhatTomTat();
            }
        }

        private string _noiDung;
        public string NoiDung
        {
            get => _noiDung;
            set
            {
                _noiDung = value;
                OnPropertyChanged();
            }
        }

        private bool _isGuiTien = true;
        public bool IsGuiTien
        {
            get => _isGuiTien;
            set
            {
                _isGuiTien = value;
                OnPropertyChanged();
                if (value)
                {
                    _isRutTien = false;
                    _isChuyenKhoan = false;
                    OnPropertyChanged(nameof(IsRutTien));
                    OnPropertyChanged(nameof(IsChuyenKhoan));
                }
                CapNhatTomTat();
            }
        }

        private bool _isRutTien;
        public bool IsRutTien
        {
            get => _isRutTien;
            set
            {
                _isRutTien = value;
                OnPropertyChanged();
                if (value)
                {
                    _isGuiTien = false;
                    _isChuyenKhoan = false;
                    OnPropertyChanged(nameof(IsGuiTien));
                    OnPropertyChanged(nameof(IsChuyenKhoan));
                }
                CapNhatTomTat();
            }
        }

        private bool _isChuyenKhoan;
        public bool IsChuyenKhoan
        {
            get => _isChuyenKhoan;
            set
            {
                _isChuyenKhoan = value;
                OnPropertyChanged();
                if (value)
                {
                    _isGuiTien = false;
                    _isRutTien = false;
                    OnPropertyChanged(nameof(IsGuiTien));
                    OnPropertyChanged(nameof(IsRutTien));
                }
                CapNhatTomTat();
            }
        }

        public string SoDuHienTaiText
        {
            get
            {
                if (TaiKhoanNguon == null) return "Số dư hiện tại: 0 VNĐ";
                return $"Số dư hiện tại: {TaiKhoanNguon.SoDu:N0} VNĐ";
            }
        }

        public string LoaiGiaoDichText
        {
            get
            {
                if (IsGuiTien) return "Gửi tiền";
                if (IsRutTien) return "Rút tiền";
                if (IsChuyenKhoan) return "Chuyển khoản";
                return "";
            }
        }

        public string SoTienText
        {
            get
            {
                if (!decimal.TryParse(SoTien, out decimal tien)) return "0 VNĐ";
                return $"{tien:N0} VNĐ";
            }
        }

        public string PhiGiaoDichText => $"{TinhPhi():N0} VNĐ";

        public string SoDuSauGiaoDichText
        {
            get
            {
                if (TaiKhoanNguon == null) return "0 VNĐ";
                if (!decimal.TryParse(SoTien, out decimal tien)) return $"{TaiKhoanNguon.SoDu:N0} VNĐ";

                decimal phi = TinhPhi();
                decimal ketQua = TaiKhoanNguon.SoDu;

                if (IsGuiTien)
                    ketQua += tien;
                else
                    ketQua -= (tien + phi);

                return $"{ketQua:N0} VNĐ";
            }
        }

        public ICommand XacNhanGiaoDichCommand { get; set; }

        public GiaoDichViewModel(TaiKhoanViewModel taiKhoanVM)
        {
            _taiKhoanVM = taiKhoanVM;
            DanhSachGiaoDich = new ObservableCollection<GiaoDichModel>();
            XacNhanGiaoDichCommand = new RelayCommand(XacNhanGiaoDich);
        }

        private void XacNhanGiaoDich(object obj)
        {
            if (TaiKhoanNguon == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản nguồn.");
                return;
            }

            if (!decimal.TryParse(SoTien, out decimal tien) || tien <= 0)
            {
                MessageBox.Show("Số tiền không hợp lệ.");
                return;
            }

            decimal phi = TinhPhi();

            if (IsChuyenKhoan)
            {
                if (TaiKhoanDich == null)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản đích.");
                    return;
                }

                if (TaiKhoanNguon == TaiKhoanDich)
                {
                    MessageBox.Show("Tài khoản nguồn và đích không được trùng nhau.");
                    return;
                }

                if (TaiKhoanNguon.SoDu < tien + phi)
                {
                    MessageBox.Show("Số dư không đủ.");
                    return;
                }

                TaiKhoanNguon.SoDu -= (tien + phi);
                TaiKhoanDich.SoDu += tien;
            }
            else if (IsRutTien)
            {
                if (TaiKhoanNguon.SoDu < tien + phi)
                {
                    MessageBox.Show("Số dư không đủ.");
                    return;
                }

                TaiKhoanNguon.SoDu -= (tien + phi);
            }
            else
            {
                TaiKhoanNguon.SoDu += tien;
            }

            DanhSachGiaoDich.Insert(0, new GiaoDichModel
            {
                MaGD = $"GD{DateTime.Now:yyyyMMddHHmmss}",
                Ngay = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                Loai = LoaiGiaoDichText,
                TKNguon = TaiKhoanNguon?.SoTK ?? "",
                TKDich = IsChuyenKhoan ? TaiKhoanDich?.SoTK ?? "" : "",
                SoTien = tien,
                NoiDung = string.IsNullOrWhiteSpace(NoiDung) ? LoaiGiaoDichText : NoiDung,
                PhiGiaoDich = phi,
                NgayTaoRaw = DateTime.Now
            });

            MessageBox.Show("Giao dịch thành công.");
            ResetForm();
        }

        private decimal TinhPhi()
        {
            if (!decimal.TryParse(SoTien, out decimal tien)) return 0;

            if (IsChuyenKhoan) return 3000;
            if (IsRutTien) return 1000;
            return 0;
        }

        private void CapNhatTomTat()
        {
            OnPropertyChanged(nameof(LoaiGiaoDichText));
            OnPropertyChanged(nameof(SoTienText));
            OnPropertyChanged(nameof(PhiGiaoDichText));
            OnPropertyChanged(nameof(SoDuSauGiaoDichText));
            OnPropertyChanged(nameof(SoDuHienTaiText));
        }

        private void ResetForm()
        {
            TaiKhoanNguon = null;
            TaiKhoanDich = null;
            SoTien = "";
            NoiDung = "";
            IsGuiTien = true;
            CapNhatTomTat();
        }
    }
}