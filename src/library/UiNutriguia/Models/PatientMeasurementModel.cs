using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiNutriguia.Models
{
    public class PatientMeasurementModel: BaseModel
    {
        public int IdPatientMeasurementModel { get; set; }
        public int IdNutritionalProfile { get; set; }
        public decimal Weight { get; set; }
        public decimal BodyFat { get; set; }
        public int? BMR { get; set; }
        public int? TDEE { get; set; }
    }
}
