using Nutriguia.Model.DataAccess;
using System.Collections.ObjectModel;
using UiNutriguia.Enums;
using UiNutriguia.Models;

namespace UiNutriguia.ViewModels.Dialogs;

public partial class SchedulerAddViewModel : ObservableObject
{
    private DataAccess dataAccess;
    private DateTime selectedDate;

    [ObservableProperty] private AppointmentModel _appointmentModel;
    [ObservableProperty] private ObservableCollection<PatientModel> _patients;

    public void InitializeViewModel()
    {
        this.dataAccess = new DataAccess();

        Patients = new ObservableCollection<PatientModel>();

        FillComboboxes();
    }

    public void FillComboboxes()
    {
        var patients = this.dataAccess.GetPatients();
        Patients.Clear();
        foreach (var patient in patients)
        {
            this.Patients.Add(patient);
        }
    }
}
