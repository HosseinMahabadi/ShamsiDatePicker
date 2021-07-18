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
    internal class CalendarPageViewModel : ShareBaseViewModel
    {
        DateType Today = new DateType(DateTime.Now);
        Date MyDate = new Date();

        #region Functuin
        public CalendarPageViewModel() 
        {
            try
            {
                MessagingCenter.Subscribe<CalendarDayBoxViewModel, CalendarDayBoxViewModel>(this,
                Globals.Messages[MessageType.NewDayIsSelected],
                (sender, arg) =>
                {
                    SelectedDayBox = arg;
                    SelectItemCallBak(arg.Month, arg.Day);
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine("CalendarPageViewModel Error!!! " + ex.Message);
            }
        }

        public void Initialize()
        {
            try
            {
                Today.Calendar = CalendarType.Shamsi;

                var date = new DateType((DateTime)SelectedDate);
                date.Calendar = CalendarType.Shamsi;
                ShamsiSelectedDate = date;

                if (ShamsiSelectedDate.Year > MaxYear)
                {
                    Year = MaxYear;
                }
                else
                {
                    Year = ShamsiSelectedDate.Year;
                }

                List<YearListViewModel> Temp = new List<YearListViewModel>();
                for (int i = MinYear; i <= MaxYear; i++)
                {
                    Temp.Add(new YearListViewModel() { YearNumber = i, ForeColor = CalendarTextColor });
                }
                YearList = Temp;

                CreateAllDays();
                CreateCarouselItems();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Initialize on CalendarPageViewModel Error!!! " + ex.Message);
            }
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

        private void CreateCarouselItems()
        {
            try
            {
                var temp = new List<CarouselItem>()
                    {
                        new CarouselItem(Year , 1)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 1))
                        },
                        new CarouselItem(Year , 2)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 2))
                        },
                        new CarouselItem(Year , 3)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 3))
                        },
                        new CarouselItem(Year , 4)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 4))
                        },
                        new CarouselItem(Year , 5)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 5))
                        },
                        new CarouselItem(Year , 6)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 6))
                        },
                        new CarouselItem(Year , 7)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 7))
                        },
                        new CarouselItem(Year , 8)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 8))
                        },
                        new CarouselItem(Year , 9)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 9))
                        },
                        new CarouselItem(Year , 10)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 10))
                        },
                        new CarouselItem(Year , 11)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 11))
                        },
                        new CarouselItem(Year , 12)
                        {
                            Days = new List<CalendarDayBoxView>(
                                AllDays.Where(d => d.DataContext.Month == 12))
                        },
                    };

                CarouselItems = temp;
                Position = (int)SelectedDayBox.Month - 1;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                        var dayBox = new CalendarDayBoxView(new CalendarDayBoxViewModel()
                        {
                            Day = i,
                            Month = m,
                            CalendarTextColor = CalendarTextColor,
                            CalendarSelectedTextColor = CalendarSelectedTextColor,
                            CalendarHighlightColor = CalendarHighlightColor,
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

                var temp = AllDays.FirstOrDefault(d => d.DataContext.Day == ShamsiSelectedDate.Day &&
                            d.DataContext.Month == ShamsiSelectedDate.Month);

                if (temp == null)
                {
                    temp = AllDays.FirstOrDefault(d => d.DataContext.Day == 29 &&
                        d.DataContext.Month == ShamsiSelectedDate.Month);
                }

                temp.DataContext.Select();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Property

        public Action<DateTime?> DateCallBack { get; set; } = null;

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
            get => _allDays;
            set => SetProperty(ref _allDays, value);
        }
        //public Action<int, int, int> CallBack { get; set; } = null;

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
                            temp = temp = _yearList.FirstOrDefault(y => y.YearNumber == MaxYear);
                        }
                        SelectedYear = temp;
                    }
                }
            }
        }

        private List<CarouselItem> _carouselItems = null;

        public List<CarouselItem> CarouselItems
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
                        if(DateCallBack != null)
                        {
                            DateCallBack(null);
                        }
                        //MessagingCenter.Send(this, Globals.Messages[MessageType.CancelButtonClicked]);
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
                        var dt = new DateType(Year, (int)SelectedDayBox.Month, (int)SelectedDayBox.Day);
                        dt.Calendar = CalendarType.Miladi;
                        var d = new DateTime(dt.Year, dt.Month, dt.Day);
                        if(DateCallBack != null)
                        {
                            DateCallBack(d);
                        }
                        //MessagingCenter.Send(this, 
                            //Globals.Messages[MessageType.OkButtonClicked], d);
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
                        await Task.Run(() =>
                         {
                             CreateAllDays();
                             CreateCarouselItems();
                         });
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

                        await Task.Run(() => Position++);

                        item.ScaleTo(2.5, 100);
                        await item.FadeTo(0.2, 100);
                        await item.FadeTo(0, 100);
                        await Task.Run(() => item.Scale = 1);
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

                        await Task.Run(() => Position--);

                        item.ScaleTo(2.5, 100);
                        await item.FadeTo(0.2, 100);
                        await item.FadeTo(0, 100);
                        await Task.Run(() => item.Scale = 1);
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
