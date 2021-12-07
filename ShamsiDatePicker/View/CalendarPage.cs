using System;
using System.Collections.Generic;
using System.Text;
using ShamsiDatePicker.ViewModel;
using Xamarin.Forms;
using CarouselView.FormsPlugin.Abstractions;
using HMExtension.Xamarin.Mvvm;
using HMExtension.Xamarin.Component;
using System.Threading.Tasks;

namespace ShamsiDatePicker.View
{
    internal class CalendarPage : ContentPage
    {
        private ListView YearListView = null;
        public CalendarPageViewModel DataContext { get; set; } = null;

        private StackLayout HeaderGrid = null;

        private Grid MainGrid = null;

        public CalendarPage(CalendarPageViewModel DataContext)
        {
            this.DataContext = DataContext;
            this.DataContext.Initialize();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Content = null;
            SizeChanged += UpdateElementOnLayoutChanged;
            Appearing += (sender, e) =>
            {
                var AppearingAnimation = new Animation(v => MainGrid.Opacity = v, 0, 1);
                AppearingAnimation.Commit(MainGrid, nameof(AppearingAnimation), 16, 150, Easing.Linear, (d, b) => UpdateChildrenLayout());
            };
            BackgroundColor = Color.Transparent;
            FlowDirection = FlowDirection.RightToLeft;

            YearListView = CreateYearListView();
            HeaderGrid = CreateHeaderGrid();
            MainGrid = CreateMainGrid();
            Content = MainGrid;

            Disappearing += async (sender, e) =>
            {
                await MainGrid.FadeTo(0, 100);
            };
        }

        private void UpdateElementOnLayoutChanged(object sender, EventArgs e)
        {
            if (Height > Width)
            {
                HeaderGrid.SetValue(Grid.RowProperty, 0);
                HeaderGrid.SetValue(Grid.RowSpanProperty, 1);
                HeaderGrid.SetValue(Grid.ColumnProperty, 1);

                MainGrid.RowDefinitions[1].Height = new GridLength(3, GridUnitType.Star);
            }
            else
            {
                HeaderGrid.SetValue(Grid.RowProperty, 0);
                HeaderGrid.SetValue(Grid.RowSpanProperty, 3);
                HeaderGrid.SetValue(Grid.ColumnProperty, 0);

                MainGrid.RowDefinitions[1].Height = new GridLength(20, GridUnitType.Star);
            }
        }

        #region MainGrid

        private Grid CreateMainGrid()
        {
            var MainGrid = new Grid
            {
                Opacity = 0,
                Margin = 0,
                Padding = 0,

                BackgroundColor = Color.Transparent,

                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) },
                    new ColumnDefinition()
                },

                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition() { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition(),
                },

                Children =
                {
                    CreateBackGroundFrame(),
                    CreateContentGrid()
                }
            };

            return MainGrid;
        }

        private Frame CreateBackGroundFrame()
        {
            var BackgroundFrame = new Frame()
            {
                BorderColor = Color.Transparent,
                BackgroundColor = Color.FromHex("#80000000"),
            };

            BackgroundFrame.SetValue(Grid.ColumnSpanProperty, 3);
            BackgroundFrame.SetValue(Grid.RowSpanProperty, 3);

            var tempBinding = new Binding() { Source = DataContext, Path = "CancelCommand", };
            var tempTapGesture = new TapGestureRecognizer() { CommandParameter = this};
            tempTapGesture.SetBinding(TapGestureRecognizer.CommandProperty, tempBinding);

            BackgroundFrame.GestureRecognizers.Add(tempTapGesture);

            return BackgroundFrame;
        }

        #region ContentGrid

        private Grid CreateContentGrid()
        {
            var ContentGrid = new Grid()
            {
                Padding = 0,
                Margin = 0,
                RowSpacing = 0,
                ColumnSpacing = 0,

                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition(),
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) }
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                },

                Children =
                {
                    HeaderGrid,
                    CreateCarouselGrid(),
                    CreateYearListGrid(),
                    CreateButtonStackLayout(),
                }
            };

            ContentGrid.SetBinding(BackgroundColorProperty, new Binding()
            {
                Path = "CalendarBackgroundColor",
                Source = DataContext,
            });

            ContentGrid.SetValue(Grid.ColumnProperty, 1);
            ContentGrid.SetValue(Grid.RowProperty, 1);

            return ContentGrid;
        }

        #region HeaderGrid
        private StackLayout CreateHeaderGrid()
        {
            var HeaderGrid = new StackLayout()
            { 
                Margin = 0,
                Padding = 0,

                Children =
                {
                    CreateYearLabel(),
                    CreateSelectedDayLabel()
                }
            };

            HeaderGrid.SetBinding(BackgroundColorProperty, new Binding()
            {
                Path = "HeaderBackgroundColor",
                Source = DataContext
            });

            HeaderGrid.SetValue(Grid.RowProperty, 0);
            HeaderGrid.SetValue(Grid.ColumnProperty, 1);

            return HeaderGrid;
        }

        private Label CreateYearLabel()
        {
            var YearLabel = new Label()
            {
                Margin = new Thickness(10, 0, 10, 0),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontFamily = "B_Nazanin",
                FontSize = 24,
            };

            YearLabel.SetBinding(Label.TextProperty, new Binding()
            {
                Source = DataContext,
                Path = "Year",
                Mode = BindingMode.OneWay
            });

            YearLabel.SetBinding(Label.TextColorProperty, new Binding()
            {
                Source = DataContext,
                Path = "HeaderTitleTextColor",
            });

            var TempGesture = new TapGestureRecognizer()
            {
                CommandParameter = new List<object>() { YearLabel, YearListView }, 
            };
            TempGesture.SetBinding(TapGestureRecognizer.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = "YearTappedCommand"

            });

            YearLabel.GestureRecognizers.Add(TempGesture);

            return YearLabel;
        }

        private Label CreateSelectedDayLabel()
        {
            var SelectedDayLabel = new Label()
            {
                Margin = new Thickness(10, 0, 10, 5),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontFamily = "B_Nazanin_Bold",
                FontSize = 30,
                TextColor = Color.White,
            };

            SelectedDayLabel.SetValue(Grid.RowProperty, 1);

            SelectedDayLabel.SetBinding(Label.TextProperty, new Binding()
            {
                Source = DataContext,
                Path = "SelectedDay",
                Mode = BindingMode.OneWay
            });

            SelectedDayLabel.SetBinding(Label.TextColorProperty, new Binding()
            {
                Source = DataContext,
                Path = "HeaderSubTitleTextColor",
            });

            var TapEvent = new TapGestureRecognizer();
            TapEvent.SetBinding(TapGestureRecognizer.CommandProperty, 
                new Binding() 
                { 
                    Path = "GoToSelectedDay", 
                    Source = DataContext
                });
            SelectedDayLabel.GestureRecognizers.Add(TapEvent);

            return SelectedDayLabel;
        }
        
        #endregion

        #region CarouselGrid

        private Grid CreateCarouselGrid()
        {
            var CarouselGrid = new Grid()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Padding = 0,
                Margin = 0,

                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition() { Height = new GridLength(3.5, GridUnitType.Star) },
                },

                Children =
                {
                    CreateCarouselView(),
                    CreateSignGrid(),
                },
            };

            CarouselGrid.SetValue(Grid.RowProperty, 1);
            CarouselGrid.SetValue(Grid.ColumnProperty, 1);

            return CarouselGrid;
        }

        private CarouselViewControl CreateCarouselView()
        {
            var carouselView = new CarouselViewControl()
            {
                FlowDirection = FlowDirection.RightToLeft,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = 0,
            };

            carouselView.SetValue(Grid.RowSpanProperty, 2);

            carouselView.SetBinding(CarouselViewControl.ItemsSourceProperty, new Binding()
            {
                Source = DataContext,
                Path = "CarouselItems"
            });

            carouselView.SetBinding(CarouselViewControl.PositionProperty, new Binding()
            {
                Source = DataContext,
                Path = "Position",
                Mode = BindingMode.TwoWay,
            });

            carouselView.ItemTemplate = new DataTemplate(() => { return CreateTemplateCarouselMainGrid(); });

            return carouselView;
        }

        #region TemplateCarouselMainGrid

        private Grid CreateTemplateCarouselMainGrid()
        {
            var MainTemplateGrid = new Grid()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 0,
                Margin = 0,

                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition() { Height = new GridLength(0.5, GridUnitType.Star) },
                    new RowDefinition() { Height = new GridLength(3, GridUnitType.Star) }
                },

                Children =
                {
                    CreateTemplateCarouselHeaderLabel(),
                    CreateTemplateCarouselDayOfWeekGrid(),
                    CreateTemplateCarouselCalendarMonth()
                }
            };

            return MainTemplateGrid;
        }

        private Label CreateTemplateCarouselHeaderLabel()
        {
            var HeaderTamplateLabel = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontFamily = "B_Nazanin_Bold",
                FontSize = 16
            };

            HeaderTamplateLabel.SetBinding(Label.TextColorProperty, new Binding()
            {
                Source = DataContext,
                Path = "CalendarTitleColor",
            });
                
            HeaderTamplateLabel.SetBinding(Label.TextProperty, new Binding()
            {
                Path = "Header",
                Mode = BindingMode.OneWay,
            });

            return HeaderTamplateLabel;
        }

        private Grid CreateTemplateCarouselDayOfWeekGrid()
        {
            var DayOfWeekTemplateGrid = new Grid()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = 0,
                Margin = 0,

                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(0.5 , GridUnitType.Star) },
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition() { Width = new GridLength(0.5 , GridUnitType.Star) },
                },
            };

            DayOfWeekTemplateGrid.SetValue(Grid.RowProperty, 1);

            DayOfWeekTemplateGrid.Children.Add(new Label()
            {
                Text = "ش",
                HorizontalTextAlignment = TextAlignment.Center
            }, 1, 0);

            DayOfWeekTemplateGrid.Children.Add(new Label()
            {
                Text = "ی",
                HorizontalTextAlignment = TextAlignment.Center
            }, 2, 0);

            DayOfWeekTemplateGrid.Children.Add(new Label()
            {
                Text = "د",
                HorizontalTextAlignment = TextAlignment.Center
            }, 3, 0);

            DayOfWeekTemplateGrid.Children.Add(new Label()
            {
                Text = "س",
                HorizontalTextAlignment = TextAlignment.Center
            }, 4, 0);

            DayOfWeekTemplateGrid.Children.Add(new Label()
            {
                Text = "چ",
                HorizontalTextAlignment = TextAlignment.Center
            }, 5, 0);

            DayOfWeekTemplateGrid.Children.Add(new Label()
            {
                Text = "پ",
                HorizontalTextAlignment = TextAlignment.Center
            }, 6, 0);

            DayOfWeekTemplateGrid.Children.Add(new Label()
            {
                Text = "ج",
                HorizontalTextAlignment = TextAlignment.Center
            }, 7, 0);

            foreach(Label label in DayOfWeekTemplateGrid.Children)
            {
                try
                {
                    label.SetBinding(Label.TextColorProperty, new Binding()
                    {
                        Source = DataContext,
                        Path = "CalendarSubTitleColor",
                    });
                }
                catch { }
            }
            return DayOfWeekTemplateGrid;
        }

        private CalendarMonthGridView CreateTemplateCarouselCalendarMonth()
        {
            var TemplateCalendarMonth = new CalendarMonthGridView() 
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            TemplateCalendarMonth.SetBinding(CalendarMonthGridView.ItemsProperty, new Binding() { Path = "Days" });
            TemplateCalendarMonth.SetValue(Grid.RowProperty, 2);

            return TemplateCalendarMonth;
        }
        #endregion

        #region SignGrid
        private Grid CreateSignGrid()
        {
            var ForwardShape = CreateForwardShape();
            var ForwardImage = CreateForwardImage();
            var BackwardShape = CreateBackwardShape();
            var BackwardImage = CreateBackwardImage();

            var TapForwardCommand = new TapGestureRecognizer()
            {
                CommandParameter = ForwardShape
            };
            TapForwardCommand.SetBinding(TapGestureRecognizer.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = "ForwardCommand"
            });

            var TapBackwardCommand = new TapGestureRecognizer()
            {
                CommandParameter = BackwardShape
            };
            TapBackwardCommand.SetBinding(TapGestureRecognizer.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = "BackwardCommand"
            });

            ForwardShape.GestureRecognizers.Add(TapForwardCommand);
            ForwardImage.GestureRecognizers.Add(TapForwardCommand);

            BackwardShape.GestureRecognizers.Add(TapBackwardCommand);
            BackwardImage.GestureRecognizers.Add(TapBackwardCommand);

            var SignGrid = new Grid()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = 0,
                Padding = 0,

                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(0.5 , GridUnitType.Star) },
                    new ColumnDefinition(),
                    new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) },
                    new ColumnDefinition(),
                    new ColumnDefinition() { Width = new GridLength(0.5 , GridUnitType.Star) }
                },

                Children =
                {
                    ForwardShape,
                    ForwardImage,

                    BackwardShape,
                    BackwardImage
                }
            };

            return SignGrid;
        }

        private XFShapeView.ShapeView CreateForwardShape()
        {
            var ForwardShape = new XFShapeView.ShapeView()
            {
                ShapeType = XFShapeView.ShapeType.Circle,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Color = Color.Silver,
                Opacity = 0
            };

            ForwardShape.SetValue(Grid.ColumnProperty, 1);

            ForwardShape.SetBinding(XFShapeView.ShapeView.ColorProperty, new Binding()
            {
                Source = DataContext,
                Path = "CalendarTitleColor",
            });

            return ForwardShape;
        }

        private Image CreateForwardImage()
        {
            var ForwardImage = new Image()
            {
                Source = ImageSource.FromResource("ShamsiDatePicker.Resources.Images.right.png"),
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 15,
                HeightRequest = 15
            };
            
            ForwardImage.SetValue(Grid.ColumnProperty, 1);

            var TintEffect = new TintImageEffect();
            TintEffect.TintColor = DataContext.CalendarTitleColor;

            ForwardImage.Effects.Add(TintEffect);

            return ForwardImage;
        }

        private XFShapeView.ShapeView CreateBackwardShape()
        {
            var BackwardShape = new XFShapeView.ShapeView()
            {
                ShapeType = XFShapeView.ShapeType.Circle,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Color = Color.Silver,
                Opacity = 0
            };

            BackwardShape.SetValue(Grid.ColumnProperty, 3);

            BackwardShape.SetBinding(XFShapeView.ShapeView.ColorProperty, new Binding()
            {
                Source = DataContext,
                Path = "CalendarTitleColor",
            });

            return BackwardShape;
        }

        private Image CreateBackwardImage()
        {
            var BackwardImage = new Image()
            {
                Source = ImageSource.FromResource("ShamsiDatePicker.Resources.Images.left.png"),
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 15,
                HeightRequest = 15
            };

            BackwardImage.SetValue(Grid.ColumnProperty, 3);

            var TintEffect = new TintImageEffect();
            TintEffect.TintColor = DataContext.CalendarTitleColor;

            BackwardImage.Effects.Add(TintEffect);

            return BackwardImage;
        }

        #endregion //SignGrid

        #endregion //CarouselGrid

        #region YearListGrid
        private Grid CreateYearListGrid()
        {
            var line = new BoxView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 0.6,
                Color = Color.Silver,
            };
            line.SetValue(Grid.RowProperty, 1);

            var YearListGrid = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) }
                },

                Children =
                {
                    YearListView,
                    line,
                }
            };
            
            YearListGrid.SetBinding(IsVisibleProperty, new Binding() 
            {
                Source = DataContext,
                Path = "YearListVisibility" 
            });

            YearListGrid.SetBinding(BackgroundColorProperty, new Binding()
            {
                Path = "CalendarBackgroundColor",
                Source = DataContext,
            });


            YearListGrid.SetValue(Grid.RowProperty, 1);
            YearListGrid.SetValue(Grid.ColumnProperty, 1);

            return YearListGrid;
        }

        private ListView CreateYearListView()
        {
            var YearListView = new ListView()
            {
                SelectionMode = ListViewSelectionMode.Single,
                SeparatorVisibility = SeparatorVisibility.None,
                HorizontalOptions = LayoutOptions.Center,
            };

            YearListView.SetBinding(ListView.ItemsSourceProperty, new Binding() 
            {
                Source = DataContext,
                Path = "YearList" 
            });
            YearListView.SetBinding(ListView.SelectedItemProperty, new Binding() 
            {
                Source = DataContext,
                Path = "SelectedYear", 
                Mode = BindingMode.TwoWay 
            });

            YearListView.ItemTemplate = new DataTemplate(() =>
            {
                return new ViewCell()
                {
                    View = CreateTemplateListYearNumberLabel(),
                };
            });

            var ItemSelectedEvent = new EventToCommandBehavior()
            {
                EventName = "ItemSelected",
                Converter = new SelectedItemEventArgsToSelectedItemConverter(),
            };
            ItemSelectedEvent.SetBinding(EventToCommandBehavior.CommandProperty, new Binding() 
            { 
                Source = DataContext,
                Path = "YearSelectedCommand"
            });
            YearListView.Behaviors.Add(ItemSelectedEvent);

            var ItemTappedEvent = new EventToCommandBehavior() 
            { 
                EventName = "ItemTapped" 
            };
            ItemTappedEvent.SetBinding(EventToCommandBehavior.CommandProperty, new Binding() 
            { 
                Source = DataContext,
                Path = "YearListTapped" 
            });
            YearListView.Behaviors.Add(ItemTappedEvent);

            return YearListView;
        }

        private Label CreateTemplateListYearNumberLabel()
        {
            var TemplateListYearNumberLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontFamily = "B_Nazanin",
            };

            TemplateListYearNumberLabel.SetBinding(Label.TextProperty, new Binding() { Path = "YearNumber" });
            TemplateListYearNumberLabel.SetBinding(Label.FontSizeProperty, new Binding() { Path = "FontSize" });
            TemplateListYearNumberLabel.SetBinding(Label.TextColorProperty, new Binding() { Path = "ForeColor" });

            return TemplateListYearNumberLabel;
        }
        #endregion

        #region ButtonStackLayout
        private StackLayout CreateButtonStackLayout()
        {
            var ButtonStackLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = 0,
                Padding = 0,
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    CreateOKButton(),
                    CreateCancelButton(),
                }
            };

            /*ButtonStackLayout.SetBinding(StackLayout.OrientationProperty, new Binding()
            {
                Source = DataContext,
                Path = "NotDeviceOrientation",
            });*/

            ButtonStackLayout.SetValue(Grid.RowProperty, 2);
            ButtonStackLayout.SetValue(Grid.ColumnProperty, 1);

            return ButtonStackLayout;
        }

        private Button CreateOKButton()
        {
            var OKButton = new Button()
            {
                Text = "تایید",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                FontFamily = "B_Nazanin",
                Margin = new Thickness(5, 10, 10, 10),
                CommandParameter = this,
            };

            OKButton.SetBinding(Button.TextColorProperty, new Binding()
            {
                Source = DataContext,
                Path = "CalendarOKButtonTextColor",
            });

            OKButton.SetBinding(Button.BackgroundColorProperty, new Binding()
            {
                Source = DataContext,
                Path = "CalendarOKButtonBackgroundColor",
            });

            OKButton.SetBinding(Button.CommandProperty, new Binding() 
            {
                Source = DataContext,
                Path = "OkCommand" 
            });

            return OKButton;
        }

        private Button CreateCancelButton()
        {
            var CancelButton = new Button()
            {
                Text = "انصراف",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                FontFamily = "B_Nazanin",
                Margin = new Thickness(10, 10, 0, 10),
                CommandParameter = this,
            };

            CancelButton.SetBinding(Button.TextColorProperty, new Binding()
            {
                Source = DataContext,
                Path = "CalendarCancelButtonTextColor",
            });

            CancelButton.SetBinding(Button.BackgroundColorProperty, new Binding()
            {
                Source = DataContext,
                Path = "CalendarCancelButtonBackgroundColor",
            });

            CancelButton.SetBinding(Button.CommandProperty, new Binding() 
            {
                Source = DataContext,
                Path = "CancelCommand" 
            });

            return CancelButton;
        }

        #endregion //ButtonStackLayout

        #endregion //ContentGrid

        #endregion //MainGrid
    }
}
