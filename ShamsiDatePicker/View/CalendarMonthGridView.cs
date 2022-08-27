using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using HMExtension.Xamarin;
using ShamsiDatePicker.ViewModel;
using System.Linq;

namespace ShamsiDatePicker.View
{
    internal class CalendarMonthGridView : ContentView
    {
        private Grid DateGrid { get; set; } = null;

        public CalendarMonthGridView()
        {
            try
            {
                DateGrid = new Grid()
                {
                    BackgroundColor = Color.Transparent,

                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,

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

                    RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                }
                };

                Content = DateGrid;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetErrorMessage());
            }
        }

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            "Items",
            typeof(List<CalendarDayBoxView>),
            typeof(CalendarMonthGridView),
            defaultValue: null,
            propertyChanged: OnItemsChanged);

        public List<CalendarDayBoxView> Items
        {
            get
            {
                return (List<CalendarDayBoxView>)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        private static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                var owner = bindable as CalendarMonthGridView;
                owner.OnItemsChanged(oldValue, newValue);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private void OnItemsChanged(object oldValue, object newValue)
        {
            try
            {
                DrawItems((List<CalendarDayBoxView>)newValue);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetErrorMessage());
            }
        }

        public void DrawItems(List<CalendarDayBoxView> items)
        {
            try
            {
                DateGrid.Children.Clear();

                if (items != null)
                {
                    if (items.Count > 0)
                    {
                        foreach (CalendarDayBoxView item in items)
                        {
                            DateGrid.Children.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

    }
}
