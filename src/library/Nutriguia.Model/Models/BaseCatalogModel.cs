namespace Nutriguia.Model.Models
{
    public class BaseCatalogModel : BaseModel
    {
        private string code;
        private string? name;
        private string? description;

        public required string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                SetProperty(ref this.code, value);
            }
        }
        public string? Name
        {
            get
            {
                return this.name;
            }
            set
            {
                SetProperty(ref this.name, value);
            }
        }
        public string? Description
        {
            get
            {
                return this.description;
            }
            set
            {
                SetProperty(ref this.description, value);
            }
        }
    }
}
