namespace Nutriguia.Model.Models
{
    public class PatientMeasurementModel: BaseModel
    {
        private int idNutritionalProfile;
        private decimal weight;
        private int? bodyFat;
        private int? calories;
        private int? bmr;
        private int? tdee;

        public required int IdNutritionalProfile
        {
            get
            {
                return this.idNutritionalProfile;
            }
            set
            {
                SetProperty(ref this.idNutritionalProfile, value);
            }
        }
        public decimal Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                SetProperty(ref this.weight, value);
            }
        }
        public int? BodyFat
        {
            get
            {
                return this.bodyFat;
            }
            set
            {
                SetProperty(ref this.bodyFat, value);
            }
        }
        public int? Calories
        {
            get
            {
                return this.calories;
            }
            set
            {
                SetProperty(ref this.calories, value);
            }
        }
        public int? BMR
        {
            get
            {
                return this.bmr;
            }
            set
            {
                SetProperty(ref this.bmr, value);
            }
        }
        public int? TDEE
        {
            get
            {
                return this.tdee;
            }
            set
            {
                SetProperty(ref this.tdee, value);
            }
        }
    }
}
