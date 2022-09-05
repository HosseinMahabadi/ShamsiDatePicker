using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShamdiDatePickerSample
{
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
                    OnPropertyChanged("TargetDate");
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ShamsiDatePicker.ShowCalendar();
        }
    }
}
