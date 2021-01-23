using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using ShamsiDatePicker.Platform.ios.Renderers;

[assembly: ExportRenderer(typeof(ShamsiDatePicker.ShamsiDatePicker), typeof(ShamsiDatePickerRenderer))]
namespace ShamsiDatePicker.Platform.ios.Renderers
{
    class ShamsiDatePickerRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            // Disabling the keyboard
            this.Control.InputView = new UIView();
        }
    }
}