using System.DirectoryServices.ActiveDirectory;
using UiNutriguia.Enums;

namespace UiNutriguia.Models
{
    public class AppointmentModel : BaseModel
    {
        public required int IdPatient { get; set; }
        public AppointmentStatus Status { get; set; }
        public required DateOnly Date { get; set; }
        public required HourOfDay StartHour { get; set; }
        public required HourOfDay EndHour { get; set; }
        public string? Notes { get; set; }
    }
}
