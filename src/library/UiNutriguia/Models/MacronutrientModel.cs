using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiNutriguia.Models
{
    public class MacronutrientModel : BaseCatalogModel
    {
        public required int IdMacronutrients { get; set; }
        public decimal Hco { get; set; }
        public decimal Lipids { get; set; }
        public decimal Protein { get; set; }
    }
}
