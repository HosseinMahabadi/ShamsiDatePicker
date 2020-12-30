using System;
using System.Collections.Generic;
using System.Text;
using HMExtension.Xamarin.Mvvm;
using System.Windows.Input;
using ShamsiDatePicker.View;

namespace ShamsiDatePicker.ViewModel
{
    internal class CalendarViewModel : ViewModelBase
    {
        private List<CarouselItem> _carouselItems = null;
        public List<CarouselItem> CarouselItems
        {
            get
            {
                return _carouselItems;
            }
            set
            {
                if (_carouselItems != value)
                {
                    _carouselItems = value;
                    OnPropertyChanged();
                }
            }
        }

        private CalendarDayBoxView _selectedItem = null;
        public CalendarDayBoxView SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;

                    OnPropertyChanged("SelectedItem");
                    OnPropertyChanged("MonthName");
                    OnPropertyChanged("YearString");
                }
            }
        }

        private int _carouselPosition = 0;
        public int CarouselPosition
        {
            get
            {
                return _carouselPosition;
            }
            set
            {
                if (_carouselPosition != value)
                {
                    _carouselPosition = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<YearViewModel> _yearList = new List<YearViewModel>();
        public List<YearViewModel> YearList
        {
            get
            {
                return _yearList;
            }
            set
            {
                if (_yearList != value)
                {
                    _yearList = value;
                    OnPropertyChanged("YearList");
                }
            }
        }

    }
}
