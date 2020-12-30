using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShamsiDatePicker.View
{
    internal class CalendarMonthGridView : ContentView
    {
        private Grid DateGrid { get; set; } = null;

        public CalendarMonthGridView()
        {
            DateGrid = new Grid()
            {
                BackgroundColor = Color.White,

                HorizontalOptions = LayoutOptions.Fill,

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

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
    "Items",
    typeof(List<CalendarDayBoxView>),
    typeof(CalendarMonthGridView),
    defaultValue: null,
    propertyChanged: OnItemsChanged);

        internal List<CalendarDayBoxView> Items
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
            DrawItems((List<CalendarDayBoxView>)newValue);
        }

        internal void DrawItems(List<CalendarDayBoxView> Items)
        {
            try
            {
                DateGrid.Children.Clear();

                if (Items != null)
                {
                    if (Items.Count > 0)
                    {
                        foreach (var item in Items)
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
