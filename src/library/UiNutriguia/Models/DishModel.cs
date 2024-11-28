namespace UiNutriguia.Models;

public class DishModel : BaseCatalogModel
{
    public int IdDish { get; set; }
    public int IdDishType { get; set; }
    public int Kcal { get; set; }
    public decimal Protein { get; set; }
    public decimal Lipids { get; set; }
    public decimal Hco { get; set; }
    public DishTypeModel DishTypeModel { get; set; }
    public List<DishFoodModel> DishFoodModel { get; set; }
}
