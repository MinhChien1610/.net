using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuan_6.ViewModels;

namespace Tuan_6.Model
{
    public class SinhVien : BaseViewModel
    {
        private string _studentId;
        public string StudentId
        {
            get => _studentId;
            set
            {
                _studentId = value;
                OnPropertyChanged();
            }
        }

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        private double _score1;
        public double Score1
        {
            get => _score1;
            set
            {
                _score1 = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AverageScore));
            }
        }

        private double _score2;
        public double Score2
        {
            get => _score2;
            set
            {
                _score2 = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AverageScore));
            }
        }

        private double _score3;
        public double Score3
        {
            get => _score3;
            set
            {
                _score3 = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AverageScore));
            }
        }

        private string _className;
        public string ClassName
        {
            get => _className;
            set
            {
                _className = value;
                OnPropertyChanged();
            }
        }

        public double AverageScore => (Score1 + Score2 + Score3) / 3.0;
    }
}