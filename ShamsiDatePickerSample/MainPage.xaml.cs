using ShamsiDatePicker;

namespace ShamsiDatePickerSample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private DateTime _targetDate = DateTime.Now;
    public DateTime TargetDate
    {
        get => _targetDate;
        set
        {
            if (_targetDate != value)
            {
                _targetDate = value;
                OnPropertyChanged(nameof(TargetDate));
            }
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ShamsiDatePicker.ShowCalendar();
    }
}
