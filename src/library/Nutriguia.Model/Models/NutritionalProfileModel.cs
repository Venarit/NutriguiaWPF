
namespace Nutriguia.Model.Models
{
    public class NutritionalProfileModel : BaseModel
    {
        private int idPatient;
        private int height;
        private string sex;
        private int idActivity;
        private int idObjective;
        private int idMacronutrient;

        public int IdPatient
        {
            get
            {
                return this.idPatient;
            }
            set
            {
                SetProperty(ref this.idPatient, value);
            }
        }
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                SetProperty(ref this.height, value);
            }
        }
        public required string Sex
        {
            get
            {
                return this.sex;
            }
            set
            {
                SetProperty(ref this.sex, value);
            }
        }
        public required int IdActivity
        {
            get
            {
                return this.idActivity;
            }
            set
            {
                SetProperty(ref this.idActivity, value);
            }
        }
        public required int IdObjective
        {
            get
            {
                return this.idObjective;
            }
            set
            {
                SetProperty(ref this.idObjective, value);
            }
        }
        public required int IdMacronutrient
        {
            get
            {
                return this.idMacronutrient;
            }
            set
            {
                SetProperty(ref this.idMacronutrient, value);
            }
        }

        public ActivityModel Activity
        {
            get;
            set;
        }
        public ObjectiveModel Objective
        {
            get;
            set;
        }
        public MacronutrientModel Macronutrient
        {
            get;
            set;
        }
        public List<PatientMeasurementModel>? PatientMeasurement
        {
            get;
            set;
        }
    }
}
