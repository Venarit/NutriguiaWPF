using Nutriguia.Model.DataAccess;
using System.Collections.ObjectModel;
using UiNutriguia.Models;

namespace UiNutriguia.ViewModels.Dialogs;

public partial class SchedulerAddViewModel : ObservableObject
{
    public Action? CloseDialog { get; set; }
    public Action? Refresh { get; set; }


    private DataAccess dataAccess;
    private DateOnly selectedDate;
    private int startHour;
    private int startMinute;
    private int endHour;
    private int endMinute;

    [ObservableProperty] private AppointmentModel _appointmentModel;
    [ObservableProperty] private ObservableCollection<PatientModel> _patients;
    [ObservableProperty] private ObservableCollection<AppointmentStatusModel> _statuses;
    [ObservableProperty] private PatientModel _selectedPatient;
    [ObservableProperty] private AppointmentStatusModel _selectedStatus;

    public SchedulerAddViewModel(AppointmentModel appointment, DateOnly date)
    {
        AppointmentModel = appointment;
        selectedDate = date;
        InitializeViewModel();
    }

    public void InitializeViewModel()
    {
        this.dataAccess = new DataAccess();

        Patients = new ObservableCollection<PatientModel>();
        Statuses = new ObservableCollection<AppointmentStatusModel>();

        FillComboboxes();
        FillTime();
    }

    private void FillTime()
    {
        StartHour = AppointmentModel.StartDateTime.Hour;
        StartMinute = AppointmentModel.StartDateTime.Minute;
        EndHour = AppointmentModel.EndDateTime.Hour;
        EndMinute = AppointmentModel.EndDateTime.Minute;
    }

    public void FillComboboxes()
    {
        var patients = this.dataAccess.GetPatients();
        Patients.Clear();
        foreach (var patient in patients)
        {
            this.Patients.Add(patient);
        }

        SelectedPatient = Patients.FirstOrDefault(p => p.IdPatient == AppointmentModel.Patient?.IdPatient);

        var statuses = this.dataAccess.GetAppointmentStatuses();
        Statuses.Clear();
        foreach (var status in statuses)
        {
            this.Statuses.Add(status);
        }

        SelectedStatus = Statuses.FirstOrDefault(p => p.IdAppointmentStatus == AppointmentModel.AppointmentStatus?.IdAppointmentStatus);
    }

    [RelayCommand]
    private void SaveAppointment()
    {
        AppointmentModel.StartDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, StartHour, StartMinute, 0 );
        AppointmentModel.EndDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, EndHour, EndMinute, 0 );
        AppointmentModel.IdAppointmentStatus = SelectedStatus.IdAppointmentStatus;
        AppointmentModel.IdPatient = SelectedPatient.IdPatient;

        this.dataAccess.SetAppointment(AppointmentModel);
        Refresh?.Invoke();
        Cancel();
    }

    [RelayCommand]
    private void Cancel()
    {
        CloseDialog?.Invoke();
    }

    public int StartHour
    {
        get => startHour;
        set
        {
            if (startHour != value)
            {
                startHour = value;
                OnPropertyChanged(nameof(StartHour));
            }
        }
    }

    public int StartMinute
    {
        get => startMinute;
        set
        {
            if (startMinute != value)
            {
                startMinute = value;
                OnPropertyChanged(nameof(StartMinute));
            }
        }
    }

    public int EndHour
    {
        get => endHour;
        set
        {
            if (endHour != value)
            {
                endHour = value;
                OnPropertyChanged(nameof(EndHour));
            }
        }
    }

    public int EndMinute
    {
        get => endMinute;
        set
        {
            if (endMinute != value)
            {
                endMinute = value;
                OnPropertyChanged(nameof(EndMinute));
            }
        }
    }

}
