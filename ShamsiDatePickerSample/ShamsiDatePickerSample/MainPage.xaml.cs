using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShamsiDatePickerSample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        private DateTime _targetDate = DateTime.Now;
        DateTime TargetDate 
        {
            get
            {
                return _targetDate;
            }
            set
            {
                if(_targetDate != value)
                {
                    _targetDate = value;
                    OnPropertyChanged();
                }
            } 
        }
    }
}
