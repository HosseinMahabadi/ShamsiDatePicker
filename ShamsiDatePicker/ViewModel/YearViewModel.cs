using System;
using System.Collections.Generic;
using System.Text;
using HMExtension.Xamarin.Mvvm;

namespace ShamsiDatePicker.ViewModel
{
    internal class YearViewModel : ViewModelBase
    {
        private int _year = 1300;
        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                if(_year != value)
                {
                    _year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        public override string ToString()
        {
            return Year.ToString();
        }
    }
}
