using System;
using System.Collections.Generic;
using System.Text;
using ShamsiDatePicker.ViewModel;
using HMExtension.Maui;
using System.Threading.Tasks;
using System.Diagnostics;
using HMControls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.Shapes;
using System.Linq;

namespace ShamsiDatePicker.View;

internal class CalendarPage : ContentPage
{
    public CalendarPage()
    {
        try
        {
            NavigationPage.SetHasNavigationBar(this, false);
            SizeChanged += UpdateElementOnLayoutChanged;
            Appearing += (s, e) =>
            {
                DataContext.Initialize();
            };
            BackgroundColor = Color.FromArgb("#80000000");
            FlowDirection = FlowDirection.RightToLeft;

            Disappearing += async (s, e) =>
            {
                await MainGrid.FadeTo(0, 100);
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    private CalendarPageViewModel _dataContext = null;
    public CalendarPageViewModel DataContext 
    {
        get => _dataContext;
        set  
        {
            if(_dataContext != value && value != null)
            {
                _dataContext = value;
                OnPropertyChanged();
                Initialize();
            }
        }
    }

    private ListView YearListView { get; set; } = null;

    private VerticalStackLayout HeaderStackLayout { get; set; } = null;

    private Grid CarouselGrid { get; set; } = null;

    private Grid YearListGrid { get; set; } = null;

    private HorizontalStackLayout ButtonStackLayout { get; set; } = null;

    private Grid MainGrid { get; set; } = null;

    private Grid ContentGrid { get; set; } = null;

    private void Initialize()
    {
        try
        {
            YearListView = CreateYearListView();
            HeaderStackLayout = CreateHeaderStackLayout();
            YearListGrid = CreateYearListGrid();
            CarouselGrid = CreateCarouselGrid();
            ButtonStackLayout = CreateButtonStackLayout();
            ContentGrid = CreateContentGrid();
            MainGrid = CreateMainGrid();
            Content = MainGrid;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    private void UpdateElementOnLayoutChanged(object sender, EventArgs e)
    {
        try
        {
            if (Height > Width)
            {
                HeaderStackLayout.SetValue(Grid.RowSpanProperty, 1);
                HeaderStackLayout.SetValue(Grid.ColumnSpanProperty, 2);

                CarouselGrid.SetValue(Grid.RowProperty, 1);
                CarouselGrid.SetValue(Grid.RowSpanProperty, 1);
                CarouselGrid.SetValue(Grid.ColumnProperty, 0);
                CarouselGrid.SetValue(Grid.ColumnSpanProperty, 2);

                YearListGrid.SetValue(Grid.RowProperty, 1);
                YearListGrid.SetValue(Grid.RowSpanProperty, 1);
                YearListGrid.SetValue(Grid.ColumnProperty, 0);
                YearListGrid.SetValue(Grid.ColumnSpanProperty, 2);

                ButtonStackLayout.SetValue(Grid.ColumnProperty, 0);
                ButtonStackLayout.SetValue(Grid.ColumnSpanProperty, 2);

                MainGrid.RowDefinitions[1].Height = new GridLength(3, GridUnitType.Star);
            }
            else
            {
                HeaderStackLayout.SetValue(Grid.RowSpanProperty, 3);
                HeaderStackLayout.SetValue(Grid.ColumnProperty, 0);
                HeaderStackLayout.SetValue(Grid.ColumnSpanProperty, 1);

                CarouselGrid.SetValue(Grid.RowProperty, 0);
                CarouselGrid.SetValue(Grid.RowSpanProperty, 2);
                CarouselGrid.SetValue(Grid.ColumnProperty, 1);
                CarouselGrid.SetValue(Grid.ColumnSpanProperty, 1);

                YearListGrid.SetValue(Grid.RowProperty, 0);
                YearListGrid.SetValue(Grid.RowSpanProperty, 2);
                YearListGrid.SetValue(Grid.ColumnProperty, 1);
                YearListGrid.SetValue(Grid.ColumnSpanProperty, 1);

                ButtonStackLayout.SetValue(Grid.ColumnProperty, 1);
                ButtonStackLayout.SetValue(Grid.ColumnSpanProperty, 1);

                MainGrid.RowDefinitions[1].Height = new GridLength(20, GridUnitType.Star);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    #region MainGrid

    private Grid CreateMainGrid()
    {
        try
        {
            Grid grid = new()
            {
                Margin = 0,
                Padding = 0,

                BackgroundColor = Colors.Transparent,

                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition() { Width = new GridLength(7, GridUnitType.Star) },
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
                    ContentGrid,
                }
            };

            return grid;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    #region ContentGrid

    private Grid CreateContentGrid()
    {
        try
        {
            Grid grid = new()
            {
                Padding = 0,
                Margin = 0,
                RowSpacing = 0,
                ColumnSpacing = 0,
                BackgroundColor = Globals.CalendarBackgroundColor,

                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition(),
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) }
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(2.5, GridUnitType.Star) },
                },

                Children =
                {
                    HeaderStackLayout,
                    CarouselGrid,
                    YearListGrid,
                    ButtonStackLayout,
                }
            };

            grid.SetValue(Grid.ColumnProperty, 1);
            grid.SetValue(Grid.RowProperty, 1);

            return grid;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #region HeaderGrid

    private VerticalStackLayout CreateHeaderStackLayout()
    {
        try
        {
            VerticalStackLayout stack = new()
            {
                Margin = 0,
                Padding = 0,
                BackgroundColor = Globals.HeaderBackgroundColor,

                Children =
                {
                    CreateYearLabel(),
                    CreateSelectedDayLabel()
                }
            };

            stack.SetValue(Grid.RowProperty, 0);
            stack.SetValue(Grid.ColumnProperty, 0);
            stack.SetValue(Grid.ColumnSpanProperty, 2);

            return stack;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private Label CreateYearLabel()
    {
        try
        {
            Label label = new()
            {
                Margin = new Thickness(10, 0, 10, 0),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontFamily = "B_Nazanin",
                FontSize = 24,
                TextColor = Globals.HeaderTitleTextColor,
            };

            label.SetBinding(Label.TextProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.Year),
                Mode = BindingMode.OneWay
            });

            var TempGesture = new TapGestureRecognizer()
            {
                CommandParameter = new List<object>() 
                {
                    label, 
                    YearListView 
                },
            };

            TempGesture.SetBinding(TapGestureRecognizer.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.YearTappedCommand),
            });

            label.GestureRecognizers.Add(TempGesture);

            return label;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private Label CreateSelectedDayLabel()
    {
        try
        {
            Label label = new()
            {
                Margin = new Thickness(10, 0, 10, 5),
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontFamily = "B_Nazanin_Bold",
                FontSize = 30,
                TextColor = Globals.HeaderSubTitleTextColor,
            };

            label.SetValue(Grid.RowProperty, 1);

            label.SetBinding(Label.TextProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.SelectedDay),
                Mode = BindingMode.OneWay
            });

            var TapEvent = new TapGestureRecognizer();
            TapEvent.SetBinding(TapGestureRecognizer.CommandProperty, new Binding()
            {
                Path = nameof(DataContext.GoToSelectedDay),
                Source = DataContext
            });
            label.GestureRecognizers.Add(TapEvent);

            return label;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region CarouselGrid

    private Grid CreateCarouselGrid()
    {
        try
        {
            Grid grid = new()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Colors.Transparent,

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

            grid.SetValue(Grid.RowProperty, 1);
            grid.SetValue(Grid.ColumnProperty, 0);
            grid.SetValue(Grid.ColumnSpanProperty, 2);

            return grid;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private CarouselView CreateCarouselView()
    {
        try
        {
            CarouselView carouselView = new()
            {
                FlowDirection = FlowDirection.RightToLeft,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = 0,
                Loop = false,
                BackgroundColor = Colors.Transparent,
            };

            carouselView.PositionChanged += (s, e) =>
            {
                try
                {
                    CarouselView sender = s as CarouselView;
                    CarouselItem currentItem = DataContext.CarouselItems[e.CurrentPosition];
                    currentItem?.CreateDaysIfNeeded();
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.GetErrorMessage());
                }
            };

            carouselView.SetValue(Grid.RowSpanProperty, 2);

            carouselView.SetBinding(ItemsView.ItemsSourceProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.CarouselItems)
            });

            carouselView.SetBinding(CarouselView.PositionProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.Position),
                Mode = BindingMode.TwoWay,
            });

            carouselView.ItemTemplate = new DataTemplate(CreateTemplateCarouselMainGrid);

            return carouselView;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #region TemplateCarouselMainGrid

    private Grid CreateTemplateCarouselMainGrid()
    {
        try
        {
            Grid grid = new()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = 0,
                Margin = 0,
                BackgroundColor = Colors.Transparent,

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

            return grid;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static Label CreateTemplateCarouselHeaderLabel()
    {
        try
        {
            Label label = new()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                FontFamily = "B_Nazanin_Bold",
                FontSize = 16,
                TextColor = Globals.CalendarTitleColor,
            };

            label.SetBinding(Label.TextProperty, new Binding()
            {
                Path = nameof(CarouselItem.Header),
                Mode = BindingMode.OneWay,
            });

            return label;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static Grid CreateTemplateCarouselDayOfWeekGrid()
    {
        try
        {
            Label l1 = new()
            {
                Text = "ش",
                TextColor = Globals.CalendarSubTitleColor,
                HorizontalTextAlignment = TextAlignment.Center,
            };
            l1.SetValue(Grid.RowProperty, 0);
            l1.SetValue(Grid.ColumnProperty, 1);

            Label l2 = new()
            {
                Text = "ی",
                TextColor = Globals.CalendarSubTitleColor,
                HorizontalTextAlignment = TextAlignment.Center,
            };
            l2.SetValue(Grid.RowProperty, 0);
            l2.SetValue(Grid.ColumnProperty, 2);

            Label l3 = new()
            {
                Text = "د",
                TextColor = Globals.CalendarSubTitleColor,
                HorizontalTextAlignment = TextAlignment.Center
            };
            l3.SetValue(Grid.RowProperty, 0);
            l3.SetValue(Grid.ColumnProperty, 3);

            Label l4 = new()
            {
                Text = "س",
                TextColor = Globals.CalendarSubTitleColor,
                HorizontalTextAlignment = TextAlignment.Center
            };
            l4.SetValue(Grid.RowProperty, 0);
            l4.SetValue(Grid.ColumnProperty, 4);

            Label l5 = new()
            {
                Text = "چ",
                TextColor = Globals.CalendarSubTitleColor,
                HorizontalTextAlignment = TextAlignment.Center
            };
            l5.SetValue(Grid.RowProperty, 0);
            l5.SetValue(Grid.ColumnProperty, 5);

            Label l6 = new()
            {
                Text = "پ",
                TextColor = Globals.CalendarSubTitleColor,
                HorizontalTextAlignment = TextAlignment.Center
            };
            l6.SetValue(Grid.RowProperty, 0);
            l6.SetValue(Grid.ColumnProperty, 6);

            Label l7 = new()
            {
                Text = "ج",
                TextColor = Globals.CalendarSubTitleColor,
                HorizontalTextAlignment = TextAlignment.Center
            };
            l7.SetValue(Grid.RowProperty, 0);
            l7.SetValue(Grid.ColumnProperty, 7);

            Grid grid = new()
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
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
                
                Children =
                {
                    l1, l2, l3, l4, l5, l6, l7
                }
            };

            grid.SetValue(Grid.RowProperty, 1);

            return grid;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static CalendarMonthGridView CreateTemplateCarouselCalendarMonth()
    {
        try
        {
            CalendarMonthGridView calendarMonth = [];

            calendarMonth.SetBinding(CalendarMonthGridView.ItemsProperty, new Binding()
            {
                Path = nameof(CarouselItem.Days)
            });

            calendarMonth.SetValue(Grid.RowProperty, 2);

            return calendarMonth;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region SignGrid

    private Grid CreateSignGrid()
    {
        try
        {
            var ForwardShape = CreateForwardShape();
            var ForwardImage = CreateForwardImage();
            var BackwardShape = CreateBackwardShape();
            var BackwardImage = CreateBackwardImage();

            TapGestureRecognizer TapForwardCommand = new()
            {
                CommandParameter = ForwardShape
            };
            TapForwardCommand.SetBinding(TapGestureRecognizer.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.ForwardCommand),
            });

            TapGestureRecognizer TapBackwardCommand = new()
            {
                CommandParameter = BackwardShape
            };
            TapBackwardCommand.SetBinding(TapGestureRecognizer.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.BackwardCommand),
            });

            ForwardShape.GestureRecognizers.Add(TapForwardCommand);
            BackwardShape.GestureRecognizers.Add(TapBackwardCommand);

            var grid = new Grid()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
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

            return grid;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static Frame CreateForwardShape()
    {
        try
        {
            Frame frame = new()
            {
                HeightRequest = 50,
                WidthRequest = 50,
                CornerRadius = 25,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Globals.CalendarTitleColor,
                BorderColor = Colors.Transparent,
                Opacity = 0,
            };

            frame.SetValue(Grid.ColumnProperty, 1);

            return frame;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static ImageTinted CreateForwardImage()
    {
        try
        {
            ImageTinted image = new()
            {
                Source = ImageSource.FromResource("ShamsiDatePicker.Resources.Images.right.png"),
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 15,
                HeightRequest = 15,
                TintColor = Globals.CalendarTitleColor,
            };

            image.SetValue(Grid.ColumnProperty, 1);

            return image;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static Frame CreateBackwardShape()
    {
        try
        {
            Frame frame = new()
            {
                HeightRequest = 50,
                WidthRequest = 50,
                CornerRadius = 25,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Globals.CalendarTitleColor,
                BorderColor = Colors.Transparent,
                Opacity = 0
            };

            frame.SetValue(Grid.ColumnProperty, 3);

            return frame;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static ImageTinted CreateBackwardImage()
    {
        try
        {
            ImageTinted image = new()
            {
                Source = ImageSource.FromResource("ShamsiDatePicker.Resources.Images.left.png"),
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 15,
                HeightRequest = 15,
                TintColor = Globals.CalendarTitleColor,
            };

            image.SetValue(Grid.ColumnProperty, 3);

            return image;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion //SignGrid

    #endregion //CarouselGrid

    #region YearListGrid

    private Grid CreateYearListGrid()
    {
        try
        {
            BoxView line = new()
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = 0.6,
                Color = Colors.Silver,
            };
            line.SetValue(Grid.RowProperty, 1);

            Grid grid = new()
            {
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition() { Height = new GridLength(0.6, GridUnitType.Absolute) },
                },

                Children =
                {
                    YearListView,
                    line,
                },

                BackgroundColor = Globals.CalendarBackgroundColor,
            };

            grid.SetBinding(IsVisibleProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.YearListVisibility),
            });

            grid.SetValue(Grid.RowProperty, 1);
            grid.SetValue(Grid.ColumnProperty, 0);
            grid.SetValue(Grid.ColumnSpanProperty, 2);

            return grid;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private ListView CreateYearListView()
    {
        try
        {
            ListView listView = new()
            {
                SelectionMode = ListViewSelectionMode.Single,
                SeparatorVisibility = SeparatorVisibility.None,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Globals.CalendarBackgroundColor,
            };

            listView.SetBinding(ListView.ItemsSourceProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.YearList),
            });
            listView.SetBinding(ListView.SelectedItemProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.SelectedYear),
                Mode = BindingMode.TwoWay
            });

            listView.ItemTemplate = new DataTemplate(() =>
            {
                return new ViewCell()
                {
                    View = CreateTemplateListYearNumberLabel(),
                };
            });

            EventToCommandBehavior ItemSelectedEvent = new()
            {
                EventName = nameof(ListView.ItemSelected),
                Converter = new SelectedItemEventArgsToSelectedItemConverter(),
            };

            ItemSelectedEvent.SetBinding(EventToCommandBehavior.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.YearSelectedCommand),
            });

            listView.Behaviors.Add(ItemSelectedEvent);

            EventToCommandBehavior ItemTappedEvent = new()
            {
                EventName = nameof(ListView.ItemTapped),
            };

            ItemTappedEvent.SetBinding(EventToCommandBehavior.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.YearListTapped),
            });

            listView.Behaviors.Add(ItemTappedEvent);

            return listView;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static Label CreateTemplateListYearNumberLabel()
    {
        try
        {
            Label label = new()
            {
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                FontFamily = "B_Nazanin",
            };

            label.SetBinding(Label.TextProperty, new Binding() 
            { 
                Path = nameof(YearListViewModel.YearNumber), 
            });
            label.SetBinding(Label.FontSizeProperty, new Binding() 
            { 
                Path = nameof(YearListViewModel.FontSize),
            });
            label.SetBinding(Label.TextColorProperty, new Binding() 
            { 
                Path = nameof(YearListViewModel.ForeColor),
            });

            return label;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region ButtonStackLayout

    private HorizontalStackLayout CreateButtonStackLayout()
    {
        try
        {
            HorizontalStackLayout stack = new()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = 0,
                Padding = 0,
                Children =
                {
                    CreateOKButton(),
                    CreateCancelButton(),
                }
            };

            stack.SetValue(Grid.RowProperty, 2);
            stack.SetValue(Grid.ColumnProperty, 0);
            stack.SetValue(Grid.ColumnSpanProperty, 2);

            return stack;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private Button CreateOKButton()
    {
        try
        {
            Button button = new()
            {
                Text = "تایید",
                FontSize = Application.Current.GetNamedSize(NamedSizes.Body),
                FontFamily = "B_Nazanin",
                Margin = new Thickness(10, 10, 5, 10),
                Padding = 5,
                WidthRequest = 100,
                CommandParameter = this,
                TextColor = Globals.CalendarOKButtonTextColor,
                BackgroundColor = Globals.CalendarOKButtonBackgroundColor,
            };

            button.SetBinding(Button.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.OkCommand),
            });

            return button;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private Button CreateCancelButton()
    {
        try
        {
            Button button = new()
            {
                Text = "انصراف",
                FontSize = Application.Current.GetNamedSize(NamedSizes.Body),
                FontFamily = "B_Nazanin",
                Margin = new Thickness(0, 10, 10, 10),
                Padding = 5,
                WidthRequest = 100,
                CommandParameter = this,
                TextColor = Globals.CalendarCancelButtonTextColor,
                BackgroundColor = Globals.CalendarCancelButtonBackgroundColor,
            };

            button.SetBinding(Button.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.CancelCommand),
            });

            return button;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion //ButtonStackLayout

    #endregion //ContentGrid

    #endregion //MainGrid

}
