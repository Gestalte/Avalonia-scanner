using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace AvaloniaApplication
{
    public interface IPermissionService
    {
        public Task<bool> CheckPermission<T>() where T : BasePermission, new();
        public Task<bool> RequestPermission<T>() where T : BasePermission, new();
    }

    public interface IToastService
    {
        public void ShowToastShort(string text);
        public void ShowToastLong(string text);
    }

    public static class Services
    {
        public static IPermissionService PermissionService { get; set; } = new DefaultPermissionService();
        public static IToastService ToastService { get; set; } = new DefaultToastService();
    }

    public class DefaultToastService : IToastService
    {
        public void ShowToastLong(string text)
        {
            System.Diagnostics.Debug.WriteLine($"{ShowToastLong} text: {text}", "[TRACE]");
        }

        public void ShowToastShort(string text)
        {
            System.Diagnostics.Debug.WriteLine($"{ShowToastShort} text: {text}", "[TRACE]");
        }
    }

    public class DefaultPermissionService : IPermissionService
    {
        public Task<bool> CheckPermission<T>() where T : BasePermission, new()
        {
            return Task.FromResult(false);
        }

        public Task<bool> RequestPermission<T>() where T : BasePermission, new()
        {
            return Task.FromResult(false);
        }
    }
}
