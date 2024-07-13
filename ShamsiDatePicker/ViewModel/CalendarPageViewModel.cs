using System;
using System.Collections.Generic;
using System.Text;
using HMExtension.Maui;
using System.Windows.Input;
using ShamsiDatePicker.View;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace ShamsiDatePicker.ViewModel;

internal class CalendarPageViewModel : ViewModelBase
{
    public CalendarPageViewModel() 
    {
        try
        {
            MessagingCenter.Subscribe<CalendarDayBoxView, CalendarDayBoxView>(this,
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

    #region Methods

    public void Initialize()
    {
        try
        {
            if (Globals.ShamsiSelectedDate.Year > MaxYear)
            {
                Year = MaxYear;
            }
            else if (Globals.ShamsiSelectedDate.Year < MinYear)
            {
                Year = MinYear;
            }
            else
            {
                Year = Globals.ShamsiSelectedDate.Year;
            }

            List<YearListViewModel> Temp = [];
            for (int i = MinYear; i <= MaxYear; i++)
            {
                Temp.Add(new YearListViewModel()
                {
                    YearNumber = i,
                    ForeColor = Globals.CalendarTextColor
                });
            }
            YearList = Temp;
            Temp = null;

            CreateCarouselItem();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Initialize on CalendarPageViewModel Error!!! " + ex.Message);
        }
    }

    private void SelectItemCallBak(int month, int day)
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

    public void CreateCarouselItem()
    {
        try
        {
            foreach(CarouselItem item in CarouselItems)
            {
                item.SetYear(Year);
            }
            SelectItemCallBak(Globals.ShamsiSelectedDate.Month, Globals.ShamsiSelectedDate.Day);

            int newPosition = Globals.ShamsiSelectedDate.Month - 1;
            if(Position == newPosition)
            {
                CarouselItems[Position].CreateDaysIfNeeded();
            }
            else
            {
                Position = newPosition;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    public void SetSelectedDate()
    {
        try
        {
            DateType dt = new(Year, SelectedDayBox.Month, SelectedDayBox.Day)
            {
                Calendar = CalendarType.Miladi
            };
            Globals.SelectedDate = new DateTime(dt.Year, dt.Month, dt.Day);
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    #endregion

    #region Property

    Date MyDate { get; set; } = new Date();

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
                    _selectedYear.ForeColor = Globals.CalendarTextColor;
                    _selectedYear.FontSize = 22;
                }

                _selectedYear = value;
                OnPropertyChanged();

                if (value != null)
                {
                    _selectedYear.ForeColor = Globals.CalendarHighlightColor;
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
        set => SetProperty(ref _year, value, nameof(Year), () => Globals.ActiveYear = value);
    }

    private string _selectedDay = null;
    public string SelectedDay
    {
        get => _selectedDay;
        set => SetProperty(ref _selectedDay, value);
    }

    private CalendarDayBoxView _selectedDayBox = null;
    public CalendarDayBoxView SelectedDayBox
    {
        get => _selectedDayBox;
        set
        {
            if (value != _selectedDayBox && _selectedDayBox != null)
            {
                _selectedDayBox.Unselect();
            }
            SetProperty(ref _selectedDayBox, value);
        }
    }

    //دیدپذیری لیست سال ها
    private bool _yearListVisibility = false;
    public bool YearListVisibility
    {
        get => _yearListVisibility;
        set => SetProperty(ref _yearListVisibility, value);
    }

    //لیست سال های قابل انتخاب از تقویم
    private List<YearListViewModel> _yearList = [];
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
                    var temp = _yearList.FirstOrDefault(y => y.YearNumber == Globals.ShamsiSelectedDate.Year);
                    temp ??= _yearList.FirstOrDefault(y => y.YearNumber == MaxYear);
                    SelectedYear = temp;
                }
            }
        }
    }

    private List<CarouselItem> _carouselItems =
        [
            new(1),
            new(2),
            new(3),
            new(4),
            new(5),
            new(6),
            new(7),
            new(8),
            new(9),
            new(10),
            new(11),
            new(12),
        ];

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
                    DateCallBack?.Invoke(Globals.SelectedDate, true);
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
                    DateCallBack?.Invoke(Globals.SelectedDate, false);
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
            _yearTappedCommand ??= new Command<List<object>>(async (sender) =>
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

            return _yearTappedCommand;
        }
    }

    private ICommand _yearSelectedCommand = null;      
    public ICommand YearSelectedCommand
    {
        get
        {
            _yearSelectedCommand ??= new Command<YearListViewModel>(async (year) =>
                {
                    Year = year.YearNumber;
                    await Task.Run(() =>
                    {
                         CreateCarouselItem();
                    });
                });

            return _yearSelectedCommand;
        }
    }

    private ICommand _yearListTapped = null;
    public ICommand YearListTapped
    {
        get
        {
            _yearListTapped ??= new Command(() =>
                 {
                     YearListVisibility = false;
                 });

            return _yearListTapped;
        }
    }

    private ICommand _forwardCommand = null;
    public ICommand ForwardCommand
    {
        get
        {
            _forwardCommand ??= new Command<object>(async (sender) => 
                {
                    var item = sender as Frame;

                    await Task.Run(() => Position--);

                    _ = item.ScaleTo(2, 150);
                    _ = await item.FadeTo(0.3, 150);
                    _ = await item.FadeTo(0, 150);
                    await item.ScaleTo(1, 1);
                });

            return _forwardCommand;
        }
    }

    private ICommand _backwardCommand = null;
    public ICommand BackwardCommand
    {
        get
        {
            _backwardCommand ??= new Command<object>(async(sender) =>
                {
                    try
                    {
                        var item = sender as Frame;

                        await Task.Run(() => Position++);

                        _ = item.ScaleTo(2, 150);
                        await item.FadeTo(0.3, 150);
                        await item.FadeTo(0, 150);
                        await item.ScaleTo(1, 1);
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.GetErrorMessage());
                    }
                });

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
                        Position = SelectedDayBox.Month - 1;
                    }
                });
            }

            return _goToSelectedDay;
        }
    }

    #endregion
}
