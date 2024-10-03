namespace UiNutriguia.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public DateTimeOffset? InsDateTime { get; set; }
        public DateTimeOffset? UpdDateTime { get; set; }
    }
}
