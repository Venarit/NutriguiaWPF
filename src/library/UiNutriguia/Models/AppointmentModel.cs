namespace UiNutriguia.Models
{
    public class AppointmentModel : BaseModel
    {
        public int IdAppointment { get; set; }
        public int IdPatient { get; set; }
        public int IdAppointmentStatus { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string? Notes { get; set; }
        public AppointmentStatusModel AppointmentStatus { get; set; }
        public PatientModel? Patient { get; set; }
    }
}
