using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using Avalonia.Maui;
using Avalonia.ReactiveUI;
using AvaloniaApplication.Maui;
using ZXing.Net.Maui.Controls;

namespace AvaloniaApplication.Android;

[Activity(
    Label = "Scanner Test",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        Services.PermissionService = new AndroidPermissionService();
        Services.ToastService = new ToastService(ApplicationContext!);

        return base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI()
            .UseMaui<MauiApplication>(this, b => b.UseBarcodeReader());
    }
}
