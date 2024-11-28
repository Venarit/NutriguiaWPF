namespace UiNutriguia.Models;

public class DishFoodModel : BaseModel
{
    public int IdDishFood {  get; set; }
    public int IdDish {  get; set; }
    public int IdFood { get; set; }
    public decimal Equivalent {  get; set; }
    public decimal Quantity { get; set; }
    public int Kcal {  get; set; }
    public decimal Protein { get; set; }
    public decimal Lipids { get; set; }
    public decimal Hco { get; set; }
    public FoodModel Food { get; set; }
}
