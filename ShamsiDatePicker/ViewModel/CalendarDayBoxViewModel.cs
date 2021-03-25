using System;
using System.Collections.Generic;
using System.Text;
//using System.Drawing;
using HMExtension.Xamarin.Mvvm;
using Xamarin.Forms;
using System.Windows.Input;

namespace ShamsiDatePicker.ViewModel
{
    internal class CalendarDayBoxViewModel : ViewModelBase
    {
        Action<uint, uint> CallBack = null;
        public CalendarDayBoxViewModel() { }
        public CalendarDayBoxViewModel(Action<uint, uint> CallBack)
        {
            this.CallBack = CallBack;
        }

        #region Property

        private uint _day = 1;
        public uint Day
        {
            get
            {
                return _day;
            }
            set
            {
                if (_day > 0 && _day < 32)
                {
                    if (_day != value)
                    {
                        _day = value;
                        OnPropertyChanged();
                    }
                }
            }
        }

        private uint _month = 1;
        public uint Month
        {
            get
            {
                return _month;
            }
            set
            {
                if (_month > 0 && _month < 13)
                {
                    if (_month != value)
                    {
                        _month = value;
                        OnPropertyChanged();
                    }
                }
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;

                    if(value)
                    {
                        if (CalendarData.SelectedDay != null)
                        {
                            CalendarData.SelectedDay.IsSelected = false;
                        }

                        CalendarData.SelectedDay = this;

                        CallBack(Month, Day);
                    }

                    OnPropertyChanged();
                }
            }
        }

        private bool _isToday = false;
        public bool IsToday
        {
            get
            {
                return _isToday;
            }
            set
            {
                if(_isToday != value)
                {
                    _isToday = value;
                    OnPropertyChanged();
                }
            }
        }

        private Color _calendarTextColor = Color.Black;
        public Color CalendarTextColor
        {
            get
            {
                return _calendarTextColor;
            }
            set
            {
                if (_calendarTextColor != value)
                {
                    _calendarTextColor = value;
                    OnPropertyChanged();
                }
            }
        }

        private Color _calendarSelectedTextColor = Color.White;
        public Color CalendarSelectedTextColor
        {
            get
            {
                return _calendarSelectedTextColor;
            }
            set
            {
                if (_calendarSelectedTextColor != value)
                {
                    _calendarSelectedTextColor = value;
                    OnPropertyChanged();
                }
            }
        }

        private Color _calendarHighlightColor = Color.FromHex("#FF4081");
        public Color CalendarHighlightColor
        {
            get
            {
                return _calendarHighlightColor;
            }
            set
            {
                if (_calendarHighlightColor != value)
                {
                    _calendarHighlightColor = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Command

        private ICommand _tapCommand = null;
        public ICommand TapCommand
        {
            get
            {
                if(_tapCommand == null)
                {
                    _tapCommand = new Command(() =>
                    {
                        IsSelected = true;
                    });
                }

                return _tapCommand;
            }
            set
            {
                _tapCommand = value;
            }
        }

        #endregion 
    }
}
