using LiveCharts.Wpf;
using LiveCharts;
using UiNutriguia.Models;
using Wpf.Ui;
using Wpf.Ui.Controls;
using System.Collections.ObjectModel;
using Nutriguia.Model.DataAccess;
using UiNutriguia.Enums;
using LiveCharts.Defaults;
using System.Runtime.Serialization;

namespace UiNutriguia.ViewModels.Pages;
public partial class DashboardViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private DataAccess dataAccess;
    private DateTime today = DateTime.Now;

    [ObservableProperty] private string _greeting = "Hola!";
    [ObservableProperty] private ObservableCollection<AppointmentModel> _nextAppointments; 
    [ObservableProperty] private ObservableCollection<AppointmentModel> _appointmentsHistory; 
    [ObservableProperty] private ObservableCollection<PatientModel> _patients; 
    [ObservableProperty] private PatientModel _nextPatient; 
    [ObservableProperty] private ObservableCollection<AppointmentStatusModel> _appointmentStatuses; 
    [ObservableProperty] private int _totalPatients; 
    [ObservableProperty] private int _totalCompleted; 
    [ObservableProperty] private int _totalConfirmed; 
    [ObservableProperty] private int _totalPending; 
    [ObservableProperty] private int _totalCancelled;

    [ObservableProperty] public SeriesCollection _seriesCollection;
    [ObservableProperty] public string[] _labels;
    [ObservableProperty] public Func<double, string> _formatter;

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
        AppointmentsHistory = new ObservableCollection<AppointmentModel>();

        SeriesCollection = new SeriesCollection();
        Formatter = value => value.ToString("N0");

        UpdateGreeting();
        FillCollections();
        GetDayAppointments();
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
        var currentHour = TimeOnly.FromDateTime(today);
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
        var groupedAppointments = AppointmentsHistory
            .GroupBy(a => a.StartDateTime.Date)
            .Select(g => new { Date = g.Key, Count = g.Count() })
            .OrderBy(g => g.Date)
            .ToList();

        ChartValues<int> dateValues = new ChartValues<int>();
        List<string> dates = new List<string>();

        foreach (var entry in groupedAppointments)
        {
            dateValues.Add(entry.Count);
            dates.Add(entry.Date.ToString("dd/MM/yyyy"));
        }

        SeriesCollection = new SeriesCollection 
        {
            new LineSeries
            {
                Title = "Citas por día",
                Values = dateValues,
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 10
            }
        };

        Labels = dates.ToArray();
    }


    private void GetHistoricAppointments(DateTime start, DateTime end)
    {
        var appointments = this.dataAccess.GetAppointmentHistory(start, end);
        AppointmentsHistory.Clear();
        foreach (var appointment in appointments)
        {
            AppointmentsHistory.Add(appointment);
        }

        TotalPatients = AppointmentsHistory.Select(a => a.Patient.IdPatient).Distinct().Count();
        TotalCompleted = AppointmentsHistory.Select(a => a.AppointmentStatus.Name).Where(a => a.Equals("Completada")).Count();
        TotalConfirmed = AppointmentsHistory.Select(a => a.AppointmentStatus.Name).Where(a => a.Equals("Confirmada")).Count();
        TotalPending = AppointmentsHistory.Select(a => a.AppointmentStatus.Name).Where(a => a.Equals("Pendiente")).Count();
        TotalCancelled = AppointmentsHistory.Select(a => a.AppointmentStatus.Name).Where(a => a.Equals("Cancelada")).Count();

        GetChart();
    }

    [RelayCommand]
    private void GetDayAppointments()
    {
        DateTime startOfDay = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0); 
        DateTime endOfDay = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);

        GetHistoricAppointments(startOfDay, endOfDay);
    }

    [RelayCommand]
    private void GetWeekAppointments()
    {
        DateTime startOfWeek = today.Date.AddDays(-(int)today.DayOfWeek);
        DateTime endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1);

        GetHistoricAppointments(startOfWeek, endOfWeek);
    }

    [RelayCommand]
    private void GetMonthAppointments()
    {
        DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
        DateTime endOfMonth = startOfMonth.AddMonths(1).AddSeconds(-1);

        GetHistoricAppointments(startOfMonth, endOfMonth);
    }

    [RelayCommand]
    private void Get3MonthAppointments()
    {
        DateTime startOfCurrentMonth = new DateTime(today.Year, today.Month, 1); 
        DateTime startOfLastThreeMonths = startOfCurrentMonth.AddMonths(-3); 
        DateTime endOfLastThreeMonths = startOfCurrentMonth.AddSeconds(-1);

        GetHistoricAppointments(startOfLastThreeMonths, endOfLastThreeMonths);
    }

    [RelayCommand]
    private void GetYearAppointments()
    {
        DateTime startOfYear = new DateTime(today.Year, 1, 1); 
        DateTime endOfYear = new DateTime(today.Year, 12, 31, 23, 59, 59);

        GetHistoricAppointments(startOfYear, endOfYear);
    }

}
