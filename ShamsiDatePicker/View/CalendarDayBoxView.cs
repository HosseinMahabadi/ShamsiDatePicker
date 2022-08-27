using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ShamsiDatePicker.ViewModel;
using XFShapeView;
using System.Diagnostics;
using HMExtension.Xamarin;

namespace ShamsiDatePicker.View
{
    internal class CalendarDayBoxView : ContentView, IDisposable
    {
        public CalendarDayBoxViewModel DataContext { get; set; } = null;

        public CalendarDayBoxView(CalendarDayBoxViewModel BindingContext)
        {
            try
            {
                DataContext = BindingContext;
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetErrorMessage());
            }
        }

        ~CalendarDayBoxView()
        {
            Dispose();
        }

        public void Dispose()
        {
            try
            {
                DataContext.Dispose();
                DataContext = null;
                UnapplyBindings();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetErrorMessage());
            }
        }

        private void InitializeComponent()
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetErrorMessage());
            }
        }

        private Frame CreateBackgroundCircleShape()
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetErrorMessage());
                return null;
            }
        }

        private Label CreateShamsiDayLabel()
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetErrorMessage());
                return null;
            }
        }
    }
}
