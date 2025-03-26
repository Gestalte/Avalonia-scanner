using Avalonia.Controls;
using Avalonia.Threading;
using DynamicData;
using Microsoft.Maui.ApplicationModel;
using ReactiveUI;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaApplication.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {

    }

    private ScannerView? scanner;
    public ScannerView? Scanner
    {
        get => scanner;
        set => scanner = this.RaiseAndSetIfChanged(ref scanner, value);
    }

    private bool showScanner;
    public bool ShowScanner
    {
        get => showScanner;
        set => showScanner = this.RaiseAndSetIfChanged(ref showScanner, value);
    }

    private string scanResult = "";
    public string ScanResult
    {
        get => scanResult;
        set
        {
            if (value != "")
            {
                ShowScanner = false;
                ShowResult = true;
            }
            else
            {
                ShowResult = false;
            }

            scanResult = this.RaiseAndSetIfChanged(ref scanResult, value);
        }
    }

    private bool showResult;
    public bool ShowResult
    {
        get => showResult;
        set => showResult = this.RaiseAndSetIfChanged(ref showResult, value);
    }

    public async Task ScanCommand()
    {
        System.Diagnostics.Debug.WriteLine(nameof(ScanCommand), "[TRACE]");

        ScanResult = "";
        ShowResult = false;

        bool hasPermission = await Services.PermissionService.CheckPermission<Permissions.Camera>();

        if (!hasPermission)
        {
            //bool gotPermission = await Services.PermissionService.RequestPermission<Permissions.Camera>();

            //if (gotPermission)
            //{
            //    hasPermission = true;
            //}

            try
            {
                using var ctsRequest = new CancellationTokenSource(TimeSpan.FromSeconds(1));
                var token = ctsRequest.Token;

                Task<bool> res = Services.PermissionService.RequestPermission<Permissions.Camera>();

                bool gotPermission = await res.WaitAsync(token);

                if (gotPermission)
                {
                    hasPermission = true;
                }
            }
            catch (OperationCanceledException oce)
            {
                System.Diagnostics.Debug.WriteLine(oce, "[ERROR]");
                // Request is shown to user but never returns a result, move on and poll with CheckPermission.
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex, "[ERROR]");
                throw;
            }

            using var ctsPollTimeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            // HACK: If permission has been given start scanning, otherwise wait a bit, after 5 seconds assume it was denied.
            while (!ctsPollTimeout.IsCancellationRequested)
            {
                bool result = await Services.PermissionService.CheckPermission<Permissions.Camera>();

                if (result)
                {
                    hasPermission = true;
                    break;
                }
            }
        }

        if (hasPermission)
        {
            Scanner ??= new ScannerView(new ScannerViewModel(this));
            ShowScanner = true;
            Scanner.StartDetecting();
        }
        else
        {
            Services.ToastService.ShowToastLong("ERROR: Camera permission not granted");
        }
    }
}
