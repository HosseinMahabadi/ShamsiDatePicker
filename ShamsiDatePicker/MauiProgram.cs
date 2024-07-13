
using HMControls;
using System.Reflection;

[assembly: ExportFont("bnazanin.ttf", Alias = "B_Nazanin")]
[assembly: ExportFont("bnazanin_bold.ttf", Alias = "B_Nazanin_Bold")]

namespace ShamsiDatePicker;

public static class MauiProgram
{
    public static MauiAppBuilder UseShamsiDatePicker(this MauiAppBuilder builder)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        builder
            .UseHMControls()
            .ConfigureFonts(fonts =>
            {
                fonts.AddEmbeddedResourceFont(assembly, "bnazanin.ttf", "B_Nazanin");
                fonts.AddEmbeddedResourceFont(assembly, "bnazanin_bold.ttf", "B_Nazanin_Bold");
            });

        return builder;
    }
}
