using Nutriguia.Model.DataAccess;
using System.Collections.ObjectModel;
using UiNutriguia.Models;

namespace UiNutriguia.ViewModels.Dialogs;

public partial class PatientAddDislikedFoodViewModel : ObservableObject
{
    public Action? CloseDialog { get; set; }
    public Action? Refresh { get; set; }

    private DataAccess dataAccess;

    [ObservableProperty] private PatientModel _patient;
    [ObservableProperty] private ObservableCollection<FoodModel> _foods;
    [ObservableProperty] private FoodModel _food;

    public PatientAddDislikedFoodViewModel(PatientModel patientModel)
    {
        Patient = patientModel;

        InitializeViewModel();
    }

    public void InitializeViewModel()
    {
        this.dataAccess = new DataAccess();

        Foods = new ObservableCollection<FoodModel>(); 
        Food = new FoodModel();

        GetFoods();
    }

    private void GetFoods()
    {
        var foodlist = this.dataAccess.GetFoods(null, null);
        foreach (var food in foodlist)
        {
            Foods.Add(food);
        }
    }

    [RelayCommand]
    private void Save()
    {
        Food = Foods.FirstOrDefault(x => x.Name.Equals(Food.Name));
        
        if (Food.IdFood != 0)
        {
            this.dataAccess.SetPatientDislikedFood(Patient.IdPatient, Food.IdFood);
        }
        
        Refresh?.Invoke();
        Cancel();
    }

    [RelayCommand]
    private void Cancel()
    {
        CloseDialog?.Invoke();
    }
}
