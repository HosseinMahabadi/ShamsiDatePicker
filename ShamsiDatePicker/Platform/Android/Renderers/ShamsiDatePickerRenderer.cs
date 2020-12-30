using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using ShamsiDatePicker;
using ShamsiDatePicker.Platform.Android.Renderers;
using Xamarin.Forms;
using Android.Runtime;
using Android.Views;

[assembly: ExportRenderer(typeof(ShamsiDatePicker.ShamsiDatePicker), typeof(ShamsiDatePickerRenderer))]
namespace ShamsiDatePicker.Platform.Android.Renderers
{
    class ShamsiDatePickerRenderer : EntryRenderer
    {
        public ShamsiDatePickerRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
        }
        protected override void OnFocusChanged(bool gainFocus, [GeneratedEnum] FocusSearchDirection direction, global::Android.Graphics.Rect previouslyFocusedRect)
        {
            base.OnFocusChanged(gainFocus, direction, previouslyFocusedRect);
        }
    }
}
