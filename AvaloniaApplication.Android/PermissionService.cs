using Microsoft.Maui.ApplicationModel;
using System.Diagnostics;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace AvaloniaApplication.Android;

public class AndroidPermissionService : IPermissionService
{
    public async Task<bool> CheckPermission<T>() where T : BasePermission, new()
    {
        long ts = Stopwatch.GetTimestamp();
        System.Diagnostics.Debug.WriteLine($"{nameof(AndroidPermissionService)} {nameof(CheckPermission)}", "[TRACE]");
        var result = await CheckStatusAsync<T>();
        System.Diagnostics.Debug.WriteLine($"{nameof(AndroidPermissionService)} {nameof(CheckPermission)} result: {result} Elapsed Time: {Stopwatch.GetElapsedTime(ts)}", "[INFO]");

        return result == PermissionStatus.Granted;
    }

    public async Task<bool> RequestPermission<T>() where T : BasePermission, new()
    {
        long ts = Stopwatch.GetTimestamp();
        System.Diagnostics.Debug.WriteLine($"{nameof(AndroidPermissionService)} {nameof(RequestPermission)}", "[TRACE]");
        var result = await RequestAsync<T>();
        System.Diagnostics.Debug.WriteLine($"{nameof(AndroidPermissionService)} {nameof(RequestPermission)} result: {result} Elapsed Time: {Stopwatch.GetElapsedTime(ts)}", "[INFO]");

        return result == PermissionStatus.Granted;
    }
}