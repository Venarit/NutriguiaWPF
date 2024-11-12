using UiNutriguia.Models;

namespace UiNutriguia.ViewModels.Pages;
public partial class DashboardViewModel : ObservableObject
{
    private bool _isInitialized = false;
    [ObservableProperty]
    private string _greeting = "Hola!";

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();

        UpdateGreeting();
    }

    public void OnNavigatedFrom()
    {
        _isInitialized = false;
    }

    public void InitializeViewModel()
    {
        _isInitialized = true;
        UpdateGreeting();
    }

    public void UpdateGreeting()
    {
        var currentHour = DateTime.Now.Hour;
        if (currentHour < 12)
        {
            Greeting = "Buenos días";
        }
        else if (currentHour < 18)
        {
            Greeting = "Buenas tardes";
        }
        else
        {
            Greeting = "Buenas noches";
        }
    }
}
