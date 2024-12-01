using Wpf.Ui.Controls;
using Wpf.Ui;
using Nutriguia.Model.DataAccess;
using UiNutriguia.Models;

namespace UiNutriguia.ViewModels.Pages;

public partial class PatientProfileViewModel(INavigationService navigationService, IContentDialogService dialogService) : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private DataAccess dataAccess;

    [ObservableProperty] private PatientModel _patient;

    [RelayCommand]
    private void GotoPage(Type type)
    {
        _ = navigationService.Navigate(type);
    }

    public void OnNavigatedTo()
    {
        if (NavigationContext.Parameter is PatientModel model)
        {
            Patient = model;
            NavigationContext.Parameter = null;
        }

        if (!_isInitialized)
            InitializeViewModel();
    }

    public void OnNavigatedFrom()
    {
        _isInitialized = false;
    }

    public void InitializeViewModel()
    {
        this.dataAccess = new DataAccess();

        _isInitialized = true;
    }

}
