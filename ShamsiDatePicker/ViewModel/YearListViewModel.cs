using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HMExtension.Xamarin.Mvvm;
using System.Windows.Input;
using HMExtension.Xamarin.Component;
namespace ShamsiDatePicker.ViewModel
{
    internal class YearListViewModel : ViewModelBase
    {
        public int YearNumber { get; set; } = 0;

        private Color _foreColor = Color.Black;
        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                if(value != _foreColor)
                {
                    _foreColor = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _fontSize = 22;

        public double FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                if(value != _fontSize)
                {
                    _fontSize = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
