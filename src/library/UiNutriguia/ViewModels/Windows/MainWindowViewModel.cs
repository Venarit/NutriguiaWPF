using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace UiNutriguia.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
            new NavigationViewItem()
            {
                Content = "Calendario",
                Icon = new SymbolIcon { Symbol = SymbolRegular.CalendarLtr28 },
                TargetPageType = typeof(Views.Pages.SchedulerPage)
            },
            new NavigationViewItem()
            {
                Content = "Pacientes",
                Icon = new SymbolIcon { Symbol = SymbolRegular.People24 },
                TargetPageType = typeof(Views.Pages.PatientsPage)
            },
            new NavigationViewItem()
            {
                Content = "Menús",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Food16 },
                TargetPageType = typeof(Views.Pages.MenuPage)
            },
            new NavigationViewItem()
            {
                Content = "Planes",
                Icon = new SymbolIcon { Symbol = SymbolRegular.ClipboardNote20 },
                TargetPageType = typeof(Views.Pages.PlansPage)
            },
            new NavigationViewItem()
            {
                Content = "Platillos",
                Icon = new SymbolIcon { Symbol = SymbolRegular.BowlSalad20 },
                TargetPageType = typeof(Views.Pages.DishesPage)
            },
            new NavigationViewItem()
            {
                Content = "Alimentos",
                Icon = new SymbolIcon { Symbol = SymbolRegular.FoodApple20 },
                TargetPageType = typeof(Views.Pages.FoodPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
