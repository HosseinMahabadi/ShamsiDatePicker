using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HMExtension.Xamarin;

namespace ShamsiDatePicker.ViewModel
{
    internal class ShareViewModelBase : ViewModelBase
    {
        public ShareViewModelBase()
        {
            var date = new DateType(SelectedDate);
            date.Calendar = CalendarType.Shamsi;
            ShamsiSelectedDate = date;

            /*MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.HeaderBackgroundColorIsChanged],
                (sender, arg) =>
                {
                    HeaderBackgroundColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.HeaderTitleTextColorIsChanged],
                (sender, arg) =>
                {
                    HeaderTitleTextColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.HeaderSubTitleTextColorIsChanged],
                (sender, arg) =>
                {
                    HeaderSubTitleTextColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.CalendarBackgroundColorIsChanged],
                (sender, arg) =>
                {
                    CalendarBackgroundColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.CalendarTextColorIsChanged],
                (sender, arg) =>
                {
                    CalendarTextColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.CalendarSelectedTextColorIsChanged],
                (sender, arg) =>
                {
                    CalendarSelectedTextColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.CalendarHighlightColorIsChanged],
                (sender, arg) =>
                {
                    CalendarHighlightColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.CalendarTitleColorIsChanged],
                (sender, arg) =>
                {
                    CalendarTitleColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.CalendarSubTitleColorIsChanged],
                (sender, arg) =>
                {
                    CalendarSubTitleColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.CalendarOKButtonTextColorIsChanged],
                (sender, arg) =>
                {
                    CalendarOKButtonTextColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.CalendarOKButtonBackgroundColorIsChanged],
                (sender, arg) =>
                {
                    CalendarOKButtonBackgroundColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.CalendarCancelButtonTextColorIsChanged],
                (sender, arg) =>
                {
                    CalendarCancelButtonTextColor = arg;
                });
            MessagingCenter.Subscribe<ShamsiDatePicker, Color>(this,
                Globals.Messages[MessageType.CalendarCancelButtonBackgroundColorIsChanged],
                (sender, arg) =>
                {
                    CalendarCancelButtonBackgroundColor = arg;
                });*/

        }
        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value, null, () =>
            {
                var date = new DateType(value);
                date.Calendar = CalendarType.Shamsi;
                ShamsiSelectedDate = date;
            });
        }

        private DateType _shamsiSelectedDate = null;
        public DateType ShamsiSelectedDate
        {
            get => _shamsiSelectedDate;
            set => SetProperty(ref _shamsiSelectedDate, value);
        }

        private CalendarDayBoxViewModel _selectedDayBox = null;
        public CalendarDayBoxViewModel SelectedDayBox
        {
            get => _selectedDayBox;
            set
            {
                if(value != _selectedDayBox && _selectedDayBox != null)
                {
                    _selectedDayBox.Unselect();
                }
                SetProperty(ref _selectedDayBox, value);
            }
        }

        private Color _headerBackgroundColor = Color.FromHex("#FF4081");
        public Color HeaderBackgroundColor
        {
            get => _headerBackgroundColor;
            set => SetProperty(ref _headerBackgroundColor, value);
        }

        private Color _headerTitleTextColor = Color.White;
        public Color HeaderTitleTextColor
        {
            get => _headerTitleTextColor;
            set => SetProperty(ref _headerTitleTextColor, value);
        }

        private Color _headerSubTitleTextColor = Color.White;
        public Color HeaderSubTitleTextColor
        {
            get => _headerSubTitleTextColor;
            set => SetProperty(ref _headerSubTitleTextColor, value);
        }

        private Color _calendarBackgroundColor = Color.White;
        public Color CalendarBackgroundColor
        {
            get => _calendarBackgroundColor;
            set => SetProperty(ref _calendarBackgroundColor, value);
        }

        private Color _calendarTextColor = Color.Black;
        public Color CalendarTextColor
        {
            get => _calendarTextColor;
            set => SetProperty(ref _calendarTextColor, value);
        }

        private Color _calendarSelectedTextColor = Color.White;
        public Color CalendarSelectedTextColor
        {
            get => _calendarSelectedTextColor;
            set => SetProperty(ref _calendarSelectedTextColor, value);
        }

        private Color _calendarHighlightColor = Color.FromHex("#FF4081");
        public Color CalendarHighlightColor
        {
            get => _calendarHighlightColor;
            set => SetProperty(ref _calendarHighlightColor, value);
        }

        private Color _calendarTitleColor = Color.Black;
        public Color CalendarTitleColor
        {
            get => _calendarTitleColor;
            set => SetProperty(ref _calendarTitleColor, value);
        }

        private Color _calendarSubTitleColor = Color.Black;
        public Color CalendarSubTitleColor
        {
            get => _calendarSubTitleColor;
            set => SetProperty(ref _calendarSubTitleColor, value);
        }

        private Color _calendarOKButtonTextColor = Color.FromHex("#FF4081");
        public Color CalendarOKButtonTextColor
        {
            get => _calendarOKButtonTextColor;
            set => SetProperty(ref _calendarOKButtonTextColor, value);
        }

        private Color _calendarOKButtonBackgroundColor = Color.Transparent;
        public Color CalendarOKButtonBackgroundColor
        {
            get => _calendarOKButtonBackgroundColor;
            set => SetProperty(ref _calendarOKButtonBackgroundColor, value);
        }

        private Color _calendarCancelButtonTextColor = Color.FromHex("#FF4081");
        public Color CalendarCancelButtonTextColor
        {
            get => _calendarCancelButtonTextColor;
            set => SetProperty(ref _calendarCancelButtonTextColor, value);
        }

        private Color _calendarCancelButtonBackgroundColor = Color.Transparent;
        public Color CalendarCancelButtonBackgroundColor
        {
            get => _calendarCancelButtonBackgroundColor;
            set => SetProperty(ref _calendarCancelButtonBackgroundColor, value);
        }

    }
}
