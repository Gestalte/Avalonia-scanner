using ReactiveUI;
using System;

namespace AvaloniaApplication.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string scanResult = "";
    public string ScanResult
    {
        get => scanResult;
        set
        {
            if (value == "")
            {
                ShowResults = false;
            }
            else
            {
                ShowResults = true;
            }

            scanResult = this.RaiseAndSetIfChanged(ref scanResult, value);
        }
    }

    private bool showResults;
    public bool ShowResults
    {
        get => showResults;
        set => showResults = this.RaiseAndSetIfChanged(ref showResults, value);
    }

    public void ScanAgain()
    {
        ShowResults = false;
        ScanResult = "";
    }

    public event Action? TorchToggled;

    public void ToggleTorch()
    {
        TorchToggled?.Invoke();
    }
}
