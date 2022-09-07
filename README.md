![ShamsiDatePickerIcon](https://user-images.githubusercontent.com/76768870/188611980-a9c6a0f6-f7bf-4224-a48b-c09fef5b837d.png)

[![NuGet](https://img.shields.io/nuget/v/ShamsiDatePicker.svg)](https://www.nuget.org/packages/ShamsiDatePicker/) 
[![NuGet](https://img.shields.io/nuget/dt/ShamsiDatePicker.svg)](https://www.nuget.org/packages/ShamsiDatePicker/)

# تقویم فارسی برای زامارین فرمز
#### این کنترل سفارشی برای برنامه نویسان زامارین فرمز تدارک دیده شده است و تاریخ هجری شمسی را شبیه کنترل DatePicker ماکروسافت، نمایش می دهد.
#### این کنترل با پلتفرم های اندروید و آی او اس سازگار است
#### این کنترل از پترن MVVM پشتیبانی می کند.


![sdp1](https://user-images.githubusercontent.com/76768870/188799594-599b7007-36ce-49ea-b086-a06c6692cce2.jpg)
![sdp2](https://user-images.githubusercontent.com/76768870/188799712-d23e72fb-634b-494f-a204-6811dedc3b53.jpg)


# روش نصب
- `کنترل تقویم فارسی برای زامارین` در سایت ناگت قابل دسترسی است : https://www.nuget.org/packages/ShamsiDatePicker
- #### نصب از طریق ویژوال استودیو
	- Tools -> NuGet Pckage Manager -> Package Manager Console -> Install-Package ShamsiDatePicker -Version 3.0.20

# روش استفاده
- #### پس از نصب پکیج، کد زیر را در سرفایل صفحه پیج مورد نظر استفاده کنید: 
```xaml
xmlns:sdp="clr-namespace:ShamsiDatePicker;assembly=ShamsiDatePicker" 
```

- #### سپس در محتوای صفحه به شکل زیر کنترل را اضافه کنید:
```xaml
<sdp:ShamsiDatePicker Date="{Binding TargetDate, Mode=TwoWay}"
                      RenderMode="Default"/> <!-- RenderMode به شما اجازه می دهد تا حالت پیش فرض زامارین فرمزیا حالت استاندارد را برای نمایش جعبه متن تاریخ استفاده کنید-->
```

- #### می توانید خصوصیات تقویم را به شکل زیر مقدار دهی کنید:
```xaml
<ContentPage.Resources>
    <Style TargetType="sdp:ShamsiDatePicker">

        <!-- کمترین سال شمسی قابل انتخاب از تقویم -->
        <Setter Property="MinimumShamsiYear" Value="1320"/>

        <!-- بیشترین سال شمسی قابل انتخاب از تقویم -->
        <Setter Property="MaximumShamsiYear" Value="1420"/>
            
        <!-- رنگ سربرگ تقویم -->
        <Setter Property="HeaderBackgroundColor" Value="SteelBlue"/>
            
        <!-- رنگ متن عنوان سربرگ تقویم -->
        <Setter Property="HeaderTitleTextColor" Value="Gold"/>
            
        <!-- رنگ متن زیر عنوان سربرگ تقویم -->
        <Setter Property="HeaderSubTitleTextColor" Value="LightYellow"/>
            
        <!-- رنگ صفحه تقویم تقویم -->
        <Setter Property="CalendarBackgroundColor" Value="WhiteSmoke"/>
            
        <!-- رنگ متن عنوان صفحه تقویم -->
        <Setter Property="CalendarTitleColor" Value="DarkGoldenrod"/>
            
        <!-- رنگ متن زیر عنوان صفحه تقویم -->
        <Setter Property="CalendarSubTitleColor" Value="DarkBlue"/>
            
        <!-- رنگ متن اعداد صفحه تقویم -->
        <Setter Property="CalendarTextColor" Value="BlueViolet"/>
            
        <!-- رنگ متن روز انتخاب شده صفحه تقویم -->
        <Setter Property="CalendarSelectedTextColor" Value="Cyan"/>
            
        <!-- رنگ هایلایت روز انتخاب شده صفحه تقویم -->
        <Setter Property="CalendarHighlightColor" Value="SteelBlue"/>
            
        <!-- رنگ متن کلید انتخاب تقویم -->
        <Setter Property="CalendarOKButtonTextColor" Value="ForestGreen"/>
            
        <!-- رنگ پس زمینه کلید انتخاب تقویم -->
        <Setter Property="CalendarOKButtonBackgroundColor" Value="LightBlue"/>
            
        <!-- رنگ متن کلید انصراف تقویم -->
        <Setter Property="CalendarCancelButtonTextColor" Value="Red"/>
            
        <!-- رنگ پس زمینه کلید انصراف تقویم -->
        <Setter Property="CalendarCancelButtonBackgroundColor" Value="LightBlue"/>
    </Style>
</ContentPage.Resources>
```
