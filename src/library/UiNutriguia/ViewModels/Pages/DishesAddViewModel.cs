using Microsoft.IdentityModel.Tokens;
using Nutriguia.Model.DataAccess;
using System.Collections.ObjectModel;
using UiNutriguia.Models;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace UiNutriguia.ViewModels.Pages;

public partial class DishesAddViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private DataAccess dataAccess;
    private DishFoodModel _selectedDishFood;

    [ObservableProperty] private IEnumerable<FoodModel> _foods;
    [ObservableProperty] private DishModel _dish;

    public DishFoodModel SelectedDishFood
    {
        get => _selectedDishFood;
        set
        {
            SetProperty(ref _selectedDishFood, value);
        }
    }

    [RelayCommand]
    private void GotoPage(Type type)
    {
        _ = navigationService.Navigate(type);
    }

    public void OnNavigatedTo()
    {
        if (NavigationContext.Parameter is DishModel model)
        {
            Dish = model;
            NavigationContext.Parameter = null;
            OnPropertyChanged(nameof(Dish));
        }
        else
        {
            Dish = new DishModel { DishFoodModel = new ObservableCollection<DishFoodModel>() };
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
        var foodCollection = new List<FoodModel>();
        foodCollection = this.dataAccess.GetFoods(null, null);
        Foods = foodCollection;

        _isInitialized = true;
    }

    [RelayCommand]
    private void AddDishFood()
    {
        if (Dish.DishFoodModel != null)
        {
            Dish.DishFoodModel.Add(new DishFoodModel
            {
                Equivalent = 1,
                Quantity = 0,
                Kcal = 0,
                Hco = 0,
                Lipids = 0,
                Protein = 0,
                Food = new FoodModel()
            });
            OnPropertyChanged(nameof(Dish));
        }
    }

    [RelayCommand]
    private void DeleteDishFood(DishFoodModel model)
    {
        if (Dish.DishFoodModel != null)
        {
            Dish.DishFoodModel.Remove(model);
            OnPropertyChanged(nameof(Dish));
        }
    }

    public void ApplyFood(string foodName)
    {
        if (!string.IsNullOrEmpty(foodName))
        {
            var food = Foods.FirstOrDefault(f => f.Name != null && f.Name.Contains(foodName, StringComparison.OrdinalIgnoreCase));
            if (food != null)
            {
                var selectedDishFood = Dish.DishFoodModel.FirstOrDefault(f => f.Food.Name != null && f.Food.Name.Equals(food.Name));
                if (selectedDishFood != null)
                {
                    selectedDishFood.Food = food;

                    OnPropertyChanged(nameof(Dish));
                }
            }
        }
    }

    [RelayCommand]
    private void SaveDish()
    {
        if (!Dish.Name.IsNullOrEmpty() && Dish.DishFoodModel.Count > 0)
        {
            this.dataAccess.SetDish(Dish);
           
            GotoPage(typeof(Views.Pages.DishesPage));
        }
    }
}
