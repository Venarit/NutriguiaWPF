namespace Nutriguia.Model.Models
{
    public class MacronutrientModel : BaseCatalogModel
    {
        private decimal hco;
        private decimal lipids;
        private decimal protein;

        public decimal Hco
        {
            get
            {
                return this.hco;
            }
            set
            {
                SetProperty(ref this.hco, value);
            }
        }
        public decimal Lipids
        {
            get
            {
                return this.lipids;
            }
            set
            {
                SetProperty(ref this.lipids, value);
            }
        }
        public decimal Protein
        {
            get
            {
                return this.protein;
                ;
            }
            set
            {
                SetProperty(ref this.protein, value);
            }
        }
    }
}
