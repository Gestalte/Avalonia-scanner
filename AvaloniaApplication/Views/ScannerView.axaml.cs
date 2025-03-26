using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Maui.Controls;
using Avalonia.Threading;
using AvaloniaApplication.ViewModels;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace AvaloniaApplication;

public partial class ScannerView : UserControl
{
    private CameraBarcodeReaderView? cameraBarcodeReaderView;

    public ScannerView()
    {
        InitializeComponent();
    }

    public ScannerView(ScannerViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"{nameof(ScannerView)}.{nameof(OnLoaded)}", "[TRACE]");

        this.cameraBarcodeReaderView = (CameraBarcodeReaderView)this.Get<MauiControlHost>("cameraBarcodeReaderHost").Content!;
        this.cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true,
            TryHarder = true,
            TryInverted = true,
        };

        if (DataContext is ScannerViewModel vm)
        {
            vm.TorchToggled += TorchToggled;
            vm.CameraLocationToggled += CameraLocationToggled;           
        }

        this.cameraBarcodeReaderView.IsDetecting = true;
        System.Diagnostics.Debug.WriteLine($"IsDetecting: {this.cameraBarcodeReaderView.IsDetecting}", "[INFO]");

        base.OnLoaded(e);
    }

    bool torchState = false;
    private void TorchToggled()
    {
        System.Diagnostics.Debug.WriteLine(nameof(TorchToggled), "[TRACE]");

        torchState = !torchState;
        if (this.cameraBarcodeReaderView is not null)
        {
            this.cameraBarcodeReaderView.IsTorchOn = torchState;
        }
    }

    CameraLocation cameraLocation = CameraLocation.Rear;
    private void CameraLocationToggled()
    {
        System.Diagnostics.Debug.WriteLine(nameof(CameraLocationToggled), "[TRACE]");

        this.cameraLocation = this.cameraLocation switch
        {
            CameraLocation.Rear => CameraLocation.Front,
            _ => CameraLocation.Rear,
        };

        if (this.cameraBarcodeReaderView is not null)
        {
            this.cameraBarcodeReaderView.CameraLocation = this.cameraLocation;
        }
    }

    public void StartDetecting()
    {
        if (this.cameraBarcodeReaderView is not null)
        {
            this.cameraBarcodeReaderView.IsDetecting = true;
            System.Diagnostics.Debug.WriteLine($"IsDetecting: {this.cameraBarcodeReaderView.IsDetecting}", "[INFO]");
        }
    }

    private void BarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
    {
        if (this.cameraBarcodeReaderView is not null)
        {
            this.cameraBarcodeReaderView.IsDetecting = false;
            System.Diagnostics.Debug.WriteLine($"IsDetecting: {this.cameraBarcodeReaderView.IsDetecting}", "[INFO]");
        }

        System.Diagnostics.Debug.WriteLine(nameof(BarcodesDetected), "[TRACE]");

        Dispatcher.UIThread.Post(() =>
        {
            if (e.Results.Length == 1 && DataContext is ScannerViewModel vm)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(BarcodesDetected)} {e.Results[0].Format} --> {e.Results[0].Value}", "[INFO]");

                vm.ReceiveScanResult(e.Results[0].Value);
            }
        });
    }
}