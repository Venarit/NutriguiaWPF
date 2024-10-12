using Microsoft.Data.SqlClient;
using Nutriguia.Model.Models;

namespace Nutriguia.Model.DataAccess
{
    public partial class DataAccess
    {
        private partial class StoredProcedures
        {
            internal const string SpGetPatients = "[Paciente].[GetPacientes]";
            internal const string SpSetPatients = "[Paciente].[SetPacientes]";
            internal const string SpGetNutritionalProfiles = "[Paciente].[GetPerfilesNutricionales]";
            internal const string SpSetNutritionalProfile = "[Paciente].[SetPerfilNutricional]";
            internal const string SpGetPatientMeasurements = "[Paciente].[GetPacienteMediciones]";
            internal const string SpSetPatientMeasurements = "[Paciente].[SetPacienteMediciones]";
        }

        public List<PatientModel> GetPatients()
        {
            List<PatientModel> returnValue;
            List<NutritionalProfileModel> nutritionalProfiles = GetNutritionalProfiles();

            returnValue = this.Query<PatientModel>(StoredProcedures.SpGetPatients, null);
            if (nutritionalProfiles != null)
            {
                foreach (var patient in returnValue)
                {
                    patient.NutritionalProfile = nutritionalProfiles.FirstOrDefault(n => n.IdPatient == patient.Id);
                }
            }
            return returnValue;
        }
        public List<NutritionalProfileModel> GetNutritionalProfiles()
        {
            List<NutritionalProfileModel> returnValue;
            using (var connection = this.GetSqlConnection())
            {
                using (var multiple = this.QueryMultiple(connection, StoredProcedures.SpGetNutritionalProfiles, null))
                {
                    returnValue = multiple.Reader.Read<NutritionalProfileModel>().ToList();
                    var activities = multiple.Reader.Read<ActivityModel>().ToList();
                    var objectives = multiple.Reader.Read<ObjectiveModel>().ToList();
                    var macros = multiple.Reader.Read<MacronutrientModel>().ToList();
                    var measurements = multiple.Reader.Read<PatientMeasurementModel>().ToList();

                    foreach (var profile in returnValue)
                    {
                        var patientMeasurements = measurements.Where(m => m.Id == profile.Id);
                        if (patientMeasurements != null)
                        {
                            profile.PatientMeasurement = (List<PatientMeasurementModel>)patientMeasurements;
                        }

                        if (activities != null)
                        {
                            profile.Activity = activities.FirstOrDefault(a => a.Id == profile.IdActivity);
                        }

                        if (objectives != null)
                        {
                            profile.Objective = objectives.FirstOrDefault(o => o.Id == profile.IdObjective);
                        }

                        if (macros != null)
                        {
                            profile.Macronutrient = macros.FirstOrDefault(m => m.Id == profile.IdMacronutrient);
                        }
                    }
                }
            }
            return returnValue;
        }
        public List<PatientMeasurementModel> GetPatientMeasurements()
        {
            var returnValue = this.Query<PatientMeasurementModel>(StoredProcedures.SpGetPatientMeasurements, null);
            return returnValue;
        }

        public void SetPatient(PatientModel patient)
        {
            this.ExecuteNonQuery(StoredProcedures.SpSetPatients, new
            {
                @Nombre             = patient.Name,
                @SegundoNombre      = patient.SecondName,
                @ApellidoP          = patient.LastNameP,
                @ApellidoM          = patient.LastNameM,
                @Email              = patient.Email,
                @Celular            = patient.Cellphone,
                @FechaNacimiento    = patient.BirthDate,
            });
        }

        public void SetPatientNutritionalProfile(NutritionalProfileModel nutritionalProfile)
        {
            this.ExecuteNonQuery(StoredProcedures.SpSetNutritionalProfile, new
            {
                @idPaciente        = nutritionalProfile.IdPatient,
                @Altura            = nutritionalProfile.Height,
                @Sexo              = nutritionalProfile.Sex,
                @idActividad       = nutritionalProfile.IdActivity,
                @idObjetivo        = nutritionalProfile.IdObjective,
                @idMacronutrientes = nutritionalProfile.IdMacronutrient,
            });
        }

    }
}
