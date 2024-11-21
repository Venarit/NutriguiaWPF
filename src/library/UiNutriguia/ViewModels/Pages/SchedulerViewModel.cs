using System.Globalization;
using Wpf.Ui.Controls;
using Wpf.Ui;
using Nutriguia.Model.DataAccess;
using System.Windows.Media;
using UiNutriguia.Models;
using System;
using UiNutriguia.Enums;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using UiNutriguia.Views.Dialogs;
using UiNutriguia.ViewModels.Dialogs;

namespace UiNutriguia.ViewModels.Pages;

public partial class SchedulerViewModel(INavigationService navigationService, IContentDialogService dialogService) : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private DataAccess dataAccess;
    private DateTime selectedDate;

    [ObservableProperty] private ObservableCollection<AppointmentModel> _appointments;
    [ObservableProperty] private ObservableCollection<AppointmentStatusModel> _appointmentStatuses;
    [ObservableProperty] private ObservableCollection<PatientModel> _patients;
    [ObservableProperty] private AppointmentModel _selectedAppointment;
    [ObservableProperty] private ContentPresenter _contentPresenter;

    public SchedulerAddViewModel SchedulerAddViewModel { get; set; }

    public DateTime SelectedDate
    {
        get => selectedDate;
        set
        {
            SetProperty(ref selectedDate, value);
            OnPropertyChanged(nameof(SelectedDateOnly));
            OnPropertyChanged(nameof(SelectedDateOnlyString));
            RefreshAppointments();
        }
    }

    public DateOnly SelectedDateOnly => DateOnly.FromDateTime(SelectedDate);
    public string SelectedDateOnlyString
    {
        get
        {
            var formattedDate = SelectedDate.ToString("MMMM dd, yyyy");
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(formattedDate);
        }
    }

    [RelayCommand]
    private void GotoPage(Type type)
    {
        _ = navigationService.Navigate(type);
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

        this.selectedDate = DateTime.Now;
        this.dataAccess = new DataAccess();

        Appointments = new ObservableCollection<AppointmentModel>();
        AppointmentStatuses = new ObservableCollection<AppointmentStatusModel>();
        Patients = new ObservableCollection<PatientModel>();

        SchedulerAddViewModel = new SchedulerAddViewModel();

        FillComboboxes();

        _isInitialized = true;
    }

    public void FillComboboxes()
    {
        var statuses = this.dataAccess.GetAppointmentStatuses();
        AppointmentStatuses.Clear();
        foreach (var status in statuses)
        {
            this.AppointmentStatuses.Add(status);
        }

        var patients = this.dataAccess.GetPatients();
        Patients.Clear();
        foreach (var patient in patients)
        {
            this.Patients.Add(patient);
        }
    }

    public void RefreshAppointments()
    {
        Appointments.Clear();
        var appointmentList = new List<AppointmentModel>();
        appointmentList = this.dataAccess.GetAppointments(SelectedDate);
        foreach (var appointment in appointmentList)
        {
            Appointments.Add(appointment);
        }
    }

    [RelayCommand] 
    private async Task AddAppointment() 
    {
        var schedulerAddDialog = new SchedulerAddDialog(dialogService.GetDialogHost(), SchedulerAddViewModel);

        _ = await schedulerAddDialog.ShowAsync();
    }

    [RelayCommand]
    public void EditAppointment(AppointmentModel selectedAppointment)
    {
        if (selectedAppointment != null)
        {
        }
    }

}
