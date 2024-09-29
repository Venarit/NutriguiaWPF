using Nutriguia.Controls;
using Wpf.Ui.Controls;
using Wpf.Ui;
using Nutriguia.ViewModels.Windows;

namespace Nutriguia.Views;

/// <summary>
/// Lógica de interacción para MainWindow.xaml
/// </summary>
public partial class MainWindow : INavigationWindow
{
    public MainWindowViewModel ViewModel { get; }

    public MainWindow(MainWindowViewModel viewModel, INavigationService navigationService)
    {
        ViewModel = viewModel;
        //DataContext = this;

        //Wpf.Ui.Appearance.SystemThemeWatcher.Watch(this);

        //InitializeComponent();
        ShowLoginDialog();

        //navigationService.SetNavigationControl(RootNavigation);
    }

    //public INavigationView GetNavigation() => RootNavigation;

    //public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

    public void SetPageService(IPageService pageService) { }

    public void ShowWindow() => Show();

    public void CloseWindow() => Close();

    /*protected override void OnClosed(EventArgs e)
    {
        //base.OnClosed(e);

        // Make sure that closing this window will begin the process of closing the application.
        Application.Current.Shutdown();
    }*/

    INavigationView INavigationWindow.GetNavigation()
    {
        throw new NotImplementedException();
    }

    public void SetServiceProvider(IServiceProvider serviceProvider)
    {
        throw new NotImplementedException();
    }

    private async Task ShowLoginDialog()
    {
        try
        {
            Login loginDialog = new Login();

            bool? result = loginDialog.ShowDialog();

            if (result == true)
            {
                //TODO: User Login logic
            }
            else 
            {
                Application.Current.Shutdown();
            }
        }
        catch (Exception ex)
        { 
            Console.WriteLine(ex.Message);
        }
        
    }


}
