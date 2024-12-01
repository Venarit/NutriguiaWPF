using System.ComponentModel;

namespace UiNutriguia.Models;

public class DishFoodModel : BaseModel
{
    public int IdDishFood {  get; set; }
    public int IdDish {  get; set; }
    public int IdFood { get; set; }
    
    private decimal _equivalent;
    private decimal _quantity;
    private int _kcal;
    private decimal _protein;
    private decimal _lipids;
    private decimal _hco;
    private FoodModel _food;

    public decimal Equivalent
    {
        get => _equivalent;
        set
        {
            if (SetProperty(ref _equivalent, value))
            {
                UpdateNutrients();
            }
        }
    }
    public decimal Quantity
    {
        get => _quantity;
        set => SetProperty(ref _quantity, value);
    }
    public int Kcal
    {
        get => _kcal;
        set => SetProperty(ref _kcal, value);
    }
    public decimal Protein
    {
        get => _protein;
        set => SetProperty(ref _protein, value);
    }
    public decimal Lipids
    {
        get => _lipids;
        set => SetProperty(ref _lipids, value);
    }
    public decimal Hco
    {
        get => _hco;
        set => SetProperty(ref _hco, value);
    }
    public FoodModel Food
    {
        get => _food;
        set
        {
            if (_food != null)
                _food.PropertyChanged -= Food_PropertyChanged;

            SetProperty(ref _food, value);

            if (_food != null)
                _food.PropertyChanged += Food_PropertyChanged;

            UpdateNutrients();
        }
    }

    private void Food_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(FoodModel.Quantity) ||
            e.PropertyName == nameof(FoodModel.Energy) ||
            e.PropertyName == nameof(FoodModel.Lipids) ||
            e.PropertyName == nameof(FoodModel.Protein) ||
            e.PropertyName == nameof(FoodModel.Hco))
        {
            UpdateNutrients();
        }
    }

    private void UpdateNutrients()
    {
        if (_food != null && _equivalent > 0)
        {
            if (_food.IdFood != 0)
            {
                Quantity = (decimal)(_food.Quantity * _equivalent);
                Kcal = (int)(_food.Energy * _equivalent);
                Lipids = (decimal)(_food.Lipids * _equivalent);
                Protein = (decimal)(_food.Protein * _equivalent);
                Hco = (decimal)(_food.Hco * _equivalent);
            }
        }
        else
        {
            Quantity = 0;
            Kcal = 0;
            Lipids = 0;
            Protein = 0;
            Hco = 0;
        }
    }
}
