using System;
using System.Collections.Generic;
using System.Text;
using HMExtension.Xamarin.Mvvm;

namespace ShamsiDatePicker.ViewModel
{
    internal static class CalendarData
    {
        public static CalendarDayBoxViewModel SelectedDay { get; set; } = null;
        public static DateTime SelectedDate { get; set; } = DateTime.Now;
        public static int MinYear { get; set; } = 1300;
        public static int MaxYear { get; set; } = 1400;
    }
}
