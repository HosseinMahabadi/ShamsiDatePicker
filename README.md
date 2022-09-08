![ShamsiDatePickerIcon](https://user-images.githubusercontent.com/76768870/188611980-a9c6a0f6-f7bf-4224-a48b-c09fef5b837d.png)

[![NuGet](https://img.shields.io/nuget/v/ShamsiDatePicker.svg)](https://www.nuget.org/packages/ShamsiDatePicker/) 
[![NuGet](https://img.shields.io/nuget/dt/ShamsiDatePicker.svg)](https://www.nuget.org/packages/ShamsiDatePicker/)

# تقویم فارسی برای زامارین فرمز
#### این کنترل سفارشی برای برنامه نویسان زامارین فرمز تدارک دیده شده است و تاریخ هجری شمسی را شبیه کنترل DatePicker ماکروسافت، نمایش می دهد.
#### این کنترل با پلتفرم های اندروید و آی او اس سازگار است.
#### این کنترل از پترن MVVM پشتیبانی می کند.

![sdp1](https://user-images.githubusercontent.com/76768870/188799594-599b7007-36ce-49ea-b086-a06c6692cce2.jpg)
![sdp2](https://user-images.githubusercontent.com/76768870/188799712-d23e72fb-634b-494f-a204-6811dedc3b53.jpg)

#  ساختار کلاس
```csharp
public class ShamsiDatePicker : KeyboardlessEntry, IDisposable
```
# روش نصب
- `کنترل تقویم فارسی برای زامارین` در سایت ناگت قابل دسترسی است : https://www.nuget.org/packages/ShamsiDatePicker
- #### نصب از طریق ویژوال استودیو
	- Tools -> NuGet Pckage Manager -> Package Manager Console -> Install-Package ShamsiDatePicker -Version 3.0.20

# روش استفاده
- #### پس از نصب پکیج، کد زیر را در سرفایل پیج مورد نظر استفاده کنید: 
```xaml
xmlns:sdp="clr-namespace:ShamsiDatePicker;assembly=ShamsiDatePicker" 
```

- #### سپس در محتوای صفحه به شکل زیر کنترل را اضافه کنید:
```xaml
<sdp:ShamsiDatePicker Date="{Binding TargetDate, Mode=TwoWay}" />
```

# خصوصیات
| Property name                    | Type         | Default value  | Bindable | Description                 |
| ----------------------------- | ----------- | --------------- | ---------- | ------------------------------------------ |
| Date | DateTime | DateTime.Now | ✅ | تاریخ انتخاب شده به هجری شمسی را  به میلادی ترجمه و در خود ذخیره می کند. تاریخ را از میلادی  به شمسی ترجمه کرده و در تقویم نمایش می دهد. |
| MinimumShamsiYear | int | 1300 | ✅ | کمترین سال شمسی قابل انتخاب از تقویم |
| MaximumShamsiYear | int | 1500 | ✅ | بیشترین سال شمسی قابل انتخاب از تقویم |
| HeaderBackgroundColor | Xamarin.Forms.Color | Color.FromHex("#FF4081") | ✅ | رنگ سربرگ تقویم |
| HeaderTitleTextColor | Xamarin.Forms.Color | Color.White | ✅ | رنگ متن عنوان سربرگ تقویم |
| HeaderSubTitleTextColor | Xamarin.Forms.Color | Color.White | ✅ | رنگ متن زیر عنوان سربرگ تقویم |
| CalendarBackgroundColor| Xamarin.Forms.Color | Color.White | ✅ | رنگ صفحه تقویم |
| CalendarTitleColor | Xamarin.Forms.Color | Color.Black | ✅ | رنگ متن عنوان صفحه تقویم |
| CalendarSubTitleColor | Xamarin.Forms.Color | Color.Black | ✅ | رنگ متن زیر عنوان صفحه تقویم |
| CalendarTextColor | Xamarin.Forms.Color | Color.Black | ✅ | رنگ متن اعداد صفحه تقویم |
| CalendarSelectedTextColor | Xamarin.Forms.Color | Color.White | ✅ | رنگ متن روز انتخاب شده صفحه تقویم |
| CalendarHighlightColor | Xamarin.Forms.Color | Color.FromHex("#FF4081") | ✅ | رنگ هایلایت روز انتخاب شده صفحه تقویم |
| CalendarOKButtonTextColor | Xamarin.Forms.Color | Color.FromHex("#FF4081") | ✅ | رنگ متن کلید انتخاب تقویم |
| CalendarOKButtonBackgroundColor | Xamarin.Forms.Color | Color.Transparent | ✅ | رنگ پس زمینه کلید انتخاب تقویم |
| CalendarCancelButtonTextColor | Xamarin.Forms.Color | Color.FromHex("#FF4081") | ✅ | رنگ پس زمینه کلید انتخاب تقویم |
| CalendarCancelButtonBackgroundColor | Xamarin.Forms.Color | Color.Transparent | ✅ | رنگ پس زمینه کلید انصراف تقویم |
| CalendarCancelButtonBackgroundColor | Xamarin.Forms.Color | Color.Transparent | ✅ | رنگ پس زمینه کلید انصراف تقویم |
| BorderColor | Xamarin.Forms.Color | Color.Black | ✅ | رنگ حاشیه جعبه متن تاریخ |
| BorderThickness | double | 1d | ✅ | اندازه حاشیه جعبه متن تاریخ |
| CornerRadius | int | 0 | ✅ | میزان انحنای لبه های جعبه متن تاریخ |
| Padding | Xamarin.FormsThickness | 5 | ✅ | میزان فاصله داخلی جعبه متن تاریخ |
| RenderMode | RenderModeType | RenderModeType.Standard |  | با تغییر این خصیصه می توانید بین حالت جعبه متن پیش فرض زامارین و جعبه متن استاندارد سوییچ کنید. چهار خصیصه قبلی فقط در حالت استاندارد فعال هستند. |

# تماس با من
- [Email: hossein.mahabadi@gmail.com](mailto:hossein.mahabadi@gmail.com)
- [Telegram](https://t.me/hossein_mahabadi)
- [Instagram](https://instagram.com/hossein.mahabadi468)