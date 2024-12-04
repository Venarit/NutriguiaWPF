using Wpf.Ui.Controls;
using Wpf.Ui;
using Nutriguia.Model.DataAccess;
using System.Collections.ObjectModel;
using System.Windows.Media;
using UiNutriguia.Models;
using UiNutriguia.Views.Pages;

namespace UiNutriguia.ViewModels.Pages;

public partial class PlansAddViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private DataAccess dataAccess;
    private int idPlan;

    [ObservableProperty] private PatientModel _patient;
    [ObservableProperty] private PlanPatientModel _planPatient;

    [ObservableProperty] private DishModel _selectedDish;

    [ObservableProperty] private List<DishModel> _dishesList;
    [ObservableProperty] private PlanOptionModel _planOption;
    [ObservableProperty] private List<FoodModel> _patientDislikedFoods;

    [ObservableProperty] private decimal _patientHco;
    [ObservableProperty] private decimal _patientProtein;
    [ObservableProperty] private decimal _patientLipids;

    private string _planDishBreakfast = string.Empty;
    private string _planDishCollation1 = string.Empty;
    private string _planDishMeal = string.Empty;
    private string _planDishCollation2 = string.Empty;
    private string _planDishDinner = string.Empty;

    public string PlanDishBreakfast
    {
        get => _planDishBreakfast;
        set
        {
            SetProperty(ref _planDishBreakfast, value);
            if (!string.IsNullOrEmpty(value))
            {
                PlanOption.BreakfastModel.Dish = GetDishByName(value);
                if (PlanOption.BreakfastModel.Dish != null)
                {
                    OnPropertyChanged(nameof(PlanOption.BreakfastModel.Dish));
                    PlanOption.RecalculateNutrients();
                    OnPropertyChanged(nameof(PlanOption));
                }
            }
        }
    }

    public string PlanDishCollation1
    {
        get => _planDishCollation1;
        set
        {
            SetProperty(ref _planDishCollation1, value);
            if (!string.IsNullOrEmpty(value))
            {
                PlanOption.Collation1Model.Dish = GetDishByName(value);
                if (PlanOption.Collation1Model.Dish != null)
                {
                    OnPropertyChanged(nameof(PlanOption.Collation1Model.Dish));
                    PlanOption.RecalculateNutrients();
                    OnPropertyChanged(nameof(PlanOption));
                }
            }
        }
    }

    public string PlanDishMeal
    {
        get => _planDishMeal;
        set
        {
            SetProperty(ref _planDishMeal, value);
            if (!string.IsNullOrEmpty(value))
            {
                PlanOption.MealModel.Dish = GetDishByName(value);
                if (PlanOption.MealModel.Dish != null)
                {
                    OnPropertyChanged(nameof(PlanOption.MealModel.Dish));
                    PlanOption.RecalculateNutrients();
                    OnPropertyChanged(nameof(PlanOption));
                }
            }
        }
    }

    public string PlanDishCollation2
    {
        get => _planDishCollation2;
        set
        {
            SetProperty(ref _planDishCollation2, value);
            if (!string.IsNullOrEmpty(value))
            {
                PlanOption.Collation2Model.Dish = GetDishByName(value);
                if (PlanOption.Collation2Model.Dish != null)
                {
                    OnPropertyChanged(nameof(PlanOption.Collation2Model.Dish));
                    PlanOption.RecalculateNutrients();
                    PlanOption.RecalculateNutrients();
                    OnPropertyChanged(nameof(PlanOption));
                }
            }
        }
    }

    public string PlanDishDinner
    {
        get => _planDishDinner;
        set
        {
            SetProperty(ref _planDishDinner, value);
            if (!string.IsNullOrEmpty(value))
            {
                PlanOption.DinnerModel.Dish = GetDishByName(value);
                if (PlanOption.DinnerModel.Dish != null)
                {
                    OnPropertyChanged(nameof(PlanOption.DinnerModel.Dish));
                    PlanOption.RecalculateNutrients();
                    PlanOption.RecalculateNutrients();
                    OnPropertyChanged(nameof(PlanOption));
                }
            }
        }
    }

    [RelayCommand]
    private void GotoPage(Type type)
    {
        _ = navigationService.Navigate(type);
    }

    public void OnNavigatedTo()
    {
        if (NavigationContext.Parameter is PatientModel model)
        {
            Patient = model;
            NavigationContext.Parameter = null;
            OnPropertyChanged(nameof(Patient));
        }
        if (NavigationContext.Parameter2 is int planId)
        {
            idPlan = planId;
            NavigationContext.Parameter2 = null;
        }
        if (!_isInitialized)
            InitializeViewModel();
    }

    public void OnNavigatedFrom()
    {
        _isInitialized = false;
    }


    public void InitializeViewModel()
    {
        this.dataAccess = new DataAccess();

        DishesList = new List<DishModel>();
        DishesList = this.dataAccess.GetDishes();

        PatientDislikedFoods = new List<FoodModel>();
        PatientDislikedFoods = dataAccess.GetPatientDislikedFoods(Patient.IdPatient);

        PlanOption = new PlanOptionModel
        {
            BreakfastModel = new PlanDishModel(),
            Collation1Model = new PlanDishModel(),
            MealModel = new PlanDishModel(),
            Collation2Model = new PlanDishModel(),
            DinnerModel = new PlanDishModel(),
        };

        GetPatientValues();

        _isInitialized = true;
    }

    private void GetPatientValues()
    {
        var tdee = Patient.NutritionalProfile.PatientMeasurement.TDEE;
        PatientHco = (decimal)(tdee * Patient.NutritionalProfile.Macronutrient.Hco);
        PatientProtein = (decimal)(tdee * Patient.NutritionalProfile.Macronutrient.Protein);
        PatientLipids = (decimal)(tdee * Patient.NutritionalProfile.Macronutrient.Lipids);
    }

    [RelayCommand]
    private void SelectDish(DishModel dish)
    {
        if (dish != null)
        {
            SelectedDish = dish;
            var dishFoods = this.dataAccess.GetDishFoods(SelectedDish.IdDish);
            SelectedDish.DishFoodModel.Clear();
            foreach (var dishFood in dishFoods)
            {
                SelectedDish.DishFoodModel.Add(dishFood);
            }
        }
    }

    [RelayCommand]
    private void Save()
    {
        if (PlanOption.BreakfastModel.Dish != null && PlanOption.Collation1Model.Dish != null &&
            PlanOption.MealModel.Dish != null && PlanOption.Collation2Model.Dish != null && 
            PlanOption.DinnerModel.Dish != null)
        {
            this.dataAccess.SetPlanOption1(PlanOption, Patient.IdPatient, idPlan);
            GotoPage(typeof(Views.Pages.PlansPage));
        }
    }

    private DishModel GetDishByName(string name) => DishesList.FirstOrDefault(d => d.Name == name);
}
