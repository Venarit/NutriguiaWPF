using Nutriguia.Model.DataAccess;
using UiNutriguia.Models;

namespace UiNutriguia.ViewModels.Dialogs;

public partial class PatientsAddMeasurementViewModel : ObservableObject
{
    public Action? CloseDialog { get; set; }
    public Action? Refresh { get; set; }
    
    private DataAccess dataAccess;
    
    [ObservableProperty] private PatientModel _patient;
    [ObservableProperty] private PatientMeasurementModel _patientMeasurement;
    [ObservableProperty] private int _selectedEquation;
    [ObservableProperty] private int _bmr;
    [ObservableProperty] private int _tdee;

    public PatientsAddMeasurementViewModel(PatientModel patientModel)
    {
        Patient = patientModel;
        
        InitializeViewModel();
    }

    public void InitializeViewModel()
    {
        this.dataAccess = new DataAccess();

        PatientMeasurement = new PatientMeasurementModel();

        SelectedEquation = 1;
    }

    [RelayCommand]
    private void Save()
    {
        Patient.NutritionalProfile.PatientMeasurement = PatientMeasurement;
        this.dataAccess.SetPatientMeasurement(Patient);
        Refresh?.Invoke();
        Cancel();
    }

    [RelayCommand]
    private void Cancel()
    {
        CloseDialog?.Invoke();
    }

    [RelayCommand]
    private void CalculateBMR()
    {
        if (SelectedEquation != null)
        {
            double BMR = 0;
            var sex = Patient.NutritionalProfile.Sex;
            var weight = PatientMeasurement.Weight;
            var height = Patient.NutritionalProfile.Height;
            var dob = DateTime.ParseExact(Patient.BirthDate, "MM/dd/yyyy HH:mm:ss", null);
            var age = DateTime.Today.Year - dob.Year;
            if (dob.Date > DateTime.Today.AddYears(-age)) age--;

            if (SelectedEquation == 1) //Harris
            {
                if (sex.Equals("M"))
                {
                    BMR = 88.362 + (13.397 * (double)weight) + (4.799 * height) - (5.677 * age); //MEN
                }
                if (sex.Equals("F"))
                {
                    BMR = 447.593 + (9.247 * (double)weight) + (3.098 * height) - (4.330 * age); //WOMEN
                }
            } 
            else //St Jeor (2)
            {
                if (sex.Equals("M"))
                {
                    BMR = (10 * (double)weight) + (6.25 * height) - (5 * age) + 5; //MEN
                }
                if (sex.Equals("F"))
                {
                    BMR = (10 * (double)weight) + (6.25 * height) - (5 * age) - 161; //WOMEN
                }
            }

            var TDEE = (decimal)BMR * Patient.NutritionalProfile.Activity.Factor;
            
            PatientMeasurement.BMR = Bmr = (int)BMR;
            PatientMeasurement.TDEE = Tdee = (int)TDEE;
        }
    }
}
