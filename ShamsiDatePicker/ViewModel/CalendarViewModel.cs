using System;
using System.Collections.Generic;
using System.Text;
using HMExtension.Xamarin;
using System.Windows.Input;
using ShamsiDatePicker.View;
using System.Collections.ObjectModel;

namespace ShamsiDatePicker.ViewModel
{
    internal class CalendarViewModel : ViewModelBase, IDisposable
    {
        public override void Dispose()
        {
            CarouselItems = null;
            YearList = null;
            base.Dispose();
        }

        private List<CarouselItem> _carouselItems = null;
        public List<CarouselItem> CarouselItems
        {
            get => _carouselItems;
            set => SetProperty(ref _carouselItems, value);
        }

        private CalendarDayBoxView _selectedItem = null;
        public CalendarDayBoxView SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private int _carouselPosition = 0;
        public int CarouselPosition
        {
            get => _carouselPosition;
            set => SetProperty(ref _carouselPosition, value);
        }

        private List<YearViewModel> _yearList = new List<YearViewModel>();
        public List<YearViewModel> YearList
        {
            get => _yearList;
            set => SetProperty(ref _yearList, value);
        }
    }
}
