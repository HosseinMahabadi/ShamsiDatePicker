using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using ShamsiDatePicker.Platform.iOS.Renderers;
using System;
[assembly: ExportRenderer(typeof(ShamsiDatePicker.ShamsiDatePicker), typeof(ShamsiDatePickerRenderer))]

namespace ShamsiDatePicker.Platform.iOS.Renderers
{
    class ShamsiDatePickerRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            // Disabling the keyboard
            Control.InputView = new UIView();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}