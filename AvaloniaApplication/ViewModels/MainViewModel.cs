using ReactiveUI;

namespace AvaloniaApplication.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string scanResult = "";
    public string ScanResult
    {
        get => scanResult;
        set => scanResult = this.RaiseAndSetIfChanged(ref scanResult, value);
    }

    public void StartScanning()
    {

    }
}
