namespace UiNutriguia.Models;

public class PlanModel : BaseModel
{
    public int IdPlan {  get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int IdPlanOption_1 { get; set; }
    public int IdPlanOption_2 { get; set; }
    public int IdPlanOption_3 { get; set; }
    public PlanOptionModel PlanOptionModel1 { get; set; }
    public PlanOptionModel PlanOptionModel2 { get; set; }
    public PlanOptionModel PlanOptionModel3 { get; set; }
}
