namespace UiNutriguia.Models
{
    public class NutritionalProfileModel : BaseModel
    {
        public int idPatient { get; set; }
        //public int IdUser { get; set; }
        public int Height { get; set; }
        public required string Sex { get; set; }
        public required ActivityModel Activity { get; set; }
        public required ObjectiveModel Objective { get; set; }
        public required MacronutrientModel Macronutrient { get; set; }
        public PatientMeasurementModel? PatientMeasurement { get; set; }
    }
}
