﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ShamsiDatePicker.ViewModel;
using HMExtension.Xamarin;

namespace ShamsiDatePicker
{
    internal static class Globals
    {
        public static int Position = 0;

        public static string AppUniqId = Guid.NewGuid().ToString();
        public static Dictionary<MessageType, string> Messages = new Dictionary<MessageType, string>()
        {
            { MessageType.NewDayIsSelected, "NewDayIsSelected" + AppUniqId},
            { MessageType.MinYearIsChanged, "MinYearIsChanged" + AppUniqId},
            { MessageType.MaxYearIsChenged, "MaxYearIsChenged" + AppUniqId},
            { MessageType.HeaderBackgroundColorIsChanged, "HeaderBackgroundColorIsChanged" + AppUniqId},
            { MessageType.HeaderTitleTextColorIsChanged, "HeaderTitleTextColorIsChanged" + AppUniqId},
            { MessageType.HeaderSubTitleTextColorIsChanged, "HeaderSubTitleTextColorIsChanged" + AppUniqId},
            { MessageType.CalendarBackgroundColorIsChanged, "CalendarBackgroundColorIsChanged" + AppUniqId},
            { MessageType.CalendarTextColorIsChanged, "CalendarTextColorIsChanged" + AppUniqId},
            { MessageType.CalendarSelectedTextColorIsChanged, "CalendarSelectedTextColorIsChanged" + AppUniqId},
            { MessageType.CalendarHighlightColorIsChanged, "CalendarHighlightColorIsChanged" + AppUniqId},
            { MessageType.CalendarTitleColorIsChanged, "CalendarTitleColorIsChanged" + AppUniqId},
            { MessageType.CalendarSubTitleColorIsChanged, "CalendarSubTitleColorIsChanged" + AppUniqId},
            { MessageType.CalendarOKButtonTextColorIsChanged, "CalendarOKButtonTextColorIsChanged" + AppUniqId},
            { MessageType.CalendarOKButtonBackgroundColorIsChanged, "CalendarOKButtonBackgroundColorIsChanged" + AppUniqId},
            { MessageType.CalendarCancelButtonTextColorIsChanged, "CalendarCancelButtonTextColorIsChanged" + AppUniqId},
            { MessageType.CalendarCancelButtonBackgroundColorIsChanged, "CalendarCancelButtonBackgroundColorIsChanged" + AppUniqId},
            { MessageType.DateIsChanged, "DateIsChanged" + AppUniqId},
            { MessageType.OkButtonClicked, "OkButtonClicked" + AppUniqId},
            { MessageType.CancelButtonClicked, "CancelButtonClicked" + AppUniqId},
        };
    }
}
