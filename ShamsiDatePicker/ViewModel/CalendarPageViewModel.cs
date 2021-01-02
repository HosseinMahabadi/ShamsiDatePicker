using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HMExtension.Xamarin.Mvvm;
using System.Windows.Input;
using ShamsiDatePicker.View;
using HMExtension.Xamarin.Component;
using System.Threading.Tasks;

namespace ShamsiDatePicker.ViewModel
{
    internal class CalendarPageViewModel : ViewModelBase
    {
        DateType Today = new DateType(DateTime.Now);
        Date MyDate = new Date();

        #region Functuin
        public CalendarPageViewModel()
        {
            Today.Calendar = CalendarType.Shamsi;

            if (ShamsiSelectedDate.Year > CalendarData.MaxYear)
            {
                Year = CalendarData.MaxYear;
            }
            else
            {
                Year = ShamsiSelectedDate.Year;
            }

            List<YearListViewModel> Temp = new List<YearListViewModel>();
            for(int i = CalendarData.MinYear; i <= CalendarData.MaxYear; i++)
            {
                Temp.Add(new YearListViewModel() { YearNumber = i });
            }
            YearList = Temp;
        }

        private void SelectItemCallBak(uint month, uint day)
        {
            var temp = MyDate.GetDayOfWeekForShamsiCalendar(Year, (int)month, (int)day);

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
        }

        private async void CreateCarouselItems()
        {
            await Task.Run(() =>
            {
                try
                {
                    var temp = new List<CarouselItem>()
                    {
                        new CarouselItem(Year , 1) { Days = AllDays.FindAll(d => d.DataContext.Month == 1)},
                        new CarouselItem(Year , 2) { Days = AllDays.FindAll(d => d.DataContext.Month == 2)},
                        new CarouselItem(Year , 3) { Days = AllDays.FindAll(d => d.DataContext.Month == 3)},
                        new CarouselItem(Year , 4) { Days = AllDays.FindAll(d => d.DataContext.Month == 4)},
                        new CarouselItem(Year , 5) { Days = AllDays.FindAll(d => d.DataContext.Month == 5)},
                        new CarouselItem(Year , 6) { Days = AllDays.FindAll(d => d.DataContext.Month == 6)},
                        new CarouselItem(Year , 7) { Days = AllDays.FindAll(d => d.DataContext.Month == 7)},
                        new CarouselItem(Year , 8) { Days = AllDays.FindAll(d => d.DataContext.Month == 8)},
                        new CarouselItem(Year , 9) { Days = AllDays.FindAll(d => d.DataContext.Month == 9)},
                        new CarouselItem(Year , 10) { Days = AllDays.FindAll(d => d.DataContext.Month == 10)},
                        new CarouselItem(Year , 11) { Days = AllDays.FindAll(d => d.DataContext.Month == 11)},
                        new CarouselItem(Year , 12) { Days = AllDays.FindAll(d => d.DataContext.Month == 12)},
                    };

                    CarouselItems = temp;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }

        public void CreateAllDays()
        {
            try
            {
                var items = new List<CalendarDayBoxView>();

                for (uint m = 1; m <= 12; m++)
                {
                    DayOfWeek StartDayOfWeek = MyDate.GetDayOfWeekForShamsiCalendar(Year, (int)m, 1);
                    int Column = MyDate.GetDayOfWeek(StartDayOfWeek);
                    int Row = 0;
                    int TotalDays = MyDate.GetDaysCountOfMonth(Year, (int)m);

                    for (uint i = 1; i <= TotalDays; i++)
                    {
                        var dayBox = new CalendarDayBoxView(new CalendarDayBoxViewModel(SelectItemCallBak)
                        {
                            Day = i,
                            Month = m,
                        })
                        {
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        };
                        dayBox.SetValue(Grid.ColumnProperty, Column + 1);
                        dayBox.SetValue(Grid.RowProperty, Row);

                        items.Add(dayBox);

                        Column++;
                        if (Column > 6)
                        {
                            Column = 0;
                            Row++;
                        }
                    }
                }

                AllDays = items;

                if (CalendarData.SelectedDay == null)
                {
                    var temp = AllDays.Find(d => d.DataContext.Day == ShamsiSelectedDate.Day &&
                                d.DataContext.Month == ShamsiSelectedDate.Month);

                    if(temp == null)
                    {
                        temp = AllDays.Find(d => d.DataContext.Day == 29 &&
                            d.DataContext.Month == ShamsiSelectedDate.Month);
                    }

                    CalendarData.SelectedDay = null;
                    temp.DataContext.IsSelected = true;
                }
                else
                {
                    var temp = AllDays.Find(d => d.DataContext.Day == CalendarData.SelectedDay.Day &&
                                d.DataContext.Month == CalendarData.SelectedDay.Month);

                    if (temp == null)
                    {
                        temp = AllDays.Find(d => d.DataContext.Day == 29 &&
                            d.DataContext.Month == CalendarData.SelectedDay.Month);
                    }

                    CalendarData.SelectedDay = null;
                    temp.DataContext.IsSelected = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Property

        private YearListViewModel _selectedYear = null;
        public YearListViewModel SelectedYear
        {
            get
            {
                return _selectedYear;
            }
            set
            {
                if (value != _selectedYear)
                {
                    if (_selectedYear != null)
                    {
                        _selectedYear.ForeColor = Color.Black;
                        _selectedYear.FontSize = 22;
                    }

                    _selectedYear = value;
                    OnPropertyChanged();

                    if (value != null)
                    {
                        _selectedYear.ForeColor = Color.FromHex("#FF4081");
                        _selectedYear.FontSize = 38;
                    }
                }
            }
        }
        public DateType ShamsiSelectedDate
        {
            get
            {
                var date = new DateType(CalendarData.SelectedDate);
                date.Calendar = CalendarType.Shamsi;

                return date;
            }
        }

        private int _position = -1;
        public int Position
        {
            get
            {
                if(_position == -1)
                {
                    var date = new DateType(CalendarData.SelectedDate);
                    date.Calendar = CalendarType.Shamsi;

                    _position = date.Month - 1;
                }
                return _position;
            }
            set
            {
                if(_position != value && value > -1 && value < 12)
                {
                    _position = value;
                    OnPropertyChanged();
                }
            }
        }
        List<CalendarDayBoxView> _allDays = new List<CalendarDayBoxView>();
        public List<CalendarDayBoxView> AllDays
        {
            get
            {
                return _allDays;
            }
            set
            {
                if (_allDays != value)
                {
                    _allDays = value;
                    OnPropertyChanged();
                }
            }
        }
        public Action<int, int, int> CallBack { get; set; } = null;

        //تاریخ انتخاب شده از تقویم
        private DateTime _selectedItem = DateTime.Now;
        public DateTime SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if(_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        //تاریخ جاری تقویم
        private int _year = 0;
        public int Year
        {
            get
            {
                if(_year == 0)
                {
                    _year = ShamsiSelectedDate.Year;
                }
                return _year;
            }
            set
            {
                if(_year != value)
                {
                    _year = value;
                    OnPropertyChanged();

                    CreateAllDays();
                    CreateCarouselItems();
                }
            }
        }

        private string _selectedDay = null;
        public string SelectedDay
        {
            get
            {
                return _selectedDay;
            }
            set
            {
                if(_selectedDay!=value)
                {
                    _selectedDay = value;
                    OnPropertyChanged();
                }
            }
        }

        //دیدپذیری لیست سال ها
        private bool _yearListVisibility = false;
        public bool YearListVisibility
        {
            get
            {
                return _yearListVisibility;
            }
            set
            {
                if(_yearListVisibility != value)
                {
                    _yearListVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        //لیست سال های قابل انتخاب از تقویم
        private List<YearListViewModel> _yearList = new List<YearListViewModel> ();
        public List<YearListViewModel> YearList
        {
            get
            {
                return _yearList;
            }
            set
            {
                if (_yearList != value)
                {
                    _yearList = value;
                    OnPropertyChanged();

                    if (value != null)
                    {
                        var temp = _yearList.Find(y => y.YearNumber == ShamsiSelectedDate.Year);
                        if(temp == null)
                        {
                            temp = temp = _yearList.Find(y => y.YearNumber == CalendarData.MaxYear);
                        }
                        SelectedYear = temp;
                    }
                }
            }
        }

        private List<CarouselItem> _carouselItems = null;

        public List<CarouselItem> CarouselItems
        {
            get
            {
                return _carouselItems;
            }
            set
            {
                if(_carouselItems != value)
                {
                    _carouselItems = value;
                    OnPropertyChanged();
                }
            }
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
                    _cancelCommand = new Command(() =>
                    {
                        CallBack(-1, -1, -1);
                    });
                }

                return _cancelCommand;
            }
            set
            {
                _cancelCommand = value;
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
                    _okCommand = new Command(() =>
                    {
                        CallBack(Year, (int)CalendarData.SelectedDay.Month, (int)CalendarData.SelectedDay.Day);
                    });
                }

                return _okCommand;
            }
            set
            {
                _okCommand = value;
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
            set
            {
                _yearTappedCommand = value;
            }
        }

        private ICommand _yearSelectedCommand = null;      
        public ICommand YearSelectedCommand
        {
            get
            {
                if(_yearSelectedCommand == null)
                {
                    _yearSelectedCommand = new Command<YearListViewModel>((year) =>
                    {
                        Year = year.YearNumber;
                    });
                }

                return _yearSelectedCommand;
            }
            set
            {
                _yearSelectedCommand = value;
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

                        await Task.Run(() => Position++);

                        item.ScaleTo(2, 100);
                        await item.FadeTo(0.8, 100);
                        await item.FadeTo(0, 100);
                        await Task.Run(() => item.Scale = 1);
                    });
                }

                return _forwardCommand;
            }
            set
            {
                _forwardCommand = value;
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

                        await Task.Run(() => Position--);

                        item.ScaleTo(2, 100);
                        await item.FadeTo(0.8, 100);
                        await item.FadeTo(0, 100);
                        await Task.Run(() => item.Scale = 1);
                    });
                }

                return _backwardCommand;
            }
            set
            {
                _backwardCommand = value;
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
                        if(CalendarData.SelectedDay != null)
                        {
                            Position = (int)CalendarData.SelectedDay.Month - 1;
                        }
                    });
                }

                return _goToSelectedDay;
            }
            set
            {
                _goToSelectedDay = value;
            }
        }

        #endregion
    }
}
