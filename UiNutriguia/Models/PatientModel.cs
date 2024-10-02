namespace UiNutriguia.Models
{
    public class PatientModel : BaseModel
    {
        public required string Name { get; set; }
        public string? SecondName { get; set; }
        public required string LastNameP { get; set; }
        public string? LastNameM { get; set; }
        public required string Email { get; set; }
        public required string Cellphone { get; set; }
        public required string BirthDate { get; set; }
    }
}
