namespace Nutriguia.Model.Models
{
    public class BaseModel : BindableBase
    {
        private int id;
        private bool? active;
        private DateTimeOffset? insDateTime;
        private DateTimeOffset? updDateTime;

        public int Id 
        { 
            get
            {
                return this.id;
            }
            set
            {
                SetProperty(ref this.id, value);
            }
        }
        public bool? Active
        {
            get
            {
                return this.active;
            }
            set
            {
                SetProperty(ref this.active, value);
            }
        }
        public DateTimeOffset? InsDateTime
        {
            get
            {
                return this.insDateTime;
            }
            set
            {
                SetProperty(ref this.insDateTime, value);
            }
        }
        public DateTimeOffset? UpdDateTime
        {
            get
            {
                return this.updDateTime;
            }
            set
            {
                SetProperty(ref this.updDateTime, value);
            }
        }
    }
}
