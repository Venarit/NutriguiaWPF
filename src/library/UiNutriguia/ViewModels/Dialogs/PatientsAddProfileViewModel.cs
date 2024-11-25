using Nutriguia.Model.DataAccess;
using UiNutriguia.Models;

namespace UiNutriguia.ViewModels.Dialogs;

public partial class PatientsAddProfileViewModel : ObservableObject
{
    public Action? CloseDialog { get; set; }
    public Action? Refresh { get; set; }

    private DataAccess dataAccess;

    [ObservableProperty] private PatientModel _patient;
    [ObservableProperty] private IEnumerable<ActivityModel> _activities;
    [ObservableProperty] private IEnumerable<ObjectiveModel> _objectives;
    [ObservableProperty] private IEnumerable<MacronutrientModel> _macronutrients;
    [ObservableProperty] private ActivityModel _selectedActivity;
    [ObservableProperty] private ObjectiveModel _selectedObjective;
    [ObservableProperty] private MacronutrientModel _selectedMacronutrient;
    [ObservableProperty] private string _title;
    [ObservableProperty] private int _height;
    [ObservableProperty] private string _selectedSex;

    public PatientsAddProfileViewModel(PatientModel patientModel)
    {
        Patient = patientModel;

        InitializeViewModel();
    }

    public void InitializeViewModel()
    {
        this.dataAccess = new DataAccess();

        Title = "Añadir Perfil";

        if (Patient.NutritionalProfile == null)
        {
            NutritionalProfileModel profile = new NutritionalProfileModel();
            Patient.NutritionalProfile = profile;
        }
        else
        {
            SelectedSex = Patient.NutritionalProfile.Sex;
            Height = Patient.NutritionalProfile.Height;
        }

        FillComboBoxes();
    }

    private void FillComboBoxes()
    {
        Activities = this.dataAccess.GetActivities();
        Objectives = this.dataAccess.GetObjectives();
        Macronutrients = this.dataAccess.GetMacronutrients();
    }

    [RelayCommand]
    private void Save()
    {
        Patient.NutritionalProfile.Height = Height;
        Patient.NutritionalProfile.Sex = SelectedSex;
        Patient.NutritionalProfile.Objective = SelectedObjective;
        Patient.NutritionalProfile.Activity = SelectedActivity;
        Patient.NutritionalProfile.Macronutrient = SelectedMacronutrient;
        
        this.dataAccess.SetNutritionalProfile(Patient);
        Refresh?.Invoke();
        Cancel();
    }

    [RelayCommand]
    private void Cancel()
    {
        CloseDialog?.Invoke();
    }
}
