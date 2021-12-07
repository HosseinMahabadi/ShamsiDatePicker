using System;
using System.Collections.Generic;
using System.Text;
//using System.Drawing;
using HMExtension.Xamarin.Mvvm;
using Xamarin.Forms;
using System.Windows.Input;

namespace ShamsiDatePicker.ViewModel
{
    internal class CalendarDayBoxViewModel : ShareViewModelBase
    {
        public void Select()
        {
            try
            {
                IsSelected = true;
                SelectedColor = CalendarHighlightColor;
                MessagingCenter.Send(this, Globals.Messages[MessageType.NewDayIsSelected], this);
            }
            catch { }
        }

        public void Unselect()
        {
            IsSelected = false;
            SelectedColor = Color.Transparent;
        }

        #region Property

        private Color _selectedColor = Color.Transparent;
        public Color SelectedColor
        {
            get => _selectedColor;
            set => SetProperty(ref _selectedColor, value);
        }

        private uint _day = 1;
        public uint Day
        {
            get => _day;
            set => SetProperty(ref _day, value);
        }

        private uint _month = 1;
        public uint Month
        {
            get => _month;
            set => SetProperty(ref _month, value);
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        private bool _isToday = false;
        public bool IsToday
        {
            get => _isToday;
            set => SetProperty(ref _isToday, value);
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
                        Select();
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
