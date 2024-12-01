using Nutriguia.Model.DataAccess;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Windows.Navigation;
using UiNutriguia.Models;
using UiNutriguia.ViewModels.Dialogs;
using UiNutriguia.Views.Dialogs;
using UiNutriguia.Views.Pages;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace UiNutriguia.ViewModels.Pages
{
    public partial class PatientsViewModel(INavigationService navigationService, IContentDialogService dialogService) : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;
        private ISnackbarService snackbarService;
        private ControlAppearance _snackbarAppearance = ControlAppearance.Secondary;
        private DataAccess dataAccess;

        #region Observables properties

        [ObservableProperty] private ObservableCollection<PatientModel> _patients;
        [ObservableProperty] private PatientModel _selectedPatient;

        #endregion

        public PatientsAddViewModel PatientsAddViewModel { get; set; }
        public PatientsAddProfileViewModel PatientsAddProfileViewModel { get; set; }
        public PatientsAddMeasurementViewModel PatientsAddMeasurementViewModel { get; set; }

        #region Commands

        [RelayCommand]
        private void GotoPage(Type type)
        {
            _ = navigationService.Navigate(type);
        }

        [RelayCommand]
        private void GotoProfilePage()
        {
            if (SelectedPatient != null)
            {
                NavigationContext.Parameter = SelectedPatient;
                _ = navigationService.Navigate(typeof(PatientProfilePage));
            }
            else
            {
                //Dialog please select patient TODO
            }
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

            Patients = new ObservableCollection<PatientModel>();
            RefreshPatients();

            _isInitialized = true;
        }

        private void RefreshPatients()
        {
            Patients.Clear();
            var patients = this.dataAccess.GetPatients();
            foreach (var patient in patients)
            { 
                Patients.Add(patient);
            }
        }

        [RelayCommand]
        private async Task AddPatient()
        {
            var newPatient = new PatientModel();

            PatientsAddViewModel = new PatientsAddViewModel(newPatient)
            {
                Refresh = () => RefreshPatients()
            };

            var patientsAddDialog = new PatientsAddDialog(dialogService.GetDialogHost(), PatientsAddViewModel);

            _ = await patientsAddDialog.ShowAsync();

        }

        [RelayCommand]
        private async Task EditPatient(PatientModel selectedPatient)
        {
            if (SelectedPatient != null)
            {
                var editPatient = SelectedPatient;

                PatientsAddViewModel = new PatientsAddViewModel(editPatient)
                {
                    Refresh = () => RefreshPatients()
                };

                var patientsAddDialog = new PatientsAddDialog(dialogService.GetDialogHost(), PatientsAddViewModel);

                _ = await patientsAddDialog.ShowAsync();
            }
        }

        [RelayCommand]
        private async Task AddProfile()
        {
            if (SelectedPatient != null)
            {
                var patient = SelectedPatient;

                PatientsAddProfileViewModel = new PatientsAddProfileViewModel(patient)
                {
                    Refresh = () => RefreshPatients()
                };

                var patientsAddProfileDialog = new PatientsAddProfileDialog(dialogService.GetDialogHost(), PatientsAddProfileViewModel);

                _ = await patientsAddProfileDialog.ShowAsync();
            }
        }

        [RelayCommand]
        private async Task AddMeasurement()
        {
            if (SelectedPatient != null)
            {
                if (SelectedPatient.NutritionalProfile != null)
                {
                    var patient = SelectedPatient;

                    PatientsAddMeasurementViewModel = new PatientsAddMeasurementViewModel(patient)
                    {
                        Refresh = () => RefreshPatients()
                    };

                    var patientsAddProfileDialog = new PatientsAddMeasurementDialog(dialogService.GetDialogHost(), PatientsAddMeasurementViewModel);

                    _ = await patientsAddProfileDialog.ShowAsync();
                }
                else
                {
                    //dialog
                }
            }
        }

    }
}
