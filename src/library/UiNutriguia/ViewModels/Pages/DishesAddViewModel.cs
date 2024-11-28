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

    [ObservableProperty] private IEnumerable<FoodModel> _foods;
    [ObservableProperty] private DishModel _dish;
    [ObservableProperty] private ObservableCollection<DishTypeModel> _dishTypes;

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
        this.dataAccess = new DataAccess();

        var foodCollection = new List<FoodModel>();
        foodCollection = this.dataAccess.GetFoods(null, null);
        Foods = foodCollection;

        this.Dish = new DishModel();
        this.DishTypes = new ObservableCollection<DishTypeModel>();

        FillComboBoxes();

        _isInitialized = true;
    }

    private void FillComboBoxes()
    {
        var dishTypes = this.dataAccess.GetDishTypes();
        this.DishTypes.Clear();
        this.DishTypes.Add(new DishTypeModel { IdDishType = 0, Code = " " });
        foreach (var type in dishTypes)
        {
            this.DishTypes.Add(type);
        }
    }
}
