using LiveCharts.Wpf;
using LiveCharts;
using UiNutriguia.Models;
using Wpf.Ui;
using Wpf.Ui.Controls;
using System.Collections.ObjectModel;
using Nutriguia.Model.DataAccess;
using UiNutriguia.Enums;

namespace UiNutriguia.ViewModels.Pages;
public partial class DashboardViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private DataAccess dataAccess;
    private SeriesCollection SeriesCollection;

    [ObservableProperty] private string _greeting = "Hola!";
    [ObservableProperty] private ObservableCollection<AppointmentModel> _nextAppointments; 
    [ObservableProperty] private ObservableCollection<PatientModel> _patients; 
    [ObservableProperty] private PatientModel _nextPatient; 
    [ObservableProperty] private ObservableCollection<AppointmentStatusModel> _appointmentStatuses; 

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
        this.dataAccess = new DataAccess();

        NextAppointments = new ObservableCollection<AppointmentModel>();
        AppointmentStatuses = new ObservableCollection<AppointmentStatusModel>();
        Patients = new ObservableCollection<PatientModel>();

        UpdateGreeting();
        FillCollections();
        GetChart();
    }

    private void FillCollections()
    {
        Patients.Clear();
        var patientList = new List<PatientModel>();
        patientList = this.dataAccess.GetPatients();
        foreach (var patient in patientList)
        {
            Patients.Add(patient);
        }

        AppointmentStatuses.Clear();
        var statusList = new List<AppointmentStatusModel>();
        statusList = this.dataAccess.GetAppointmentStatuses();
        foreach (var status in statusList)
        {
            AppointmentStatuses.Add(status);
        }

        NextAppointments.Clear();
        var appointmentList = new List<AppointmentModel>();
        appointmentList = this.dataAccess.GetNextAppointments();
        foreach (var appointment in appointmentList)
        {
            appointment.Patient = Patients.FirstOrDefault(p => p.IdPatient == appointment.IdPatient);
            appointment.AppointmentStatus = AppointmentStatuses.FirstOrDefault(a => a.IdAppointmentStatus == appointment.IdAppointmentStatus);
            NextAppointments.Add(appointment);
        }

        if (NextAppointments.Count > 0)
        {
            NextPatient = NextAppointments.First().Patient;
        }
    }

    private void UpdateGreeting()
    {
        var currentHour = TimeOnly.FromDateTime(DateTime.Now);
        var day = new TimeOnly(12, 0);
        var afternoon = new TimeOnly(19, 0);

        if (currentHour < day)
        {
            Greeting = "Buenos días";
        }
        else if (currentHour < afternoon)
        {
            Greeting = "Buenas tardes";
        }
        else
        {
            Greeting = "Buenas noches";
        }
    }

    private void GetChart()
    {
        SeriesCollection = new SeriesCollection
        {
            new LineSeries
            {
                Values = new ChartValues<double> { 3, 5, 7, 4 }
            },
            new ColumnSeries
            {
                Values = new ChartValues<decimal> { 5, 6, 2, 7 }
            }
        };
    }
}
