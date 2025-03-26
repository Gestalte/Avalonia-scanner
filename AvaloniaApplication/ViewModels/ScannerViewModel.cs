using ReactiveUI;
using System;

namespace AvaloniaApplication.ViewModels
{
    public class ScannerViewModel : ViewModelBase
    {
        public ScannerViewModel()
        {
            this.mainViewModel = null!;
        }

        public ScannerViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        private readonly MainViewModel mainViewModel;

        private bool showResults;
        public bool ShowResults
        {
            get => showResults;
            set => showResults = this.RaiseAndSetIfChanged(ref showResults, value);
        }

        public event Action? TorchToggled;
        public void ToggleTorch()
        {
            TorchToggled?.Invoke();
        }

        public event Action? CameraLocationToggled;
        public void ToggleCameraLocation()
        {
            CameraLocationToggled?.Invoke();
        }

        public void CancelCommand()
        {
            this.mainViewModel.ShowScanner = false;
        }

        public void ReceiveScanResult(string scanResult)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(ReceiveScanResult)} scanResult: {scanResult}", "[TRACE]");

            this.mainViewModel.ScanResult = scanResult;
        }
    }
}
