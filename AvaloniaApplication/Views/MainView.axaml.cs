using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Maui.Controls;
using Avalonia.Threading;
using AvaloniaApplication.ViewModels;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace AvaloniaApplication.Views;

public partial class MainView : UserControl
{
    private CameraBarcodeReaderView? cameraBarcodeReaderView;

    public MainView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        this.cameraBarcodeReaderView = (CameraBarcodeReaderView)this.Get<MauiControlHost>("cameraBarcodeReaderHost").Content!;
        this.cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            Multiple = true,
            TryHarder = true,
            TryInverted = true,
        };

        if (DataContext is MainViewModel vm)
        {
            vm.TorchToggled += Vm_TorchToggled;
        }

        base.OnLoaded(e);
    }

    bool torchState = false;
    private void Vm_TorchToggled()
    {
        torchState = !torchState;
        if (this.cameraBarcodeReaderView is not null)
        {
            this.cameraBarcodeReaderView.IsTorchOn = torchState;
        }
    }

    private void BarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine(nameof(BarcodesDetected));

        Dispatcher.UIThread.Post(() =>
        {
            if (e.Results.Length == 1)
            {
                if (DataContext is MainViewModel vm)
                {
                    vm.ScanResult = e.Results[0].Value;
                    System.Diagnostics.Debug.WriteLine($"{nameof(BarcodesDetected)} {e.Results[0].Format} --> {e.Results[0].Value}", "[TRACE]");
                }
            }
        });
    }
}
