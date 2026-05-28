using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Tuan_13.Model;

namespace Tuan_13.ViewModels
{
    public class bai_1_14ViewModel : INotifyPropertyChanged
    {
        private string _selectedMaLop;

        public ObservableCollection<string> DanhSachMaLop { get; set; }

        public string SelectedMaLop
        {
            get { return _selectedMaLop; }
            set
            {
                _selectedMaLop = value;
                OnPropertyChanged("SelectedMaLop");
            }
        }

        public bai_1_14ViewModel()
        {
            LoadMaLop();
        }

        private void LoadMaLop()
        {
            using (QLSINHVIENEntities db = new QLSINHVIENEntities())
            {
                DanhSachMaLop = new ObservableCollection<string>(
                    db.Lop.Select(l => l.MaLop).ToList()
                );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}