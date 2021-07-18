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
            get => _foreColor;
            set => SetProperty(ref _foreColor, value);
        }

        private double _fontSize = 22;
        public double FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }
    }
}
