using Nutriguia.Model.DataAccess;
using UiNutriguia.Models;

namespace UiNutriguia.ViewModels.Dialogs;

public partial class PatientsAddViewModel : ObservableObject
{
    public Action? CloseDialog { get; set; }
    public Action? Refresh { get; set; }

    private DataAccess dataAccess;

    [ObservableProperty] private PatientModel _patient;
    [ObservableProperty] private string _title;


    public PatientsAddViewModel(PatientModel patientModel)
    {
        Patient = patientModel;

        InitializeViewModel();
    }

    public void InitializeViewModel()
    {
        this.dataAccess = new DataAccess();

        if (Patient.IdPatient == 0)
        {
            Title = "Añadir Paciente";
        }
        else
        {
            Title = "Editar Paciente";
        }
    }

    [RelayCommand]
    private void SavePatient()
    {
        this.dataAccess.SetPatient(Patient);
        Refresh?.Invoke();
        Cancel();
    }

    [RelayCommand]
    private void Cancel()
    {
        CloseDialog?.Invoke();
    }
}
