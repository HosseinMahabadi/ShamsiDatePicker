<img src="ShamsiDatePicker\Resources\Images\ShamsiDatePickerIcon.png?raw=true" width="128">

[![NuGet](https://img.shields.io/nuget/v/ShamsiDatePicker.svg)](https://www.nuget.org/packages/ShamsiDatePicker/) 
[![NuGet](https://img.shields.io/nuget/dt/ShamsiDatePicker.svg)](https://www.nuget.org/packages/ShamsiDatePicker/)

# تقویم فارسی برای زامارین فرمز
این کنترل سفارشی برای برنامه نویسان زامارین فرمز تدارک دیده شده است و تاریخ هجری شمسی را شبیه کنترل DatePicker ماکروسافت، نمایش می دهد.
این کنترل در با پلتفرم های اندروید و آی او اس سازگار است.
این کنترل از پترن MVVM پشتیبانی می کند.

<br/>
<p float="right">
<img alt="ShamsiDatePicker calendar page" src="ShamsiDatePicker\Resources\Images\sdp1.jpg?raw=true" width="200" />
<img alt="ShamsiDatePicker year page" src="ShamsiDatePicker\Resources\Images\sdp2.jpg?raw=true" width="200" />
</p>

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
                      MinimumShamsiYear="1320"
                      MaximumShamsiYear="1420"
                      RenderMode="Default"/>
```