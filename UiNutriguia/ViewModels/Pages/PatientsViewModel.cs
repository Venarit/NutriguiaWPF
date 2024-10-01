using Nutriguia.Model.DataAccess;
using System.Diagnostics.Tracing;
using System.Windows.Navigation;
using UiNutriguia.Models;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace UiNutriguia.ViewModels.Pages
{
    public partial class PatientsViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private IEnumerable<PatientModel> _patients;

        private DataAccess dataAccess;

        private INavigationService navigationService;

        [RelayCommand]
        private void AddPatient(Type type)
        {
            _ = navigationService.Navigate(type);
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
