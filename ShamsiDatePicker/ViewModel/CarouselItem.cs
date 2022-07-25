using System;
using System.Collections.Generic;
using System.Text;
using HMExtension.Xamarin.Mvvm;
using HMExtension.Xamarin.Component;
using ShamsiDatePicker.View;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;

namespace ShamsiDatePicker.ViewModel
{
    internal class CarouselItem : ShareViewModelBase, IDisposable
    {
        ~CarouselItem()
        {
            Dispose();
        }

        public void Dispose()
        {
            try
            {
                Days = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(Globals.GetErrorMessage(ex));
            }
        }

        Date MyDate = new Date();
        public CarouselItem(int Year, int Month)
        {
            this.Year = Year;
            this.Month = Month;
        }

        private int _month;
        public int Month
        {
            get => _month;
            set => SetProperty(ref _month, value);
        }

        private int _year;
        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }
        public string Header
        {
            get
            {
                return ((ShamsiMonthType)(Month - 1)).ToString() + " " + Year.ToString();
            }
        }

        private List<CalendarDayBoxView> _days = new List<CalendarDayBoxView>();
        public List<CalendarDayBoxView> Days
        {
            get => _days;
            set => SetProperty(ref _days, value);
        }

        private bool DaysCreated = false;

        public void CreateDaysIfNeeded()
        {
            if (!DaysCreated)
            {
                Days.Clear();
                DayOfWeek StartDayOfWeek = MyDate.GetDayOfWeekForShamsiCalendar(Year, Month, 1);
                int Column = MyDate.GetDayOfWeek(StartDayOfWeek);
                int Row = 0;
                int TotalDays = MyDate.GetDaysCountOfMonth(Year, Month);
                var tempDays = new List<CalendarDayBoxView>();

                for (uint i = 1; i <= TotalDays; i++)
                {
                    var TargetViewModel = new CalendarDayBoxViewModel()
                    {
                        Day = i,
                        Month = (uint)Month,
                        CalendarTextColor = CalendarTextColor,
                        CalendarSelectedTextColor = CalendarSelectedTextColor,
                        CalendarHighlightColor = CalendarHighlightColor,
                    };

                    var dayBox = new CalendarDayBoxView(TargetViewModel)
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    };
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

                if (Month == ShamsiSelectedDate.Month)
                {
                    temp = Days.FirstOrDefault(d => d.DataContext.Day == ShamsiSelectedDate.Day &&
                        d.DataContext.Month == ShamsiSelectedDate.Month);

                    if (ShamsiSelectedDate.Day == 30 && ShamsiSelectedDate.Month == 12)
                    {
                        temp = Days.FirstOrDefault(d => d.DataContext.Day == 29);
                    }
                }

                if (temp != null)
                {
                    temp.DataContext.Select();
                }

                DaysCreated = true;

                tempDays = null;
                temp = null;
            }
        }
    }
}
