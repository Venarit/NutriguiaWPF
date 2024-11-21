using Nutriguia.Model.DataAccess;
using Wpf.Ui.Controls;
using Wpf.Ui;
using UiNutriguia.Models;
using System.Collections.ObjectModel;

namespace UiNutriguia.ViewModels.Pages;

public partial class FoodViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private DataAccess dataAccess;
    private FoodTypeModel _selectedFoodType;
    private FoodEguModel _selectedFoodUnit;
    private string _filterText;

    #region Observable Properties

    [ObservableProperty] private IEnumerable<FoodModel> _foods;
    [ObservableProperty] private ObservableCollection<FoodTypeModel> _foodTypes;
    [ObservableProperty] private ObservableCollection<FoodEguModel> _units;

    #endregion

    public FoodTypeModel SelectedFoodType 
    { 
        get => _selectedFoodType;
        set 
        { 
            SetProperty(ref _selectedFoodType, value);
            ApplyFilter();
        }
    }

    public FoodEguModel SelectedFoodUnit
    {
        get => _selectedFoodUnit;
        set
        {
            SetProperty(ref _selectedFoodUnit, value);
            ApplyFilter();
        }
    }

    public string FilterText
    {
        get => _filterText;
        set
        {
            SetProperty(ref _filterText, value);
            ApplyFilter();
        }
    }

    #region Commands

    [RelayCommand]
    private void GotoPage(Type type)
    {
        _ = navigationService.Navigate(type);
    }

    #endregion

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

        this.FoodTypes = new ObservableCollection<FoodTypeModel>();
        this.Units = new ObservableCollection<FoodEguModel>();
        FillComboBoxes();

        _isInitialized = true;
    }

    public void FillComboBoxes()
    {
        var types = this.dataAccess.GetFoodTypes();
        this.FoodTypes.Clear();
        foreach (var type in types)
        { 
            this.FoodTypes.Add(type);
        }
        this.FoodTypes.Add(new FoodTypeModel { IdFoodType = 0, Code = " " });

        var unitsList = this.dataAccess.GetFoodUnits();
        this.Units.Clear();
        foreach (var unit in unitsList)
        {
            this.Units.Add(unit);
        }
        this.Units.Add(new FoodEguModel { IdUnit = 0, Code = " " });
    }

    public void ApplyFilter()
    {
        var foodCollection = new List<FoodModel>();

        var foodTypeId = SelectedFoodType?.IdFoodType;
        var unitId = SelectedFoodUnit?.IdUnit;

        foodCollection = this.dataAccess.GetFoods(foodTypeId == 0 ? null : foodTypeId, unitId == 0 ? null : unitId);

        if (!string.IsNullOrEmpty(FilterText))
        {
            foodCollection = foodCollection
                .Where(f => f.Name != null && f.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        Foods = foodCollection;
    }
}
