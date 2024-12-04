using System.ComponentModel;

namespace UiNutriguia.Models;

public class PlanOptionModel : BaseModel
{
    public int IdPlanOption {  get; set; }
    public int Breakfast {  get; set; }
    public int Collation1 { get; set; }
    public int Meal { get; set; }
    public int Collation2 { get; set; }
    public int Dinner { get; set; }

    private int _kcal;
    public int Kcal
    {
        get => _kcal;
        private set
        {
            _kcal = value;
            OnPropertyChanged(nameof(Kcal));
        }
    }

    private decimal _protein;
    public decimal Protein
    {
        get => _protein;
        private set
        {
            _protein = value;
            OnPropertyChanged(nameof(Protein));
        }
    }
    
    private decimal _lipids;
    public decimal Lipids
    {
        get => _lipids;
        private set
        {
            _lipids = value;
            OnPropertyChanged(nameof(Lipids));
        }
    }
    
    private decimal _hco;
    public decimal Hco
    {
        get => _hco;
        private set
        {
            _hco = value;
            OnPropertyChanged(nameof(Hco));
        }
    }

    private PlanDishModel _breakfastModel;
    public PlanDishModel BreakfastModel
    {
        get => _breakfastModel;
        set => SetDishModel(ref _breakfastModel, value, nameof(BreakfastModel));
    }


    private PlanDishModel _collation1Model;
    public PlanDishModel Collation1Model
    {
        get => _collation1Model;
        set => SetDishModel(ref _collation1Model, value, nameof(Collation1Model));
    }

    private PlanDishModel _mealModel;
    public PlanDishModel MealModel
    {
        get => _mealModel;
        set => SetDishModel(ref _mealModel, value, nameof(MealModel));
    }

    private PlanDishModel _collation2Model;
    public PlanDishModel Collation2Model
    {
        get => _collation2Model;
        set => SetDishModel(ref _collation2Model, value, nameof(Collation2Model));
    }

    private PlanDishModel _dinnerModel;
    public PlanDishModel DinnerModel
    {
        get => _dinnerModel;
        set => SetDishModel(ref _dinnerModel, value, nameof(DinnerModel));
    }

    public void RecalculateNutrients()
    {
        var models = new List<PlanDishModel> { BreakfastModel, Collation1Model, MealModel, Collation2Model, DinnerModel };
        Kcal = (int)models.Sum(m => m?.Dish?.Kcal ?? 0);
        Protein = models.Sum(m => m?.Dish?.Protein ?? 0);
        Lipids = models.Sum(m => m?.Dish?.Lipids ?? 0);
        Hco = models.Sum(m => m?.Dish?.Hco ?? 0);
    }

    private void SetDishModel(ref PlanDishModel field, PlanDishModel value, string propertyName)
    {
        if (field != value)
        {
            if (field != null) field.PropertyChanged -= OnDishChanged;
            field = value;
            if (field != null) field.PropertyChanged += OnDishChanged;
            OnPropertyChanged(propertyName);
            RecalculateNutrients();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void OnDishChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PlanDishModel.Dish))
        {
            RecalculateNutrients();
        }
    }

    public void Dispose()
    {
        BreakfastModel.PropertyChanged -= OnDishChanged;
        Collation1Model.PropertyChanged -= OnDishChanged;
        MealModel.PropertyChanged -= OnDishChanged;
        Collation2Model.PropertyChanged -= OnDishChanged;
        DinnerModel.PropertyChanged -= OnDishChanged;
    }

}
