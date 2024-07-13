using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using HMExtension.Maui;
using ShamsiDatePicker.ViewModel;
using System.Linq;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace ShamsiDatePicker.View;

internal class CalendarMonthGridView : Grid
{
    public CalendarMonthGridView()
    {
        BackgroundColor = Colors.Transparent;

        HorizontalOptions = LayoutOptions.Fill;
        VerticalOptions = LayoutOptions.Fill;

        ColumnDefinitions =
        [
            new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) },
            new ColumnDefinition(),
            new ColumnDefinition(),
            new ColumnDefinition(),
            new ColumnDefinition(),
            new ColumnDefinition(),
            new ColumnDefinition(),
            new ColumnDefinition(),
            new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) },
        ];

        RowDefinitions =
        [
            new RowDefinition(),
            new RowDefinition(),
            new RowDefinition(),
            new RowDefinition(),
            new RowDefinition(),
            new RowDefinition(),
        ];
    }

#region Bindables

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items),
        typeof(List<CalendarDayBoxView>),
        typeof(CalendarMonthGridView),
        propertyChanged: OnItemsChanged);

    #endregion

    #region Properties

    public List<CalendarDayBoxView> Items
    {
        get => (List<CalendarDayBoxView>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    #endregion

    #region Methods

    private static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            var owner = bindable as CalendarMonthGridView;
            owner.CreateItems();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.GetErrorMessage());
        }
    }

    private void CreateItems()
    {
        try
        {
            Clear();
            foreach(CalendarDayBoxView item in Items)
            {
                Children.Add(item);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
}
