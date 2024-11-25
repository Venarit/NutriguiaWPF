
namespace UiNutriguia.Models
{
    public class NutritionalProfileModel : BaseModel
    {
        public int IdNutritionalProfile { get; set; }
        public int IdPatient { get; set; }
        public int Height { get; set; }
        public  string Sex { get; set; }
        public int IdActivity { get; set; }
        public int IdObjective { get; set; }
        public int IdMacronutrients { get; set; }
        public  ActivityModel Activity { get; set; }
        public  ObjectiveModel Objective { get; set; }
        public  MacronutrientModel Macronutrient { get; set; }
        public PatientMeasurementModel? PatientMeasurement { get; set; }
    }
}
