using System;
using System.Collections.Generic;
using System.Text;
using ShamsiDatePicker.ViewModel;
using System.Diagnostics;
using HMExtension.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace ShamsiDatePicker.View;

internal class CalendarDayBoxView : Frame
{
    public CalendarDayBoxView(int month, int day)
    {
        try
        {
            BindingContext = this;

            Month = month;
            Day = day;
            TapCommand = new Command(Select);
            BackgroundColor = Colors.Transparent;
            CornerRadius = 60;
            Padding = 0;
            HasShadow = false;

            Label label = new()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 18.5,
                FontFamily = "B_Nazanin",
                TextColor = Globals.CalendarTextColor,
                Text = Day.ToString(),
            };

            DataTrigger isSelectedTriggerFrame = new(typeof(Frame))
            {
                Value = true,
                Binding = new Binding() { Path = nameof(IsSelected) },
            };

            DataTrigger isSelectedTriggerLabel = new(typeof(Label))
            {
                Value = true,
                Binding = new Binding() { Path = nameof(IsSelected) },
            };

            DataTrigger isEnabledTriggerLabel = new(typeof(Label))
            {
                Value = false,
                Binding = new Binding() { Path = nameof(IsEnabled) },
            };

            isSelectedTriggerFrame.Setters.Add(new Setter()
            {
                Property = BackgroundColorProperty,
                Value = Globals.CalendarHighlightColor,
            });

            isSelectedTriggerFrame.Setters.Add(new Setter()
            {
                Property = ScaleProperty,
                Value = 1.2,
            });

            isSelectedTriggerLabel.Setters.Add(new Setter()
            {
                Property = Label.TextColorProperty,
                Value = Globals.CalendarSelectedTextColor,
            });

            isEnabledTriggerLabel.Setters.Add(new Setter()
            {
                Property = Label.TextColorProperty,
                Value = Colors.DarkGray,
            });

            Triggers.Add(isSelectedTriggerFrame);
            label.Triggers.Add(isSelectedTriggerLabel);
            label.Triggers.Add(isEnabledTriggerLabel);

            TapGestureRecognizer thisTaped = new();
            thisTaped.SetBinding(TapGestureRecognizer.CommandProperty, new Binding()
            {
                Path = nameof(TapCommand)
            });
            GestureRecognizers.Add(thisTaped);

            Content = label;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    public void Select()
    {
        try
        {
            IsSelected = true;
            //SelectedColor = Globals.CalendarHighlightColor;
            MessagingCenter.Send(this, Globals.Messages[MessageType.NewDayIsSelected], this);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    public void Unselect()
    {
        IsSelected = false;
        //SelectedColor = Colors.Transparent;
    }

    #region Property

    //public Microsoft.Maui.Graphics.Color SelectedColor { get; set; } = Colors.Transparent;


    public int Month { get; set; } = 0;

    public int Day { get; set; } = 0;

    private bool _isSelected = false;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _isToday = false;
    public bool IsToday
    {
        get => _isToday;
        set
        {
            if (_isToday != value)
            {
                _isToday = value;
                OnPropertyChanged();
            }
        }
    }

    #endregion

    #region Command

    private Command _tapCommand = null;
    public Command TapCommand
    {
        get => _tapCommand;
        set 
        {
            if (_tapCommand != value)
            {
                _tapCommand = value;
                OnPropertyChanged();
            }
        }
    }

    #endregion

}
