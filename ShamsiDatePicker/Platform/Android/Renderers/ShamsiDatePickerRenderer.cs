using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Views;
using Android.Content;
using ShamsiDatePicker.Platform.Android.Renderers;
using Android.Runtime;
using Android.Views.InputMethods;

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

            if (e.NewElement != null)
            {
                ((ShamsiDatePicker)e.NewElement).PropertyChanging += OnPropertyChanging;
            }

            if (e.OldElement != null)
            {
                ((ShamsiDatePicker)e.OldElement).PropertyChanging -= OnPropertyChanging;
            }

            // Disable the Keyboard on Focus
            this.Control.ShowSoftInputOnFocus = false;
            this.Control.SetCursorVisible(false);
        }

        private void OnPropertyChanging(object sender, PropertyChangingEventArgs propertyChangingEventArgs)
        {
            // Check if the view is about to get Focus
            if (propertyChangingEventArgs.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
            {
                // incase if the focus was moved from another Entry
                // Forcefully dismiss the Keyboard 
                InputMethodManager imm = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(this.Control.WindowToken, 0);
            }
        }
    }
}
