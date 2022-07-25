using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HMExtension.Xamarin.Mvvm;
using System.Windows.Input;
using ShamsiDatePicker.View;
using HMExtension.Xamarin.Component;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;

namespace ShamsiDatePicker.ViewModel
{
    internal class CalendarPageViewModel : ShareViewModelBase, IDisposable
    {
        DateType Today = new DateType(DateTime.Now);
        Date MyDate = new Date();

        #region Methods

        public CalendarPageViewModel() 
        {
            try
            {
                MessagingCenter.Subscribe<CalendarDayBoxViewModel, CalendarDayBoxViewModel>(this,
                Globals.Messages[MessageType.NewDayIsSelected],
                (sender, arg) =>
                {
                    if (arg != SelectedDayBox)
                    {
                        SelectedDayBox = arg;
                        SelectItemCallBak(arg.Month, arg.Day);
                        SetSelectedDate();
                    }
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine("CalendarPageViewModel Error!!! " + ex.Message);
            }
        }

        ~CalendarPageViewModel()
        {
            Dispose();
        }

        public void Dispose()
        {
            MessagingCenter.Unsubscribe<CalendarDayBoxViewModel, CalendarDayBoxViewModel>(this, Globals.Messages[MessageType.NewDayIsSelected]);
        }

        public async void Initialize()
        {
            try
            {
                Today.Calendar = CalendarType.Shamsi;

                if (ShamsiSelectedDate.Year > MaxYear)
                {
                    Year = MaxYear;
                }
                else if(ShamsiSelectedDate.Year < MinYear)
                {
                    Year = MinYear;
                }
                else
                {
                    Year = ShamsiSelectedDate.Year;
                }

                await Task.Run(() =>
                {
                    List<YearListViewModel> Temp = new List<YearListViewModel>();
                    for (int i = MinYear; i <= MaxYear; i++)
                    {
                        Temp.Add(new YearListViewModel() { YearNumber = i, ForeColor = CalendarTextColor });
                    }
                    YearList = Temp;
                    Temp = null;
                });

                CreateAllDays();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Initialize on CalendarPageViewModel Error!!! " + ex.Message);
            }
        }

        private void SelectItemCallBak(uint month, uint day)
        {
            var temp = MyDate.GetDayOfWeekForShamsiCalendar(Year, (int)month, (int)day);
            //Debug.WriteLine(temp.ToString());
            string dow = null;
            switch(temp)
            {
                case DayOfWeek.Saturday:
                    {
                        dow = "شنبه";
                        break;
                    }
                case DayOfWeek.Sunday:
                    {
                        dow = "یک شنبه";
                        break;
                    }
                case DayOfWeek.Monday:
                    {
                        dow = "دوشنبه";
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        dow = "سه شنبه";
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        dow = "چهارشنبه";
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        dow = "پنجشنبه";
                        break;
                    }
                case DayOfWeek.Friday:
                    {
                        dow = "جمعه";
                        break;
                    }
            }
            SelectedDay = string.Format("{0}, {1} {2}", dow, day.ToString(), (ShamsiMonthType)(month - 1));

            dow = null;
        }

        public void CreateAllDays()
        {
            try
            {
                CarouselItems.Clear();

                for (uint m = 1; m <= 12; m++)
                {
                    CarouselItems.Add(new CarouselItem(Year, (int)m)
                    {
                        ShamsiSelectedDate = ShamsiSelectedDate,
                        CalendarTextColor = CalendarTextColor,
                        CalendarSelectedTextColor = CalendarSelectedTextColor,
                        CalendarHighlightColor = CalendarHighlightColor,
                    });
                }

                SelectItemCallBak((uint)ShamsiSelectedDate.Month, (uint)ShamsiSelectedDate.Day);

                Position = ShamsiSelectedDate.Month - 1;

                CarouselItems[ShamsiSelectedDate.Month - 1].CreateDaysIfNeeded();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void SetSelectedDate()
        {
            try
            {
                var dt = new DateType(Year, (int)SelectedDayBox.Month, (int)SelectedDayBox.Day);
                dt.Calendar = CalendarType.Miladi;
                SelectedDate = new DateTime(dt.Year, dt.Month, dt.Day);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Property

        public Action<DateTime, bool> DateCallBack { get; set; } = null;

        private int _minYear = 0;
        public int MinYear
        {
            get => _minYear;
            set => SetProperty(ref _minYear, value);
        }

        private int _maxYear = 0;
        public int MaxYear
        {
            get => _maxYear;
            set => SetProperty(ref _maxYear, value);
        }

        private YearListViewModel _selectedYear = null;
        public YearListViewModel SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (value != _selectedYear)
                {
                    if (_selectedYear != null)
                    {
                        _selectedYear.ForeColor = CalendarTextColor;
                        _selectedYear.FontSize = 22;
                    }

                    _selectedYear = value;
                    OnPropertyChanged();

                    if (value != null)
                    {
                        _selectedYear.ForeColor = CalendarHighlightColor;
                        _selectedYear.FontSize = 38;
                    }
                }
            }
        }

        private int _position = 0;
        public int Position
        {
            get => _position;
            set
            {
                if(value > -1 && value < 12)
                {
                    SetProperty(ref _position, value);
                }
            }
        }

        //تاریخ انتخاب شده از تقویم
        private DateTime _selectedItem = DateTime.Now;
        public DateTime SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        //تاریخ جاری تقویم
        private int _year = 0;
        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }

        private string _selectedDay = null;
        public string SelectedDay
        {
            get => _selectedDay;
            set => SetProperty(ref _selectedDay, value);
        }

        //دیدپذیری لیست سال ها
        private bool _yearListVisibility = false;
        public bool YearListVisibility
        {
            get => _yearListVisibility;
            set => SetProperty(ref _yearListVisibility, value);
        }

        //لیست سال های قابل انتخاب از تقویم
        private List<YearListViewModel> _yearList = new List<YearListViewModel> ();
        public List<YearListViewModel> YearList
        {
            get => _yearList;
            set
            {
                if (_yearList != value)
                {
                    _yearList = value;
                    OnPropertyChanged();

                    if (value != null)
                    {
                        var temp = _yearList.FirstOrDefault(y => y.YearNumber == ShamsiSelectedDate.Year);
                        if(temp == null)
                        {
                            temp = _yearList.FirstOrDefault(y => y.YearNumber == MaxYear);
                        }
                        SelectedYear = temp;
                    }
                }
            }
        }

        private ObservableCollection<CarouselItem> _carouselItems = new ObservableCollection<CarouselItem>();

        public ObservableCollection<CarouselItem> CarouselItems
        {
            get => _carouselItems;
            set => SetProperty(ref _carouselItems, value);
        }

        #endregion

        #region Command

        //کلید انصراف
        private ICommand _cancelCommand = null;
        public ICommand CancelCommand
        {
            get
            {
                if(_cancelCommand == null)
                {
                    _cancelCommand = new Command<CalendarPage>((sender) =>
                    {
                        DateCallBack?.Invoke(SelectedDate, true);
                    });
                }

                return _cancelCommand;
            }
        }

        //کلید تایید
        private ICommand _okCommand = null;
        public ICommand OkCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new Command<CalendarPage>((sender) =>
                    {
                        DateCallBack?.Invoke(SelectedDate, false);
                    });
                }

                return _okCommand;
            }
        }

        private ICommand _yearTappedCommand = null;
        public ICommand YearTappedCommand
        {
            get
            {
                if(_yearTappedCommand == null)
                {
                    _yearTappedCommand = new Command<List<object>>(async (sender) =>
                    {
                        var label = sender[0] as Label;
                        var listView = sender[1] as ListView;

                        await label.FadeTo(0.4, 150);
                        await label.FadeTo(1, 100);

                        if (!YearListVisibility)
                        {
                            if (SelectedYear != null)
                            {
                                listView.ScrollTo(SelectedYear, ScrollToPosition.Center, false);
                            }
                        }

                        YearListVisibility = !YearListVisibility;
                    });
                }

                return _yearTappedCommand;
            }
        }

        private ICommand _yearSelectedCommand = null;      
        public ICommand YearSelectedCommand
        {
            get
            {
                if(_yearSelectedCommand == null)
                {
                    _yearSelectedCommand = new Command<YearListViewModel>(async (year) =>
                    {
                        Year = year.YearNumber;
                        //await Task.Run(() =>
                         //{
                             CreateAllDays();
                         //});
                    });
                }

                return _yearSelectedCommand;
            }
        }

        private ICommand _yearListTapped = null;
        public ICommand YearListTapped
        {
            get
            {
                if(_yearListTapped == null)
                {
                    _yearListTapped = new Command(() =>
                     {
                         YearListVisibility = false;
                     });
                }

                return _yearListTapped;
            }
        }

        private ICommand _forwardCommand = null;
        public ICommand ForwardCommand
        {
            get
            {
                if (_forwardCommand == null)
                {
                    _forwardCommand = new Command<object>(async (sender) => 
                    {
                        var item = sender as XFShapeView.ShapeView;

                        _ = await Task.Run(() => Position++);

                        _ = item.ScaleTo(2.5, 100);
                        _ = await item.FadeTo(0.2, 100);
                        _ = await item.FadeTo(0, 100);
                        _ = await Task.Run(() => item.Scale = 1);
                    });
                }

                return _forwardCommand;
            }
        }

        private ICommand _backwardCommand = null;
        public ICommand BackwardCommand
        {
            get
            {
                if (_backwardCommand == null)
                {
                    _backwardCommand = new Command<object>(async (sender) =>
                    {
                        var item = sender as XFShapeView.ShapeView;

                        _ = await Task.Run(() => Position--);

                        _ = item.ScaleTo(2.5, 100);
                        _ = await item.FadeTo(0.2, 100);
                        _ = await item.FadeTo(0, 100);
                        _ = await Task.Run(() => item.Scale = 1);
                    });
                }

                return _backwardCommand;
            }
        }

        private ICommand _goToSelectedDay = null;

        public ICommand GoToSelectedDay
        {
            get
            {
                if(_goToSelectedDay == null)
                {
                    _goToSelectedDay = new Command(() =>
                    {
                        if(SelectedDayBox != null)
                        {
                            Position = (int)SelectedDayBox.Month - 1;
                        }
                    });
                }

                return _goToSelectedDay;
            }
        }

        #endregion
    }
}
