using System;
using System.Collections.Generic;
using System.Text;
using HMExtension.Maui;
using System.Windows.Input;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace ShamsiDatePicker.ViewModel;

internal class YearListViewModel : ViewModelBase
{
    public int YearNumber { get; set; } = 0;

    private Color _foreColor = Colors.Black;
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
