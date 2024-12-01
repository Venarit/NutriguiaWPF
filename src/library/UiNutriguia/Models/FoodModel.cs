namespace UiNutriguia.Models;

public class FoodModel : BaseModel
{
    public int IdFood { get; set; }
    public int IdFoodType { get; set; }

    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    public decimal? Quantity { get; set; }
    public int IdUnit { get; set; }
    public decimal? GrossWeight { get; set; }
    public decimal? NetWeight { get; set; }
    public decimal? Energy { get; set; }
    public decimal? Protein { get; set; }
    public decimal? Lipids { get; set; }
    public decimal? Hco { get; set; }
    public decimal? Fiber { get; set; }
    public decimal? VitaminA { get; set; }
    public decimal AscorbicAcid { get; set; }
    public decimal? FolicAcid { get; set; }
    public decimal? Iron { get; set; }
    public decimal? Potasium { get; set; }
    public decimal? GlycemicIndex { get; set; }
    public decimal? GlycemicLoad { get; set; }
    public decimal? Sugar { get; set; }
    public decimal? Sodium { get; set; }
    public decimal? Calcium { get; set; }
    public decimal? Selenium { get; set; }
    public decimal? Phosphorus { get; set; }
    public decimal? Colesterol { get; set; }
    public FoodTypeModel Type { get; set; }
    public FoodEguModel Unit { get; set; }
}
