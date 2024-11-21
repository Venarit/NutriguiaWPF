using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiNutriguia.Models
{
    public class ObjectiveModel : BaseCatalogModel
    {
        public required int IdObjective { get; set; }
        public int Calories { get; set; }
    }
}
