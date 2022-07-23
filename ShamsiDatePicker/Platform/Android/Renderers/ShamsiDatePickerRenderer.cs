using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Views;
using Android.Content;
using ShamsiDatePicker.Platform.Android.Renderers;
using Android.Runtime;
using Android.Views.InputMethods;
using System;
using System.Diagnostics;
using Android.Graphics.Drawables;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(ShamsiDatePicker.ShamsiDatePicker), typeof(ShamsiDatePickerRenderer))]

namespace ShamsiDatePicker.Platform.Android.Renderers
{
    class ShamsiDatePickerRenderer : EntryRenderer
    {
        public ShamsiDatePicker MainElement => Element as ShamsiDatePicker;

        public ShamsiDatePickerRenderer(Context context) : base(context)
        { }
        protected override FormsEditText CreateNativeControl()
        {
            var control = base.CreateNativeControl();
            if (MainElement.RenderMode == ViewModel.RenderModeType.Standard)
            {
                UpdateBackground(control);
            }
            return control;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            var MainElement = (ShamsiDatePicker)Element;

            try
            {
                if (e.NewElement != null)
                {
                    MainElement.PropertyChanging += OnPropertyChanging;
                }

                if (e.OldElement != null)
                {
                    MainElement.PropertyChanging -= OnPropertyChanging;
                }

                // Disable the Keyboard on Focus
                Control.ShowSoftInputOnFocus = false;
                Control.SetCursorVisible(false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ShamsiDatePicker --> " + ex.TargetSite + " " + ex.Message);
            }
        }

        private void OnPropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        {
            try
            {
                // Check if the view is about to get Focus
                if (e.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
                {
                    // incase if the focus was moved from another Entry
                    // Forcefully dismiss the Keyboard 
                    InputMethodManager imm = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(Control.WindowToken, HideSoftInputFlags.None);
                }

                if (e.PropertyName == Entry.IsPasswordProperty.PropertyName)
                {
                    MainElement.IsPassword = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ShamsiDatePicker --> " + ex.TargetSite + " " + ex.Message);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (MainElement.RenderMode == ViewModel.RenderModeType.Standard)
            {
                if (e.PropertyName == ShamsiDatePicker.CornerRadiusProperty.PropertyName)
                {
                    UpdateBackground();
                }
                else if (e.PropertyName == ShamsiDatePicker.BorderThicknessProperty.PropertyName)
                {
                    UpdateBackground();
                }
                else if (e.PropertyName == ShamsiDatePicker.BorderColorProperty.PropertyName)
                {
                    UpdateBackground();
                }
            }
            base.OnElementPropertyChanged(sender, e);
        }

        protected void UpdateBackground(FormsEditText control)
        {
            if (control == null) return;

            var gd = new GradientDrawable();
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(Context.ToPixels(MainElement.CornerRadius));
            gd.SetStroke((int)Context.ToPixels(MainElement.BorderThickness), MainElement.BorderColor.ToAndroid());
            control.SetBackground(gd);

            var padTop = (int)Context.ToPixels(MainElement.Padding.Top);
            var padBottom = (int)Context.ToPixels(MainElement.Padding.Bottom);
            var padLeft = (int)Context.ToPixels(MainElement.Padding.Left);
            var padRight = (int)Context.ToPixels(MainElement.Padding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }

        protected override void UpdateBackgroundColor()
        {
            if (MainElement.RenderMode == ViewModel.RenderModeType.Standard)
            {
                UpdateBackground();
            }
        }

        protected override void UpdateBackground()
        {
            UpdateBackground(Control);
        }

        protected override void Dispose(bool disposing) 
        {
            MainElement.PropertyChanging -= OnPropertyChanging;
            base.Dispose(disposing);
        }
    }
}
