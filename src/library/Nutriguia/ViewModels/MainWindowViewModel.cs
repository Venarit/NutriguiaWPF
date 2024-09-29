using System.Collections.ObjectModel;
using Wpf.Ui.Controls;
using Wpf.Ui;

namespace Nutriguia.ViewModels.Windows;

public partial class MainWindowViewModel : ViewModel
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private string applicationTitle = string.Empty;

    [ObservableProperty]
    private ObservableCollection<object> navigationItems = [];

    [ObservableProperty]
    private ObservableCollection<object> navigationFooter = [];

    [ObservableProperty]
    private ObservableCollection<System.Windows.Controls.MenuItem> trayMenuItems = [];

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Style",
        "IDE0060:Remove unused parameter",
        Justification = "Testing"
    )]
    public MainWindowViewModel(INavigationService navigationService)
    {
        if (!_isInitialized)
        {
            InitializeViewModel();
        }
    }

    private void InitializeViewModel()
    {
        applicationTitle = "WPF UI - MVVM Demo";

        navigationItems =
        [
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.PatientsPage)
            },
            new NavigationViewItem()
            {
                Content = "Data",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.PatientsPage)
            },
        ];

        navigationFooter =
        [
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.PatientsPage)
            },
        ];

        trayMenuItems = [new() { Header = "Home", Tag = "tray_home" }];

        _isInitialized = true;
    }
}
