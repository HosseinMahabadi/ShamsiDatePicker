using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ShamsiDatePicker.ViewModel;
using HMExtension.Xamarin.Component;
using Rg.Plugins.Popup.Services;
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
            
            //IsReadOnly = true;
            
            /*var Tap = new TapGestureRecognizer();
            Tap.Tapped += TapGestureRecognizer_Tapped;
            GestureRecognizers.Add(Tap);*/

            Focused += ShamsiDatePicker_Focused;
        }

        [Obsolete]
        private async void ShamsiDatePicker_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                IsReadOnly = true;
                var np = new CalendarPage(new CalendarPageViewModel()
                {
                    CallBack = CallBack
                });
                np.BackgroundColor = Color.Transparent;

                await PopupNavigation.PushAsync(np, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
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

        [Obsolete]
        private async void CallBack(int year, int month, int day)
        {
            IsReadOnly = false;

            await PopupNavigation.PopAsync(true);
            if (year != -1)
            {
                var temp = new DateType(year, month, day);
                temp.Calendar = CalendarType.Miladi;

                Date = new DateTime(temp.Year, temp.Month, temp.Day);
                CalendarData.SelectedDate = (DateTime)Date;
            }
        }

        [Obsolete]
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                Focus();

                var np = new CalendarPage(new CalendarPageViewModel()
                {
                    CallBack = CallBack
                });

                np.BackgroundColor = Color.Transparent;

                await PopupNavigation.PushAsync(np, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
