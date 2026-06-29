using Tuan_6.ViewModels;

namespace Tuan_6.Model
{
    public class LopHoc : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}