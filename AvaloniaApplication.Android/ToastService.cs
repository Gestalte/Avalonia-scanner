using Android.Content;
using Android.Widget;

namespace AvaloniaApplication.Android;

public class ToastService(Context context) : IToastService
{
    private readonly Context context = context;

    public void ShowToastLong(string text)
    {
        System.Diagnostics.Debug.WriteLine($"{nameof(ToastService)}.{nameof(ShowToastLong)} text: {text}");
        var toast = Toast.MakeText(this.context, text, ToastLength.Long);
        toast?.Show();
    }

    public void ShowToastShort(string text)
    {
        System.Diagnostics.Debug.WriteLine($"{nameof(ToastService)}.{nameof(ShowToastShort)} text: {text}");
        var toast = Toast.MakeText(this.context, text, ToastLength.Short);
        toast?.Show();
    }
}
