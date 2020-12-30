using System;
using System.Collections.Generic;
using System.Text;
using HMExtension.Xamarin.Mvvm;
using HMExtension.Xamarin.Component;
using ShamsiDatePicker.View;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace ShamsiDatePicker.ViewModel
{
    internal class CarouselItem : ViewModelBase
    {
        Date MyDate = new Date();
        public CarouselItem(int Year, int Month)
        {
            this.Year = Year;
            this.Month = Month;
        }

        private int _month;
        public int Month
        {
            get
            {
                return _month;
            }
            set
            {
                if (_month != value)
                {
                    _month = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _year;
        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Header
        {
            get
            {
                return ((ShamsiMonthType)(Month - 1)).ToString() + " " + Year.ToString();
            }
        }

        private List<CalendarDayBoxView> _days = null;
        public List<CalendarDayBoxView> Days
        {
            get
            {
                return _days;
            }
            set
            {
                if (_days != value)
                {
                    _days = value;
                    OnPropertyChanged("Days");
                }
            }
        }

    }
}
