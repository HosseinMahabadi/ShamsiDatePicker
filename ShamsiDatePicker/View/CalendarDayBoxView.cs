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
            this.BindingContext = BindingContext;
            DataContext = BindingContext;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            var ThisTaped = new TapGestureRecognizer();
            ThisTaped.SetBinding(TapGestureRecognizer.CommandProperty,
                new Binding() { Source = DataContext, Path = "TapCommand" });
            GestureRecognizers.Add(ThisTaped);

            var ShamsiDayLabel = CreateShamsiDayLabel();
            var BackgroundCircleShape = CreateBackgroundCircleShape();
            BackgroundCircleShape.Scale = 1.2;
            var MainGrid = new Grid();

            BackgroundCircleShape.Content = ShamsiDayLabel;
            MainGrid.Children.Add(BackgroundCircleShape);

            Content = MainGrid;
        }

        ShapeView CreateBackgroundCircleShape()
        {
            ShapeView BackgroundCircleShape = new ShapeView()
            {
                ShapeType = ShapeType.Circle,
                BorderWidth = 0,
                BorderColor = Color.Transparent,
            };

            DataTrigger BackgroundCircleShapeSelectedTrigger = new DataTrigger(typeof(ShapeView))
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
                Property = ShapeView.ColorProperty,
                Value = Color.FromHex("#FF4081")
            });

            BackgroundCircleShape.Triggers.Add(BackgroundCircleShapeSelectedTrigger);

            return BackgroundCircleShape;
        }

        Label CreateShamsiDayLabel()
        {
            var ShamsiDayLabel = new Label()
            {
                TextColor = Color.Black,
                BackgroundColor = Color.Transparent,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 15.2,
                //FontFamily = (OnPlatform<string>)MyStyle.Resources["B_Nazanin"],
                FontFamily = "B_Nazanin"
            };

            ShamsiDayLabel.SetBinding(Label.TextProperty, 
                new Binding { Source = DataContext, Path = "Day", Mode = BindingMode.OneWay });

            DataTrigger LabelIsEnabledTrigger = new DataTrigger(typeof(Label))
            {
                Binding = new Binding() { Source = this, Path = "IsEnabled" },
                Value = false,
            };
            LabelIsEnabledTrigger.Setters.Add(new Setter()
            {
                Property = Label.TextColorProperty,
                Value = Color.DarkGray,
            });

            DataTrigger LabelIsSelectedTrigger = new DataTrigger(typeof(Label))
            {
                Binding = new Binding() { Source = DataContext, Path = "IsSelected" },
                Value = true,
            };
            LabelIsSelectedTrigger.Setters.Add(new Setter()
            {
                Property = Label.TextColorProperty,
                Value = Color.White,
            });

            ShamsiDayLabel.Triggers.Add(LabelIsEnabledTrigger);
            ShamsiDayLabel.Triggers.Add(LabelIsSelectedTrigger);

            return ShamsiDayLabel;
        }
    }
}
