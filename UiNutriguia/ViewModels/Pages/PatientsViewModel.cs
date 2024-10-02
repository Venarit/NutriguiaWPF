using Nutriguia.Model.DataAccess;
using System.Diagnostics.Tracing;
using System.Windows.Navigation;
using UiNutriguia.Models;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace UiNutriguia.ViewModels.Pages
{
    public partial class PatientsViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;
        private ISnackbarService snackbarService;
        private ControlAppearance _snackbarAppearance = ControlAppearance.Secondary;

        [ObservableProperty]
        private IEnumerable<PatientModel> _patients;

        private DataAccess dataAccess;

        [RelayCommand]
        private void GotoPage(Type type)
        {
            _ = navigationService.Navigate(type);
        }

        public void OpenSnackBar(bool result, string message)
        {
            // if result=error message: _snackbarAppearance = ControlAppearance.Danger
            // if result=success message: _snackbarAppearance = ControlAppearance.Success

            snackbarService.Show(
                "Guardar paciente a base de datos:",
                "Error/Success Message: " + message,
                _snackbarAppearance,
                new SymbolIcon(SymbolRegular.Fluent24),
                TimeSpan.FromSeconds(5)
            );
        }

        public void AddPatient(PatientModel patient)
        {
            try
            {
                this.dataAccess.SetPatients(patient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting patients: " + ex.Message);
            }
        }

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom() { }

        public void InitializeViewModel()
        {
            this.dataAccess = new DataAccess();

            var patientCollection = new List<PatientModel>();
            
            patientCollection = this.dataAccess.GetPatients();

            Patients = patientCollection;
            
            _isInitialized = true;
        }

    }
}
