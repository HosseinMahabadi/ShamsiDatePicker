using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ShamsiDatePicker.ViewModel;
using XFShapeView;

namespace ShamsiDatePicker.View
{
    internal class CalendarDayBoxView : ContentView
    {
        public CalendarDayBoxViewModel DataContext { get; set; } = null;

        public CalendarDayBoxView(CalendarDayBoxViewModel BindingContext)
        {
            DataContext = BindingContext;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            var ThisTaped = new TapGestureRecognizer();
            ThisTaped.SetBinding(TapGestureRecognizer.CommandProperty,
                new Binding() 
                { 
                    Source = DataContext, 
                    Path = "TapCommand" 
                });
            GestureRecognizers.Add(ThisTaped);

            var MainGrid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    CreateBackgroundCircleShape(),
                    CreateShamsiDayLabel(),
                }
            };

            Content = MainGrid;
        }

        private Frame CreateBackgroundCircleShape()
        {
            Frame BackgroundCircleShape = new Frame()
            {
                HasShadow = false,
                BackgroundColor = Color.Transparent,
                CornerRadius = 15,
                Scale = 1.2,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            DataTrigger BackgroundCircleShapeSelectedTrigger = new DataTrigger(typeof(Frame))
            {
                Value = true,
                Binding = new Binding()
                {
                    Source = DataContext,
                    Path = "IsSelected"
                },
            };

            BackgroundCircleShapeSelectedTrigger.Setters.Add(new Setter()
            {
                Property = Frame.BackgroundColorProperty,
                Value = new Binding() 
                { 
                    Source = DataContext, 
                    Path = "CalendarHighlightColor" 
                },
            });

            BackgroundCircleShape.Triggers.Add(BackgroundCircleShapeSelectedTrigger); 

            return BackgroundCircleShape;
        }

        private Label CreateShamsiDayLabel()
        {
            var ShamsiDayLabel = new Label()
            {
                BackgroundColor = Color.Transparent,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 18.5,
                //FontFamily = (OnPlatform<string>)MyStyle.Resources["B_Nazanin"],
                FontFamily = "B_Nazanin"
            };

            ShamsiDayLabel.SetBinding(Label.TextColorProperty, new Binding()
            {
                Source = DataContext,
                Path = "CalendarTextColor",
            });

            ShamsiDayLabel.SetBinding(Label.TextProperty, new Binding() 
            { 
                Source = DataContext, 
                Path = "Day", 
                Mode = BindingMode.OneWay,
            });

            DataTrigger LabelIsEnabledTrigger = new DataTrigger(typeof(Label))
            {
                Binding = new Binding() 
                { 
                    Source = this, 
                    Path = "IsEnabled" 
                },
                Value = false,
            };
            LabelIsEnabledTrigger.Setters.Add(new Setter()
            {
                Property = Label.TextColorProperty,
                Value = Color.DarkGray,
            });

            DataTrigger LabelIsSelectedTrigger = new DataTrigger(typeof(Label))
            {
                Binding = new Binding() 
                { 
                    Source = DataContext, 
                    Path = "IsSelected" 
                },
                Value = true,
            };
            var setter = new Setter() { Property = Label.TextColorProperty };

            LabelIsSelectedTrigger.Setters.Add(new Setter()
            {
                Property = Label.TextColorProperty,
                Value = new Binding() { Source = DataContext, Path = "CalendarSelectedTextColor" },
            });

            ShamsiDayLabel.Triggers.Add(LabelIsEnabledTrigger);
            ShamsiDayLabel.Triggers.Add(LabelIsSelectedTrigger);

            return ShamsiDayLabel;
        }
    }
}
