using System;
using System.Collections.Generic;
using System.Text;
using HMExtension.Xamarin.Mvvm;
using HMExtension.Xamarin.Component;
using ShamsiDatePicker.View;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
            get => _month;
            set => SetProperty(ref _month, value);
        }

        private int _year;
        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }
        public string Header
        {
            get
            {
                return ((ShamsiMonthType)(Month - 1)).ToString() + " " + Year.ToString();
            }
        }

        private List<CalendarDayBoxView> _days = new List<CalendarDayBoxView>();
        public List<CalendarDayBoxView> Days
        {
            get => _days;
            set => SetProperty(ref _days, value);
        }

    }
}
