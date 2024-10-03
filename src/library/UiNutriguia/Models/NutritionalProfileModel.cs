namespace UiNutriguia.Models
{
    public class NutritionalProfileModel : BaseModel
    {
        PatientModel Patient { get; set; }
        public int IdUser { get; set; }
        public int Height { get; set; }
        public int Sex { get; set; }
        ActivityModel Activity { get; set; }
        ObjectiveModel Objective { get; set; }
        MacronutrientModel Macronutrient { get; set; }
    }
}
