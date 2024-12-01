namespace UiNutriguia.Models
{
    public class BaseModel : ObservableObject
    {
        public bool? Active { get; set; }
        public DateTimeOffset? InsDateTime { get; set; }
        public DateTimeOffset? UpdDateTime { get; set; }
    }
}
