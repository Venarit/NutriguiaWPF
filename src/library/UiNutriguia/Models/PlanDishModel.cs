namespace UiNutriguia.Models;

public class PlanDishModel : BaseModel
{
    public int IdPlanDish { get; set; }
    public int IdDish { get; set; }
    private DishModel _dish;
    public DishModel Dish
    {
        get => _dish;
        set
        {
            if (_dish != value)
            {
                _dish = value;
                OnPropertyChanged(nameof(Dish));
            }
        }
    }
}
