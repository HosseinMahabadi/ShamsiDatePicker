using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ShamsiDatePicker.ViewModel;

namespace ShamsiDatePicker
{
    internal static class Globals
    {
        public static Dictionary<MessageType, string> Messages = new Dictionary<MessageType, string>()
        {
            { MessageType.NewDayIsSelected, "NewDayIsSelected"},
            { MessageType.MinYearIsChanged, "MinYearIsChanged"},
            { MessageType.MaxYearIsChenged, "MaxYearIsChenged"},
            { MessageType.HeaderBackgroundColorIsChanged, "HeaderBackgroundColorIsChanged"},
            { MessageType.HeaderTitleTextColorIsChanged, "HeaderTitleTextColorIsChanged"},
            { MessageType.HeaderSubTitleTextColorIsChanged, "HeaderSubTitleTextColorIsChanged"},
            { MessageType.CalendarBackgroundColorIsChanged, "CalendarBackgroundColorIsChanged"},
            { MessageType.CalendarTextColorIsChanged, "CalendarTextColorIsChanged"},
            { MessageType.CalendarSelectedTextColorIsChanged, "CalendarSelectedTextColorIsChanged"},
            { MessageType.CalendarHighlightColorIsChanged, "CalendarHighlightColorIsChanged"},
            { MessageType.CalendarTitleColorIsChanged, "CalendarTitleColorIsChanged"},
            { MessageType.CalendarSubTitleColorIsChanged, "CalendarSubTitleColorIsChanged"},
            { MessageType.CalendarOKButtonTextColorIsChanged, "CalendarOKButtonTextColorIsChanged"},
            { MessageType.CalendarOKButtonBackgroundColorIsChanged, "CalendarOKButtonBackgroundColorIsChanged"},
            { MessageType.CalendarCancelButtonTextColorIsChanged, "CalendarCancelButtonTextColorIsChanged"},
            { MessageType.CalendarCancelButtonBackgroundColorIsChanged, "CalendarCancelButtonBackgroundColorIsChanged"},
            { MessageType.DateIsChanged, "DateIsChanged"},
            { MessageType.OkButtonClicked, "OkButtonClicked"},
            { MessageType.CancelButtonClicked, "CancelButtonClicked"},
        };
    }
}
