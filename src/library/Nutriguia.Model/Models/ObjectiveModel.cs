namespace Nutriguia.Model.Models
{
    public class ObjectiveModel : BaseCatalogModel
    {
        private int calories;
        public int Calories
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
    }
}
