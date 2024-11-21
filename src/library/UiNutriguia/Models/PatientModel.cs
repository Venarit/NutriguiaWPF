namespace UiNutriguia.Models
{
    public class PatientModel : BaseModel
    {
        public int IdPatient { get; set; }
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string LastNameP { get; set; }
        public string? LastNameM { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string BirthDate { get; set; }
        public NutritionalProfileModel? NutritionalProfile { get; set; }
    }
}
