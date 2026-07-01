using Tuan_10.Helper;
using Tuan_10.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Tuan_10.ViewModels
{
    public class KhoaViewModel : BaseViewModel
    {
        private QLSINHVIENEntities db = new QLSINHVIENEntities();

        public ObservableCollection<Khoa> DS_Khoa { get; set; }

        private Khoa _SelectedKhoa;
        public Khoa SelectedKhoa
        {
            get => _SelectedKhoa;
            set
            {
                _SelectedKhoa = value;
                OnPropertyChanged(nameof(SelectedKhoa));
            }
        }

        private string _MaKhoaInput;
        public string MaKhoaInput
        {
            get => _MaKhoaInput;
            set
            {
                _MaKhoaInput = value;
                OnPropertyChanged(nameof(MaKhoaInput));
            }
        }

        private string _TenKhoaInput;
        public string TenKhoaInput
        {
            get => _TenKhoaInput;
            set
            {
                _TenKhoaInput = value;
                OnPropertyChanged(nameof(TenKhoaInput));
            }
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public KhoaViewModel()
        {
            LoadData();
            CommandSetup();
        }

        void LoadData()
        {
            DS_Khoa = new ObservableCollection<Khoa>(db.Khoa.ToList());
            OnPropertyChanged(nameof(DS_Khoa));
        }

        void CommandSetup()
        {
            // THÊM
            AddCommand = new RelayCommand(_ =>
            {
                if (string.IsNullOrWhiteSpace(MaKhoaInput)) return;

                var k = new Khoa
                {
                    MaKhoa = MaKhoaInput,
                    TenKhoa = TenKhoaInput
                };

                db.Khoa.Add(k);
                db.SaveChanges();
                LoadData();
            });

            // SỬA
            EditCommand = new RelayCommand(_ =>
            {
                if (SelectedKhoa == null) return;

                var k = db.Khoa.FirstOrDefault(x => x.MaKhoa == SelectedKhoa.MaKhoa);
                if (k != null)
                {
                    k.TenKhoa = SelectedKhoa.TenKhoa;
                    db.SaveChanges();
                    LoadData();
                }
            });

            // XÓA
            DeleteCommand = new RelayCommand(_ =>
            {
                if (SelectedKhoa == null) return;

                var result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa?",
                    "Xác nhận",
                    MessageBoxButton.YesNo);

                if (result != MessageBoxResult.Yes) return;

                try
                {
                    var k = db.Khoa.FirstOrDefault(x => x.MaKhoa == SelectedKhoa.MaKhoa);
                    if (k != null)
                    {
                        db.Khoa.Remove(k);
                        db.SaveChanges();
                        LoadData();
                    }
                }
                catch
                {
                    MessageBox.Show("Không thể xóa dữ liệu này!");
                }
            });
        }
    }
}