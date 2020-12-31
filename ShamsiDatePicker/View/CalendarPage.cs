using System;
using System.Collections.Generic;
using System.Text;
using ShamsiDatePicker.ViewModel;
using Xamarin.Forms;
using CarouselView.FormsPlugin.Abstractions;
using HMExtension.Xamarin.Mvvm;
using HMExtension.Xamarin.Component;

namespace ShamsiDatePicker.View
{
    public class CalendarPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        internal CalendarPageViewModel DataContext { get; set; } = null;
        internal CalendarPage(CalendarPageViewModel DataContext)
        {
            this.DataContext = DataContext;
            BindingContext = DataContext;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            FlowDirection = FlowDirection.RightToLeft;
            Content = CreateMainGrid();
        }

        #region MainGrid

        private Grid CreateMainGrid()
        {
            var MainGrid = new Grid
            {
                BackgroundColor = Color.White,

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
                    new RowDefinition()
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
                BackgroundColor = Color.FromHex("#7F000000")
            };

            BackgroundFrame.SetValue(Grid.ColumnSpanProperty, 3);
            BackgroundFrame.SetValue(Grid.RowSpanProperty, 3);

            var tempBinding = new Binding() { Source = DataContext, Path = "CancelCommand", };
            var tempTapGesture = new TapGestureRecognizer();
            tempTapGesture.SetBinding(TapGestureRecognizer.CommandProperty, tempBinding);

            BackgroundFrame.GestureRecognizers.Add(tempTapGesture);

            return BackgroundFrame;
        }

        #region ContentGrid

        private Grid CreateContentGrid()
        {
            var ContentGrid = new Grid()
            {
                BackgroundColor = Color.White,

                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition(),
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) }
                },

                Children =
                {
                    CreateHeaderGrid(),
                    CreateCarouselGrid(),
                    CreateYearListGrid(),
                    CreateButtonStackLayout()
                }
            };

            ContentGrid.SetValue(Grid.ColumnProperty, 1);
            ContentGrid.SetValue(Grid.RowProperty, 1);

            return ContentGrid;
        }

        #region HeaderGrid
        private StackLayout CreateHeaderGrid()
        {
            var HeaderGrid = new StackLayout()
            {
                BackgroundColor = Color.FromHex("#FF4081"),

                Children =
                {
                    CreateYearLabel(),
                    CreateSelectedDayLabel()
                }
            };

            HeaderGrid.SetValue(Grid.RowProperty, 0);

            return HeaderGrid;
        }

        private Label CreateYearLabel()
        {
            var YearLabel = new Label()
            {
                Margin = new Thickness(10, 0, 10, -5),
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontFamily = "B_Nazanin",
                FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label)),
                TextColor = Color.White,
            };

            YearLabel.SetBinding(Label.TextProperty, new Binding()
            {
                Source = DataContext,
                Path = "Year",
                Mode = BindingMode.OneWay
            });

            var TempGesture = new TapGestureRecognizer() { CommandParameter = YearLabel };
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
                Margin = new Thickness(10, -5, 10, 5),
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

            var TapEvent = new TapGestureRecognizer();
            TapEvent.SetBinding(TapGestureRecognizer.CommandProperty, new Binding() { Path = "GoToSelectedDay" });
            SelectedDayLabel.GestureRecognizers.Add(TapEvent);

            return SelectedDayLabel;
        }
        
        #endregion

        #region CarouselGrid

        private Grid CreateCarouselGrid()
        {
            var CarouselGrid = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition() { Height = new GridLength(7, GridUnitType.Star) },
                },

                Children =
                {
                    CreateCarouselView(),
                    CreateSignGrid(),
                },
            };

            CarouselGrid.SetValue(Grid.RowProperty, 1);

            return CarouselGrid;
        }

        private CarouselViewControl CreateCarouselView()
        {
            var carouselView = new CarouselViewControl()
            {
                FlowDirection = FlowDirection.RightToLeft
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
                Path = "Position"
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

                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition() { Height = new GridLength(6, GridUnitType.Star) }
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
                HorizontalOptions = LayoutOptions.Center,
                FontFamily = "B_Nazanin_Bold",
                FontSize = 16
            };

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

            return DayOfWeekTemplateGrid;
        }

        private CalendarMonthGridView CreateTemplateCarouselCalendarMonth()
        {
            var TemplateCalendarMonth = new CalendarMonthGridView();
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
                BackgroundColor = Color.White,

                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) }
                },

                Children =
                {
                    CreateYearListView(),
                    line,
                }
            };

            YearListGrid.SetBinding(Grid.IsVisibleProperty, new Binding() 
            {
                Source = DataContext,
                Path = "YearListVisibility" 
            });
            YearListGrid.SetValue(Grid.RowProperty, 1);

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

            var ItemSelectedEvent = new EventToCommandBehavior()
            {
                EventName = "ItemSelected",
                Converter = new SelectedItemEventArgsToSelectedItemConverter(),
            };
            ItemSelectedEvent.SetBinding(EventToCommandBehavior.CommandProperty, new Binding() { Path = "YearSelectedCommand" });

            YearListView.Behaviors.Add(ItemSelectedEvent);

            var TappedEvent = new TapGestureRecognizer();
            TappedEvent.SetBinding(TapGestureRecognizer.CommandProperty, new Binding() { Path = "YearListTapped" });

            YearListView.GestureRecognizers.Add(TappedEvent);

            YearListView.ItemTemplate = new DataTemplate(() =>
                {
                    return new ViewCell()
                    {
                        View = CreateTemplateListYearNumberLabel(),
                    };
                });

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
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    CreateOKButton(),
                    CreateCancelButton(),
                }
            };

            ButtonStackLayout.SetValue(Grid.RowProperty, 2);

            return ButtonStackLayout;
        }

        private Button CreateOKButton()
        {
            var OKButton = new Button()
            {
                Text = "تایید",
                TextColor = Color.FromHex("#FF4081"),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                FontFamily = "B_Nazanin",
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0, 0, 0, 5),
            };

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
                TextColor = Color.FromHex("#FF4081"),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                FontFamily = "B_Nazanin",
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(0, 0, 0, 5),
            };

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
