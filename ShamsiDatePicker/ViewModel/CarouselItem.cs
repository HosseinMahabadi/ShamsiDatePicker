using System;
using System.Collections.Generic;
using System.Text;
using HMExtension.Maui;
using ShamsiDatePicker.View;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace ShamsiDatePicker.ViewModel;

internal class CarouselItem : ViewModelBase
{
    public CarouselItem(int month)
    {
        Month = month;
    }

    private int _month = 0;
    public int Month
    {
        get => _month;
        set => SetProperty(ref _month, value);
    }

    private int _year = 0;
    public int Year
    {
        get => _year;
        set => SetProperty(ref _year, value, nameof(Year), ()=> OnPropertyChanged(nameof(Header)));
    }

    public string Header
    {
        get
        {
            return ((ShamsiMonthType)(Month - 1)).ToString() + " " + Year.ToString();
        }
    }

    private List<CalendarDayBoxView> _days = [];
    public List<CalendarDayBoxView> Days
    {
        get => _days;
        set => SetProperty(ref _days, value);
    }

    public bool DaysCreated { get; set; } = false;

    public void SetYear(int year)
    {
        try
        {
            Year = year;
            Days ??= [];
            DaysCreated = false;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void CreateDaysIfNeeded()
    {
        if (!DaysCreated)
        {            
            int Column = Globals.GetFirstDayOfShamsiMonth(Month);
            int Row = 0;
            Date MyDate = new();
            int TotalDays = MyDate.GetDaysCountOfMonth(Year, Month);

            List<CalendarDayBoxView> tempDays = [];

            for (int i = 1; i <= TotalDays; i++)
            {
                CalendarDayBoxView dayBox = new(Month, i);

                dayBox.SetValue(Grid.ColumnProperty, Column + 1);
                dayBox.SetValue(Grid.RowProperty, Row);

                tempDays.Add(dayBox);

                Column++;
                if (Column > 6)
                {
                    Column = 0;
                    Row++;
                }
            }
            Days = tempDays;

            CalendarDayBoxView temp = null;

            if (Month == Globals.ShamsiSelectedDate.Month)
            {
                temp = Days.FirstOrDefault(d => d.Day == Globals.ShamsiSelectedDate.Day &&
                    d.Month == Globals.ShamsiSelectedDate.Month);

                if (Globals.ShamsiSelectedDate.Day == 30 && Globals.ShamsiSelectedDate.Month == 12)
                {
                    temp = Days.FirstOrDefault(d => d.Day == 29);
                }
            }

            temp?.Select();
            DaysCreated = true;
            tempDays = null;
            temp = null;
        }
    }
}
