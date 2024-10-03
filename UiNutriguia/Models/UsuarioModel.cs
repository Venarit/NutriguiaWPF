namespace UiNutriguia.Models
{
    public class UsuarioModel : BaseModel
    {
        public required string User { get; set; }
        protected string Password { get; set; }
    }
}
