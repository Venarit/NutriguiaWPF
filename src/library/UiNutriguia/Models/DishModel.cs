using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UiNutriguia.Models;

public class DishModel : BaseCatalogModel
{
    private int _kcal;
    private decimal _protein;
    private decimal _lipids;
    private decimal _hco;
    private ObservableCollection<DishFoodModel> _dishFoodModel;

    public int IdDish { get; set; }
    public int IdDishType { get; set; }
    public DishTypeModel DishTypeModel { get; set; }

    public int Kcal
    {
        get => _kcal;
        private set
        {
            _kcal = value;
            OnPropertyChanged(nameof(Kcal));
        }
    }
    public decimal Protein
    {
        get => _protein;
        private set
        {
            _protein = value;
            OnPropertyChanged(nameof(Protein));
        }
    }
    public decimal Lipids
    {
        get => _lipids;
        private set
        {
            _lipids = value;
            OnPropertyChanged(nameof(Lipids));
        }
    }
    public decimal Hco
    {
        get => _hco;
        private set
        {
            _hco = value;
            OnPropertyChanged(nameof(Hco));
        }
    }
    public ObservableCollection<DishFoodModel> DishFoodModel
    {
        get => _dishFoodModel;
        set
        {
            if (_dishFoodModel != null)
                _dishFoodModel.CollectionChanged -= DishFoodModel_CollectionChanged;

            _dishFoodModel = value;

            if (_dishFoodModel != null)
                _dishFoodModel.CollectionChanged += DishFoodModel_CollectionChanged;

            UpdateNutritionalValues();
        }
    }
    public DishModel()
    {
        DishFoodModel = new ObservableCollection<DishFoodModel>();
        DishFoodModel.CollectionChanged += DishFoodModel_CollectionChanged;
    }

    private void DishFoodModel_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        UpdateNutritionalValues();
    }

    private void UpdateNutritionalValues()
    {
        Kcal = DishFoodModel.Sum(df => df.Kcal);
        Protein = DishFoodModel.Sum(df => df.Protein);
        Lipids = DishFoodModel.Sum(df => df.Lipids);
        Hco = DishFoodModel.Sum(df => df.Hco);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
