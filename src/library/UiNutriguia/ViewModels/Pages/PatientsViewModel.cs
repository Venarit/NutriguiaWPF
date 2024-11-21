using Nutriguia.Model.DataAccess;
using System.Diagnostics.Tracing;
using System.Windows.Navigation;
using UiNutriguia.Models;
using UiNutriguia.Views.Pages;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace UiNutriguia.ViewModels.Pages
{
    public partial class PatientsViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;
        private ISnackbarService snackbarService;
        private ControlAppearance _snackbarAppearance = ControlAppearance.Secondary;
        private DataAccess dataAccess;

        #region Observables properties

        [ObservableProperty]
        private IEnumerable<PatientModel> _patients;

        [ObservableProperty]
        private IEnumerable<ActivityModel> _activities;
        
        [ObservableProperty]
        private IEnumerable<ObjectiveModel> _objectives;

        [ObservableProperty]
        private IEnumerable<MacronutrientModel> _macronutrients;

        #endregion

        #region Commands

        [RelayCommand]
        private void GotoPage(Type type)
        {
            _ = navigationService.Navigate(type);
        }
        
        #endregion

        public void OpenSnackBar(bool result, string message)
        {
            // if result=error message: _snackbarAppearance = ControlAppearance.Danger
            // if result=success message: _snackbarAppearance = ControlAppearance.Success

            //snackbarService.Show(
            //    "Guardar paciente a base de datos:",
            //    "Error/Success Message: " + message,
            //    _snackbarAppearance,
            //    new SymbolIcon(SymbolRegular.Fluent24),
            //    TimeSpan.FromSeconds(5)
            //);
        }

        public void AddPatient(PatientModel patient)
        {
            try
            {
                //this.dataAccess.SetPatients(patient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting patients: " + ex.Message);
            }
        }

        public void AddProfile(NutritionalProfileModel profile)
        {

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

            var patientCollection = new List<PatientModel>();
            var activityCollection = new List<ActivityModel>();
            var objectiveCollection = new List<ObjectiveModel>();
            var macroCollection = new List<MacronutrientModel>();

            patientCollection = this.dataAccess.GetPatients();
            activityCollection = this.dataAccess.GetActivities();
            objectiveCollection = this.dataAccess.GetObjectives();
            macroCollection = this.dataAccess.GetMacronutrients();

            Patients = patientCollection;
            Activities = activityCollection;
            Objectives = objectiveCollection;
            Macronutrients = macroCollection;
            
            _isInitialized = true;
        }

    }
}
