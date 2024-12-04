using Nutriguia.Model.DataAccess;
using Wpf.Ui.Controls;
using Wpf.Ui;
using UiNutriguia.Models;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using UiNutriguia.Views.Pages;

namespace UiNutriguia.ViewModels.Pages;

public partial class DishesViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private DataAccess dataAccess;
    private DishTypeModel _selectedDishType;
    private DishModel _selectedDish;
    private string _filterText;

    #region Observable Properties
    [ObservableProperty] private SeriesCollection _seriesCollection;
    [ObservableProperty] private IEnumerable<DishModel> _dishes;
    [ObservableProperty] private ObservableCollection<DishTypeModel> _dishTypes;
    #endregion

    public DishTypeModel SelectedDishType
    {
        get => _selectedDishType;
        set
        {
            SetProperty(ref _selectedDishType, value);
            ApplyFilter();
        }
    }

    public DishModel SelectedDish
    {
        get => _selectedDish;
        set
        {
            SetProperty(ref _selectedDish, value);
            GetDishFoods();
            GetSelectedDishChart();
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

    [RelayCommand]
    private void GotoPage(Type type)
    {
        _ = navigationService.Navigate(type);
    }

    [RelayCommand]
    private void GotoEdit()
    {
        if (SelectedDish != null)
        {
            NavigationContext.Parameter = SelectedDish;
            _ = navigationService.Navigate(typeof(DishesAddPage));
        }
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

        var dishesList = new List<DishModel>();
        dishesList = this.dataAccess.GetDishes();
        Dishes = dishesList;

        this.DishTypes = new ObservableCollection<DishTypeModel>();
        FillComboBoxes();

        _isInitialized = true;
    }

    public void Reload()
    {
        var dishesList = new List<DishModel>();
        dishesList = this.dataAccess.GetDishes();
        Dishes = dishesList;
    }
    
    public void FillComboBoxes()
    {
        //var dishTypes = this.dataAccess.GetDishTypes();
        //this.DishTypes.Clear();
        //this.DishTypes.Add(new DishTypeModel { IdDishType = 0, Code = " " });
        //foreach (var type in dishTypes)
        //{
        //    this.DishTypes.Add(type);
        //}
    }

    public void ApplyFilter()
    {
        var dishesList = new List<DishModel>();


        dishesList = this.dataAccess.GetDishes();

        if (!string.IsNullOrEmpty(FilterText))
        {
            dishesList = dishesList
                .Where(f => f.Name != null && f.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        Dishes = dishesList;
    }

    public void GetDishFoods()
    {
        if (SelectedDish != null)
        {
            SelectedDish.DishFoodModel = new ObservableCollection<DishFoodModel>();
            var dishFoods = this.dataAccess.GetDishFoods(SelectedDish.IdDish);
            foreach (var food in dishFoods)
            {
                SelectedDish.DishFoodModel.Add(food);
            }
        }
    }

    public void GetSelectedDishChart()
    {
        if (SelectedDish != null)
        {
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Carbohidratos",
                    Values = new ChartValues<ObservableValue> { new ObservableValue((double)SelectedDish.Hco) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Proteinas",
                    Values = new ChartValues<ObservableValue> { new ObservableValue((double)SelectedDish.Protein) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Grasas",
                    Values = new ChartValues<ObservableValue> { new ObservableValue((double)SelectedDish.Lipids) },
                    DataLabels = true
                }
            };
        }
    }

    [RelayCommand]
    public void DeleteDish()
    {
        this.dataAccess.DeleteDish(SelectedDish.IdDish);
        Reload();
    }

}
