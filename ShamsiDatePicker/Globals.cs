using System;
using System.Collections.Generic;
using System.Text;
using ShamsiDatePicker.ViewModel;
using HMExtension.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace ShamsiDatePicker;

internal static class Globals
{
    public static int Position = 0;

    private static Date MyDate { get; set; } = new();

    private static int _activeYear = 0;
    public static int ActiveYear
    {
        get => _activeYear;
        set
        {
            if (_activeYear != value)
            {
                _activeYear = value;
                System.DayOfWeek StartDayOfWeek = MyDate.GetDayOfWeekForShamsiCalendar(value, 1, 1);
                FirstDayOfShamsiYear = MyDate.GetDayOfWeek(StartDayOfWeek);
            }
        }
    }

    private static DateTime _selectedDate = DateTime.Now;
    public static DateTime SelectedDate
    {
        get => _selectedDate;
        internal set 
        {
            if (_selectedDate != value)
            {
                _selectedDate = value;
                DateType date = new(value)
                {
                    Calendar = CalendarType.Shamsi
                };
                ShamsiSelectedDate = date;
            }
        }
    }

    public static DateType ShamsiSelectedDate { get; set; }
    
    public static int FirstDayOfShamsiYear { get; internal set; } = -1;

    public static Color HeaderBackgroundColor { get; internal set; } = Color.FromArgb("#FF4081");
    public static Color HeaderTitleTextColor { get; internal set; } = Colors.White;
    public static Color HeaderSubTitleTextColor { get; internal set; } = Colors.White;
    public static Color CalendarBackgroundColor { get; internal set; } = Colors.White;
    public static Color CalendarTextColor { get; set; } = Colors.Black;
    public static Color CalendarSelectedTextColor { get; set; } = Colors.White;
    public static Color CalendarHighlightColor { get; set; } = Color.FromArgb("#FF4081");
    public static Color CalendarTitleColor { get; internal set; } = Colors.Black;
    public static Color CalendarSubTitleColor { get; internal set; } = Colors.Black;
    public static Color CalendarOKButtonTextColor { get; internal set; } = Color.FromArgb("#FF4081");
    public static Color CalendarOKButtonBackgroundColor { get; internal set; } = Colors.Transparent;
    public static Color CalendarCancelButtonTextColor { get; internal set; } = Color.FromArgb("#FF4081");
    public static Color CalendarCancelButtonBackgroundColor { get; internal set; } = Colors.Transparent;

    public static int GetFirstDayOfShamsiMonth(int month)
    {
        if (FirstDayOfShamsiYear >= 0)
        {
            int sum = FirstDayOfShamsiYear;

            if (month < 7)
            {
                sum += (month - 1) * 31;
            }
            else
            {
                sum += 6 * 31;
                sum += (month - 7) * 30;
            }
            return sum % 7;
        }
        else
        {
            throw new Exception($"{nameof(FirstDayOfShamsiYear)} must bigger or equal 0.");
        }
    }

    public static string AppUniqId = Guid.NewGuid().ToString();
    public static Dictionary<MessageType, string> Messages = new()
    {
        { MessageType.NewDayIsSelected, "NewDayIsSelected" + AppUniqId},
        { MessageType.MinYearIsChanged, "MinYearIsChanged" + AppUniqId},
        { MessageType.MaxYearIsChenged, "MaxYearIsChenged" + AppUniqId},
        { MessageType.HeaderBackgroundColorIsChanged, "HeaderBackgroundColorIsChanged" + AppUniqId},
        { MessageType.HeaderTitleTextColorIsChanged, "HeaderTitleTextColorIsChanged" + AppUniqId},
        { MessageType.HeaderSubTitleTextColorIsChanged, "HeaderSubTitleTextColorIsChanged" + AppUniqId},
        { MessageType.CalendarBackgroundColorIsChanged, "CalendarBackgroundColorIsChanged" + AppUniqId},
        { MessageType.CalendarTextColorIsChanged, "CalendarTextColorIsChanged" + AppUniqId},
        { MessageType.CalendarSelectedTextColorIsChanged, "CalendarSelectedTextColorIsChanged" + AppUniqId},
        { MessageType.CalendarHighlightColorIsChanged, "CalendarHighlightColorIsChanged" + AppUniqId},
        { MessageType.CalendarTitleColorIsChanged, "CalendarTitleColorIsChanged" + AppUniqId},
        { MessageType.CalendarSubTitleColorIsChanged, "CalendarSubTitleColorIsChanged" + AppUniqId},
        { MessageType.CalendarOKButtonTextColorIsChanged, "CalendarOKButtonTextColorIsChanged" + AppUniqId},
        { MessageType.CalendarOKButtonBackgroundColorIsChanged, "CalendarOKButtonBackgroundColorIsChanged" + AppUniqId},
        { MessageType.CalendarCancelButtonTextColorIsChanged, "CalendarCancelButtonTextColorIsChanged" + AppUniqId},
        { MessageType.CalendarCancelButtonBackgroundColorIsChanged, "CalendarCancelButtonBackgroundColorIsChanged" + AppUniqId},
        { MessageType.DateIsChanged, "DateIsChanged" + AppUniqId},
        { MessageType.OkButtonClicked, "OkButtonClicked" + AppUniqId},
        { MessageType.CancelButtonClicked, "CancelButtonClicked" + AppUniqId},
    };
}
