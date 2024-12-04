namespace UiNutriguia.Models;

public class PlanPatientModel : BaseModel
{
    public int IdPlanPatient { get; set; }
    public int IdPlan { get; set; }
    public int IdPatient { get; set; }
    public PlanModel PlanModel { get; set; }
    public PatientModel PatientModel { get; set; }
}
