using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiNutriguia.Models
{
    public class PatientMeasurementModel: BaseModel
    {
        public required int IdNutritionalProfile { get; set; }
        public decimal Weight { get; set; }
        public int? BodyFat { get; set; }
        public int? Calories { get; set; }
        public int? BMR { get; set; }
        public int? TDEE { get; set; }
    }
}
