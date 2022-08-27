using System;
using Xamarin.Forms;
using ShamsiDatePicker.ViewModel;
using HMExtension.Xamarin;
using ShamsiDatePicker.View;
using System.Threading.Tasks;
using System.Diagnostics;
using HMControls;

namespace ShamsiDatePicker
{
    public class ShamsiDatePicker : KeyboardlessEntry, IDisposable
    {
        public ShamsiDatePicker() : base()
        {
            try
            {
                var temp = new DateType(Date);
                temp.Calendar = CalendarType.Shamsi;
                ShamsiDateString = temp.GetDateString('/');
                Text = ShamsiDateString;
                SetBinding(TextProperty, new Binding
                {
                    Source = this,
                    Path = nameof(ShamsiDateString),
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetErrorMessage());
            }
        }

        ~ShamsiDatePicker()
        {
            Dispose();
        }

        public override void ActionOnFocused()
        {
            ShowCalendar();
        }

        public async void ShowCalendar()
        {
            try
            {
                var RootPageViewModel = new CalendarPageViewModel()
                {
                    SelectedDate = Date,
                    CalendarBackgroundColor = CalendarBackgroundColor,
                    CalendarCancelButtonBackgroundColor = CalendarCancelButtonBackgroundColor,
                    CalendarCancelButtonTextColor = CalendarCancelButtonTextColor,
                    CalendarOKButtonBackgroundColor = CalendarOKButtonBackgroundColor,
                    CalendarHighlightColor = CalendarHighlightColor,
                    CalendarOKButtonTextColor = CalendarOKButtonTextColor,
                    CalendarSelectedTextColor = CalendarSelectedTextColor,
                    CalendarSubTitleColor = CalendarSubTitleColor,
                    CalendarTextColor = CalendarTextColor,
                    CalendarTitleColor = CalendarTitleColor,
                    HeaderBackgroundColor = HeaderBackgroundColor,
                    HeaderSubTitleTextColor = HeaderSubTitleTextColor,
                    HeaderTitleTextColor = HeaderTitleTextColor,
                    MaxYear = MaximumShamsiYear,
                    MinYear = MinimumShamsiYear,
                    DateCallBack = CloseCalendar,
                };

                var RootPage = new CalendarPage();
                RootPage.DataContext = RootPageViewModel;
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
            "HeaderBackgroundColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.FromHex("#FF4081"),
            propertyChanged: OnHeaderBackgroundColorChanged);

        private static void OnHeaderBackgroundColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnHeaderBackgroundColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnHeaderBackgroundColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.HeaderBackgroundColorIsChanged], (Color)newValue);
        }

        public Color HeaderTitleTextColor
        {
            get
            {
                return (Color)GetValue(HeaderTitleTextColorProperty);
            }
            set
            {
                SetValue(HeaderTitleTextColorProperty, value);
            }
        }
        public static readonly BindableProperty HeaderTitleTextColorProperty = BindableProperty.Create("HeaderTitleTextColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.White,
            propertyChanged: OnHeaderTitleTextColorChanged);

        private static void OnHeaderTitleTextColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnHeaderTitleTextColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnHeaderTitleTextColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.HeaderTitleTextColorIsChanged], (Color)newValue);
        }

        public Color HeaderSubTitleTextColor
        {
            get
            {
                return (Color)GetValue(HeaderSubTitleTextColorProperty);
            }
            set
            {
                SetValue(HeaderSubTitleTextColorProperty, value);
            }
        }
        public static readonly BindableProperty HeaderSubTitleTextColorProperty = BindableProperty.Create("HeaderSubTitleTextColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.White,
            propertyChanged: OnHeaderSubTitleTextColorChanged);

        private static void OnHeaderSubTitleTextColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnHeaderSubTitleTextColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnHeaderSubTitleTextColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.HeaderSubTitleTextColorIsChanged], (Color)newValue);
        }

        public Color CalendarBackgroundColor
        {
            get
            {
                return (Color)GetValue(CalendarBackgroundColorProperty);
            }
            set
            {
                SetValue(CalendarBackgroundColorProperty, value);
            }
        }

        public static readonly BindableProperty CalendarBackgroundColorProperty = BindableProperty.Create("CalendarBackgroundColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.White,
            propertyChanged: OnCalendarBackgroundColorChanged);

        private static void OnCalendarBackgroundColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnCalendarBackgroundColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnCalendarBackgroundColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.CalendarBackgroundColorIsChanged], (Color)newValue);
        }

        public Color CalendarTextColor
        {
            get
            {
                return (Color)GetValue(CalendarTextColorProperty);
            }
            set
            {
                SetValue(CalendarTextColorProperty, value);
            }
        }

        public static readonly BindableProperty CalendarTextColorProperty = BindableProperty.Create("CalendarTextColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.Black,
            propertyChanged: OnCalendarTextColorChanged);

        private static void OnCalendarTextColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnCalendarTextColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnCalendarTextColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.CalendarTextColorIsChanged]
            //, (Color)newValue);
        }

        public Color CalendarSelectedTextColor
        {
            get
            {
                return (Color)GetValue(CalendarSelectedTextColorProperty);
            }
            set
            {
                SetValue(CalendarSelectedTextColorProperty, value);
            }
        }
        public static readonly BindableProperty CalendarSelectedTextColorProperty = BindableProperty.Create("CalendarSelectedTextColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.White,
            propertyChanged: OnCalendarSelectedTextColorChanged);

        private static void OnCalendarSelectedTextColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnCalendarSelectedTextColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnCalendarSelectedTextColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.CalendarSelectedTextColorIsChanged], (Color)newValue);
        }

        public Color CalendarHighlightColor
        {
            get
            {
                return (Color)GetValue(CalendarHighlightColorProperty);
            }
            set
            {
                SetValue(CalendarHighlightColorProperty, value);
            }
        }

        public static readonly BindableProperty CalendarHighlightColorProperty = BindableProperty.Create("CalendarHighlightColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.FromHex("#FF4081"),
            propertyChanged: OnCalendarHighlightColorChanged);

        private static void OnCalendarHighlightColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnCalendarHighlightColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnCalendarHighlightColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.CalendarHighlightColorIsChanged], (Color)newValue);
        }

        public Color CalendarTitleColor
        {
            get
            {
                return (Color)GetValue(CalendarTitleColorProperty);
            }
            set
            {
                SetValue(CalendarTitleColorProperty, value);
            }
        }

        public static readonly BindableProperty CalendarTitleColorProperty = BindableProperty.Create("CalendarTitleColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.Black,
            propertyChanged: OnCalendarTitleColorChanged);

        private static void OnCalendarTitleColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnCalendarTitleColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnCalendarTitleColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.CalendarTitleColorIsChanged], (Color)newValue);
        }


        public Color CalendarSubTitleColor
        {
            get
            {
                return (Color)GetValue(CalendarSubTitleColorProperty);
            }
            set
            {
                SetValue(CalendarSubTitleColorProperty, value);
            }
        }

        public static readonly BindableProperty CalendarSubTitleColorProperty = BindableProperty.Create("CalendarSubTitleColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.Black,
            propertyChanged: OnCalendarSubTitleColorChanged);

        private static void OnCalendarSubTitleColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnCalendarSubTitleColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnCalendarSubTitleColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.CalendarSubTitleColorIsChanged], (Color)newValue);
        }

        public Color CalendarOKButtonTextColor
        {
            get
            {
                return (Color)GetValue(CalendarOKButtonTextColorProperty);
            }
            set
            {
                SetValue(CalendarOKButtonTextColorProperty, value);
            }
        }

        public static readonly BindableProperty CalendarOKButtonTextColorProperty = BindableProperty.Create("CalendarOKButtonTextColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.FromHex("#FF4081"),
            propertyChanged: OnCalendarOKButtonTextColorChanged);

        private static void OnCalendarOKButtonTextColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnCalendarOKButtonTextColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnCalendarOKButtonTextColorChanged(object oldValue, object newValue)
        {
           //MessagingCenter.Send(this, Globals.Messages[MessageType.CalendarOKButtonTextColorIsChanged], (Color)newValue);
        }

        public Color CalendarOKButtonBackgroundColor
        {
            get
            {
                return (Color)GetValue(CalendarOKButtonBackgroundColorProperty);
            }
            set
            {
                SetValue(CalendarOKButtonBackgroundColorProperty, value);
            }
        }

        public static readonly BindableProperty CalendarOKButtonBackgroundColorProperty = BindableProperty.Create("CalendarOKButtonBackgroundColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.Transparent,
            propertyChanged: OnCalendarOKButtonBackgroundColorChanged);

        private static void OnCalendarOKButtonBackgroundColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnCalendarOKButtonBackgroundColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnCalendarOKButtonBackgroundColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.CalendarOKButtonBackgroundColorIsChanged], (Color)newValue);
        }
        //***

        public Color CalendarCancelButtonTextColor
        {
            get
            {
                return (Color)GetValue(CalendarCancelButtonTextColorProperty);
            }
            set
            {
                SetValue(CalendarCancelButtonTextColorProperty, value);
            }
        }

        public static readonly BindableProperty CalendarCancelButtonTextColorProperty = BindableProperty.Create("CalendarCancelButtonTextColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.FromHex("#FF4081"),
            propertyChanged: OnCalendarCancelButtonTextColorChanged);

        private static void OnCalendarCancelButtonTextColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnCalendarCancelButtonTextColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnCalendarCancelButtonTextColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.CalendarCancelButtonTextColorIsChanged], (Color)newValue);
        }

        public Color CalendarCancelButtonBackgroundColor
        {
            get
            {
                return (Color)GetValue(CalendarCancelButtonBackgroundColorProperty);
            }
            set
            {
                SetValue(CalendarCancelButtonBackgroundColorProperty, value);
            }
        }

        public static readonly BindableProperty CalendarCancelButtonBackgroundColorProperty = BindableProperty.Create("CalendarCancelButtonBackgroundColor",
            typeof(Color),
            typeof(ShamsiDatePicker),
            defaultValue: Color.Transparent,
            propertyChanged: OnCalendarCancelButtonBackgroundColorChanged);

        private static void OnCalendarCancelButtonBackgroundColorChanged(BindableObject sender, object oldValue, object newValue)
        {
            try
            {
                var Object = sender as ShamsiDatePicker;
                Object.OnCalendarCancelButtonBackgroundColorChanged(oldValue, newValue);
            }
            catch { }
        }

        private void OnCalendarCancelButtonBackgroundColorChanged(object oldValue, object newValue)
        {
            //MessagingCenter.Send(this, Globals.Messages[MessageType.CalendarCancelButtonBackgroundColorIsChanged], (Color)newValue);
        }
        //*****************************************************************************
        //**********************************************************
        //*************************************************
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date),
            typeof(DateTime),
            typeof(ShamsiDatePicker),
            defaultValue: DateTime.Now,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnDateChanged);

        private static void OnDateChanged(BindableObject sender, object oldValue, object newValue)
        {
            Debug.WriteLine("Date old value " + ((DateTime)oldValue).ToString());
            Debug.WriteLine("Date new value " + ((DateTime)newValue).ToString());

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
                    var temp = new DateType((DateTime)newValue);
                    temp.Calendar = CalendarType.Shamsi;

                    ShamsiDateString = temp.GetDateString('/');
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
                //MessagingCenter.Send(this, Globals.Messages[MessageType.MinYearIsChanged], temp);
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
            //MessagingCenter.Send(this, Globals.Messages[MessageType.MinYearIsChanged], Convert.ToInt32(newValue));
        }

        public void Dispose()
        {
            try
            {
                UnapplyBindings();
                //MasterParent = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetErrorMessage());
            }
        }

    }
}
