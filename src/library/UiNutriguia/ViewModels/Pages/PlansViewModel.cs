using Wpf.Ui.Controls;
using Wpf.Ui;
using Nutriguia.Model.DataAccess;
using UiNutriguia.Models;
using UiNutriguia.Views.Pages;

namespace UiNutriguia.ViewModels.Pages;

public partial class PlansViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private DataAccess dataAccess;

    [ObservableProperty] private Visibility _gridVisibility = Visibility.Hidden;
    [ObservableProperty] private Visibility _messageVisibility = Visibility.Hidden;
    [ObservableProperty] private Visibility _chooseMessageVisibility = Visibility.Visible;
    [ObservableProperty] private List<PatientModel> _patients;
    
    private PlanPatientModel _planPatient;
    public PlanPatientModel PlanPatient
    {
        get => _planPatient;
        set
        {
            SetProperty(ref _planPatient, value);
            if (value != null)
            {
                GridVisibility = Visibility.Visible;
                MessageVisibility = Visibility.Hidden;
            }
        }
    }

    private PatientModel _selectedPatient;
    public PatientModel SelectedPatient
    {
        get => _selectedPatient;
        set
        {
            SetProperty(ref _selectedPatient, value);
            GetNutritionalPlan();
        }
    }
    private string _filterText;
    public string FilterText
    {
        get => _filterText;
        set
        {
            SetProperty(ref _filterText, value);
            ApplyFilter();
        }
    }

    [RelayCommand]
    private void GotoPage(Type type)
    {
        _ = navigationService.Navigate(type);
    }

    [RelayCommand]
    private void GotoAddPlanOption()
    {
        if (PlanPatient != null && SelectedPatient != null)
        {
            NavigationContext.Parameter = SelectedPatient;
            _ = navigationService.Navigate(typeof(PlansAddPage));
        }
    }

    [RelayCommand]
    private void AddNewPlan()
    {
        if (SelectedPatient != null)
        {
            PlanPatient = new PlanPatientModel
            {
                IdPlan = 0,
                IdPatient = SelectedPatient.IdPatient,
                PatientModel = SelectedPatient,
                PlanModel = new PlanModel
                {
                    PlanOptionModel1 = new PlanOptionModel(),
                    PlanOptionModel2 = new PlanOptionModel(),
                    PlanOptionModel3 = new PlanOptionModel(),
                }
            };
        }
    }

    public void OnNavigatedTo()
    {
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

        Patients = new List<PatientModel>();
        Patients = this.dataAccess.GetPatients();

        _isInitialized = true;
    }

    public void Reload()
    {

    }

    public void ApplyFilter()
    {
        if (!string.IsNullOrEmpty(FilterText))
        {
            SelectedPatient = Patients.Where(p => p.FullName.Equals(FilterText)).FirstOrDefault();
        }
    }

    public void GetNutritionalPlan()
    { 
        if (SelectedPatient != null)
        {
            ChooseMessageVisibility = Visibility.Hidden;

            var planPatients = this.dataAccess.GetPlanPatient(SelectedPatient.IdPatient);
            if (planPatients.Any())
                PlanPatient = planPatients.First();

            if (PlanPatient != null)
            {
                GridVisibility = Visibility.Visible;
                MessageVisibility = Visibility.Hidden;
            }
            else
            {
                MessageVisibility = Visibility.Visible;
                GridVisibility = Visibility.Hidden;
            }
        }
    }

}
