using System;
using ShamsiDatePicker.ViewModel;
using HMExtension.Maui;
using ShamsiDatePicker.View;
using System.Threading.Tasks;
using System.Diagnostics;
using HMControls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace ShamsiDatePicker;

public class ShamsiDatePicker : KeyboardlessEntry
{
    public ShamsiDatePicker()
    {
        try
        {
            DateType temp = new(Date)
            {
                Calendar = CalendarType.Shamsi
            };
            ShamsiDateString = temp.GetDateString('/');
            Text = ShamsiDateString;
            SetBinding(TextProperty, new Binding
            {
                Source = this,
                Path = nameof(ShamsiDateString),
            });
            Globals.ShamsiSelectedDate = temp;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    public override void ActionOnFocus()
    {
        ShowCalendar();
    }

    public async void ShowCalendar()
    {
        try
        {
            CalendarPage RootPage = new()
            {
                DataContext = new()
                {
                    MaxYear = MaximumShamsiYear,
                    MinYear = MinimumShamsiYear,
                    DateCallBack = CloseCalendar,
                }
            };
            await Navigation.PushModalAsync(RootPage, false);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    private async void CloseCalendar(DateTime date, bool IsCancel = false)
    {
        try
        {
            var Result = await Navigation.PopModalAsync(true);
            Result = null;

            if (!IsCancel)
            {
                Date = date;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    public Color HeaderBackgroundColor
    {
        get => (Color)GetValue(HeaderBackgroundColorProperty);
        set => SetValue(HeaderBackgroundColorProperty, value);
    }

    public static readonly BindableProperty HeaderBackgroundColorProperty = BindableProperty.Create(
        nameof(HeaderBackgroundColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Color.FromArgb("#FF4081"),
        propertyChanged: OnHeaderBackgroundColorChanged);

    private static void OnHeaderBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.HeaderBackgroundColor = (Color)newValue;
    }

    public Color HeaderTitleTextColor
    {
        get => (Color)GetValue(HeaderTitleTextColorProperty);
        set => SetValue(HeaderTitleTextColorProperty, value);
    }

    public static readonly BindableProperty HeaderTitleTextColorProperty = BindableProperty.Create(
        nameof(HeaderTitleTextColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Colors.White,
        propertyChanged: OnHeaderTitleTextColorChanged);

    private static void OnHeaderTitleTextColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.HeaderTitleTextColor = (Color)newValue;
    }

    public Color HeaderSubTitleTextColor
    {
        get => (Color)GetValue(HeaderSubTitleTextColorProperty);
        set => SetValue(HeaderSubTitleTextColorProperty, value);
    }

    public static readonly BindableProperty HeaderSubTitleTextColorProperty = BindableProperty.Create(
        nameof(HeaderSubTitleTextColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Colors.White,
        propertyChanged: OnHeaderSubTitleTextColorChanged);

    private static void OnHeaderSubTitleTextColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.HeaderSubTitleTextColor = (Color)newValue;
    }

    public Color CalendarBackgroundColor
    {
        get => (Color)GetValue(CalendarBackgroundColorProperty);
        set => SetValue(CalendarBackgroundColorProperty, value);
    }

    public static readonly BindableProperty CalendarBackgroundColorProperty = BindableProperty.Create(
        nameof(CalendarBackgroundColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Colors.White,
        propertyChanged: OnCalendarBackgroundColorChanged);

    private static void OnCalendarBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.CalendarBackgroundColor = (Color)newValue;
    }

    public Color CalendarTextColor
    {
        get => (Color)GetValue(CalendarTextColorProperty);
        set => SetValue(CalendarTextColorProperty, value);
    }

    public static readonly BindableProperty CalendarTextColorProperty = BindableProperty.Create(
        nameof(CalendarTextColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Colors.Black,
        propertyChanged: OnCalendarTextColorChanged);

    private static void OnCalendarTextColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.CalendarTextColor = (Color)newValue;
    }

    public Color CalendarSelectedTextColor
    {
        get => (Color)GetValue(CalendarSelectedTextColorProperty);
        set => SetValue(CalendarSelectedTextColorProperty, value);
    }
    public static readonly BindableProperty CalendarSelectedTextColorProperty = BindableProperty.Create(
        nameof(CalendarSelectedTextColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Colors.White,
        propertyChanged: OnCalendarSelectedTextColorChanged);

    private static void OnCalendarSelectedTextColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.CalendarSelectedTextColor = (Color)newValue;
    }

    public Color CalendarHighlightColor
    {
        get => (Color)GetValue(CalendarHighlightColorProperty);
        set => SetValue(CalendarHighlightColorProperty, value);
    }

    public static readonly BindableProperty CalendarHighlightColorProperty = BindableProperty.Create(
        nameof(CalendarHighlightColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Color.FromArgb("#FF4081"),
        propertyChanged: OnCalendarHighlightColorChanged);

    private static void OnCalendarHighlightColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.CalendarHighlightColor = (Color)newValue;
    }

    public Color CalendarTitleColor
    {
        get => (Color)GetValue(CalendarTitleColorProperty);
        set => SetValue(CalendarTitleColorProperty, value);
    }

    public static readonly BindableProperty CalendarTitleColorProperty = BindableProperty.Create(
        nameof(CalendarTitleColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Colors.Black,
        propertyChanged: OnCalendarTitleColorChanged);

    private static void OnCalendarTitleColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.CalendarTitleColor = (Color)newValue;
    }

    public Color CalendarSubTitleColor
    {
        get => (Color)GetValue(CalendarSubTitleColorProperty);
        set => SetValue(CalendarSubTitleColorProperty, value);
    }

    public static readonly BindableProperty CalendarSubTitleColorProperty = BindableProperty.Create(
        nameof(CalendarSubTitleColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Colors.Black,
        propertyChanged: OnCalendarSubTitleColorChanged);

    private static void OnCalendarSubTitleColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.CalendarSubTitleColor = (Color)newValue;
    }

    public Color CalendarOKButtonTextColor
    {
        get => (Color)GetValue(CalendarOKButtonTextColorProperty);
        set => SetValue(CalendarOKButtonTextColorProperty, value);
    }

    public static readonly BindableProperty CalendarOKButtonTextColorProperty = BindableProperty.Create(
        nameof(CalendarOKButtonTextColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Color.FromArgb("#FF4081"),
        propertyChanged: OnCalendarOKButtonTextColorChanged);

    private static void OnCalendarOKButtonTextColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.CalendarOKButtonTextColor = (Color)newValue;
    }

    public Color CalendarOKButtonBackgroundColor
    {
        get => (Color)GetValue(CalendarOKButtonBackgroundColorProperty);
        set => SetValue(CalendarOKButtonBackgroundColorProperty, value);
    }

    public static readonly BindableProperty CalendarOKButtonBackgroundColorProperty = BindableProperty.Create(
        nameof(CalendarOKButtonBackgroundColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Colors.Transparent,
        propertyChanged: OnCalendarOKButtonBackgroundColorChanged);

    private static void OnCalendarOKButtonBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.CalendarOKButtonBackgroundColor = (Color)newValue;
    }

    public Color CalendarCancelButtonTextColor
    {
        get => (Color)GetValue(CalendarCancelButtonTextColorProperty);
        set => SetValue(CalendarCancelButtonTextColorProperty, value);
    }

    public static readonly BindableProperty CalendarCancelButtonTextColorProperty = BindableProperty.Create(
        nameof(CalendarCancelButtonTextColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Color.FromArgb("#FF4081"),
        propertyChanged: OnCalendarCancelButtonTextColorChanged);

    private static void OnCalendarCancelButtonTextColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.CalendarCancelButtonTextColor = (Color)newValue;
    }

    public Color CalendarCancelButtonBackgroundColor
    {
        get => (Color)GetValue(CalendarCancelButtonBackgroundColorProperty);
        set => SetValue(CalendarCancelButtonBackgroundColorProperty, value);
    }

    public static readonly BindableProperty CalendarCancelButtonBackgroundColorProperty = BindableProperty.Create(
        nameof(CalendarCancelButtonBackgroundColor),
        typeof(Color),
        typeof(ShamsiDatePicker),
        defaultValue: Colors.Transparent,
        propertyChanged: OnCalendarCancelButtonBackgroundColorChanged);

    private static void OnCalendarCancelButtonBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Globals.CalendarCancelButtonBackgroundColor = (Color)newValue;
    }

    //*****************************************************************************
    //**********************************************************
    //*************************************************

    public DateTime Date
    {
        get => (DateTime)GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }

    public static readonly BindableProperty DateProperty = BindableProperty.Create(
        nameof(Date),
        typeof(DateTime),
        typeof(ShamsiDatePicker),
        defaultValue: DateTime.Now,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnDateChanged);

    private static void OnDateChanged(BindableObject sender, object oldValue, object newValue)
    {
        try
        {
            var Object = sender as ShamsiDatePicker;
            Object.OnDateChanged(oldValue, newValue);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Date1 Error!!!! " + ex.Message);
        }
    }

    private void OnDateChanged(object oldValue, object newValue)
    {
        try
        {
            if (newValue != null)
            {
                DateType temp = new((DateTime)newValue)
                {
                    Calendar = CalendarType.Shamsi
                };

                ShamsiDateString = temp.GetDateString('/');
                Globals.SelectedDate = (DateTime)newValue;
            }
            else
            {
                ShamsiDateString = null;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Date2 Error!!!! " + ex.Message);
        }
    }

    private string _shamsiDateString = null;
    public string ShamsiDateString
    {
        get => _shamsiDateString;
        set 
        {
            if(_shamsiDateString != value)
            {
                _shamsiDateString = value;
                OnPropertyChanged();
            }
        }
    }

    public int MinimumShamsiYear
    {
        get => (int)GetValue(MinimumShamsiYearProperty);
        set
        {
            if (value <= MaximumShamsiYear)
            {
                SetValue(MinimumShamsiYearProperty, value);
            }
            else
            {
                throw new ArgumentOutOfRangeException(
                    "Minimum shamsi year must be smaller than maximum shamsi year.");
            }
        }
    }

    public static readonly BindableProperty MinimumShamsiYearProperty = BindableProperty.Create(
        nameof(MinimumShamsiYear),
        typeof(int),
        typeof(ShamsiDatePicker),
        propertyChanged: OnMinimumDateChanged,
        defaultValue: 1300);

    private static void OnMinimumDateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var owner = bindable as ShamsiDatePicker;
        owner.OnMinimumDateChanged(oldValue, newValue);
    }

    private void OnMinimumDateChanged(object oldValue, object newValue)
    {
        try
        {
            var temp = Convert.ToInt32(newValue);
            //MessagingCenter.Send(this, Globals.Messages[MessageType.MinYearIsChanged], temp);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int MaximumShamsiYear
    {
        get => (int)GetValue(MaximumShamsiYearProperty);
        set
        {
            if (value >= MinimumShamsiYear)
            {
                SetValue(MaximumShamsiYearProperty, value);
            }
            else
            {
                throw new ArgumentOutOfRangeException(
                    "Maximum shamsi year must be bigger than minimum shamsi year.");
            }
        }
    }

    public static readonly BindableProperty MaximumShamsiYearProperty = BindableProperty.Create(
        nameof(MaximumShamsiYear),
        typeof(int),
        typeof(ShamsiDatePicker),
        defaultValue: 1500);
}
