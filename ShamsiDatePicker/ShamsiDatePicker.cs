using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ShamsiDatePicker.ViewModel;
using HMExtension.Xamarin.Component;
using ShamsiDatePicker.View;

namespace ShamsiDatePicker
{
    public class ShamsiDatePicker : Entry
    {
        public ShamsiDatePicker()
        { 
            var TextBinding = new Binding()
            {
                Source = this,
                Path = "ShamsiDateString",
                Mode = BindingMode.OneWay
            };
            SetBinding(TextProperty, TextBinding);
                        
            /*var Tap = new TapGestureRecognizer();
            Tap.Tapped += OpenCalendar;
            GestureRecognizers.Add(Tap);*/

            Focused += OpenCalendar;
        }

        private async void OpenCalendar(object sender, EventArgs e)
        {
            try
            {
                var rootPage = new CalendarPage(new CalendarPageViewModel()
                {
                    CallBack = CloseCalendar
                });

                await Navigation.PushModalAsync(rootPage, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private void CloseCalendar(int year, int month, int day)
        {
            Navigation.PopModalAsync(false);
            if (year != -1)
            {
                var temp = new DateType(year, month, day);
                temp.Calendar = CalendarType.Miladi;

                Date = new DateTime(temp.Year, temp.Month, temp.Day);
                CalendarData.SelectedDate = (DateTime)Date;
            }

            Unfocus();
        }

        public DateTime? Date
        {
            get
            {
                return (DateTime)GetValue(DateProperty);
            }
            set
            {
                SetValue(DateProperty, value);
            }
        }

        public static readonly BindableProperty DateProperty = BindableProperty.Create("Date",
            typeof(DateTime?),
            typeof(ShamsiDatePicker),
            defaultValue: DateTime.Now,
            propertyChanged: OnDateChanged
           );

        private static void OnDateChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnDateChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnDateChanged(object oldValue, object newValue)
        {
            CalendarData.SelectedDate = (DateTime)newValue;

            OnPropertyChanged("ShamsiDate");
            OnPropertyChanged("ShamsiDateString");
        }

        public DateType ShamsiDate
        {
            get
            {
                try
                {
                    if (Date != null)
                    {
                        var temp = new DateType((DateTime)Date);
                        temp.Calendar = CalendarType.Shamsi;

                        return temp;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public string ShamsiDateString
        {
            get
            {
                if (Date != null)
                {
                    var temp = new DateType((DateTime)Date);
                    temp.Calendar = CalendarType.Shamsi;

                    return temp.GetDateString('/');
                }
                else
                {
                    return null;
                }
            }
        }

        public int MinimumShamsiYear
        {
            get
            {
                return (int)GetValue(MinimumShamsiYearProperty);
            }
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
            "MinimumShamsiYear",
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
                CalendarData.MinYear = temp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int MaximumShamsiYear
        {
            get
            {
                return (int)GetValue(MaximumShamsiYearProperty);
            }
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
            "MaximumShamsiYear",
            typeof(int),
            typeof(ShamsiDatePicker),
            propertyChanged: OnMaximumDateChanged,
            defaultValue: 1500);

        private static void OnMaximumDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var owner = bindable as ShamsiDatePicker;
            owner.OnMaximumDateChanged(oldValue, newValue);
        }

        private void OnMaximumDateChanged(object oldValue, object newValue)
        {
            CalendarData.MaxYear = Convert.ToInt32(newValue);
        }
    }
}
