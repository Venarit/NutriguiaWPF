using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using UiNutriguia.Models;
using System.Configuration;
using System.Diagnostics;

namespace Nutriguia.Model.DataAccess
{
    public class DataAccess
    {
        #region Stored Procedures
        private const string SpGetPatients = "[Paciente].[GetPacientes]";
        private const string SpSetPatients = "[Paciente].[SetPacientes]";
        private const string SpGetNutritionalProfiles = "[Paciente].[GetPerfilesNutricionales]";
        private const string SpGetPatientMeasurements = "[Paciente].[GetPacienteMediciones]";
        private const string SpGetActivity = "[Catalogo].[GetActividad]";
        private const string SpGetMacronutrients = "[Catalogo].[GetMacronutrientes]";
        private const string SpGetObjectives = "[Catalogo].[GetObjetivo]";
        #endregion

        private SqlConnection connection;

        public DataAccess()
        {
            try
            {
                connection = new SqlConnection(GetConnectionString());
                connection.Open();

                Console.WriteLine("State: {0}", connection.State);
                Console.WriteLine("ConnectionString: {0}", connection.ConnectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        }

        public List<PatientModel> GetPatients()
        {
            List<PatientModel> patients = new List<PatientModel>();
            List<NutritionalProfileModel> nutritionalProfiles = GetNutritionalProfiles();

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand(SpGetPatients, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int secondNameIndex = reader.GetOrdinal("SegundoNombre");
                        int lastNameMIndex = reader.GetOrdinal("Apellido_M");

                        while (reader.Read())
                        {
                            PatientModel patient = new PatientModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("idPaciente")),
                                Name = reader.GetString(reader.GetOrdinal("Nombre")),
                                SecondName = reader.IsDBNull(secondNameIndex) ? null : reader.GetString(secondNameIndex),
                                LastNameP = reader.GetString(reader.GetOrdinal("Apellido_P")),
                                LastNameM = reader.IsDBNull(lastNameMIndex) ? null : reader.GetString(lastNameMIndex),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Cellphone = reader.GetString(reader.GetOrdinal("Celular")),
                                BirthDate = reader.GetString(reader.GetOrdinal("FechaNacimiento")),
                                Active = reader.GetBoolean(reader.GetOrdinal("Activo"))
                            };

                            patient.NutritionalProfile = nutritionalProfiles.FirstOrDefault(x => x.idPatient == patient.Id);

                            patients.Add(patient);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving patients: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return patients;
        }
        public List<NutritionalProfileModel> GetNutritionalProfiles()
        {
            List<NutritionalProfileModel> nutritionalProfiles = new List<NutritionalProfileModel>();

            try
            {
                List<ActivityModel> activities = GetActivities();
                List<ObjectiveModel> objectives = GetObjectives();
                List<MacronutrientModel> macronutrients = GetMacronutrients();

                List<PatientMeasurementModel> patientMeasurements = GetPatientMeasurements();

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand(SpGetNutritionalProfiles, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NutritionalProfileModel nutritionalProfile = new NutritionalProfileModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("idPerfilNutricional")),
                                idPatient = reader.GetInt32(reader.GetOrdinal("idPaciente")),
                                Height = reader.GetInt32(reader.GetOrdinal("Altura")),
                                Sex = reader.GetString(reader.GetOrdinal("Sexo")),
                                Activity = activities.FirstOrDefault(a => a.Id == reader.GetInt32(reader.GetOrdinal("idActividad"))),
                                Objective = objectives.FirstOrDefault(o => o.Id == reader.GetInt32(reader.GetOrdinal("idObjetivo"))),
                                Macronutrient = macronutrients.FirstOrDefault(m => m.Id == reader.GetInt32(reader.GetOrdinal("idMacronutrientes"))),
                                Active = reader.GetBoolean(reader.GetOrdinal("Activo"))
                            };

                            nutritionalProfile.PatientMeasurement = patientMeasurements.FirstOrDefault(m => m.IdNutritionalProfile == nutritionalProfile.Id);

                            nutritionalProfiles.Add(nutritionalProfile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving patient nutritional profiles : " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return nutritionalProfiles;
        }
        public List<PatientMeasurementModel> GetPatientMeasurements()
        {
            List<PatientMeasurementModel> patientMeasurements = new List<PatientMeasurementModel>();

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand(SpGetPatientMeasurements, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bodyfatIndex = reader.GetOrdinal("GrasaCorporal");
                            int caloriesIndex = reader.GetOrdinal("Calorias");
                            int bmrIndex = reader.GetOrdinal("BMR");
                            int tdeIndex = reader.GetOrdinal("TDEE");

                            PatientMeasurementModel patientMeasurement = new PatientMeasurementModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("idPacienteMedicion")),
                                IdNutritionalProfile = reader.GetInt32(reader.GetOrdinal("idPerfilNutricional")),
                                Weight = reader.GetDecimal(reader.GetOrdinal("Peso")),
                                BodyFat = reader.IsDBNull(bodyfatIndex) ? null : reader.GetInt32(bodyfatIndex),
                                Calories = reader.IsDBNull(caloriesIndex) ? null : reader.GetInt32(caloriesIndex),
                                BMR = reader.IsDBNull(bmrIndex) ? null : reader.GetInt32(bmrIndex),
                                TDEE = reader.IsDBNull(tdeIndex) ? null : reader.GetInt32(tdeIndex),
                                Active = reader.GetBoolean(reader.GetOrdinal("Activo"))
                            };
                            patientMeasurements.Add(patientMeasurement);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving patient measurements : " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return patientMeasurements;
        }

        public void SetPatients(PatientModel patient)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand(SpSetPatients, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Nombre", patient.Name));
                    cmd.Parameters.Add(new SqlParameter("@SegundoNombre", patient.SecondName));
                    cmd.Parameters.Add(new SqlParameter("@ApellidoP", patient.LastNameP));
                    cmd.Parameters.Add(new SqlParameter("@ApellidoM", patient.LastNameM));
                    cmd.Parameters.Add(new SqlParameter("@Email", patient.Email));
                    cmd.Parameters.Add(new SqlParameter("@Celular", patient.Cellphone));
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", patient.BirthDate));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving patients: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        #region Get Catalogs

        public List<ActivityModel> GetActivities()
        {
            List<ActivityModel> activities = new List<ActivityModel>();

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand(SpGetActivity, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ActivityModel activity = new ActivityModel
                            { 
                                Id = reader.GetInt32(reader.GetOrdinal("idActividad")),
                                Code = reader.GetString(reader.GetOrdinal("Codigo")),
                                Name = reader.GetString(reader.GetOrdinal("Nombre")),
                                Description = reader.GetString(reader.GetOrdinal("Descripcion")),
                                Factor = reader.GetDecimal(reader.GetOrdinal("Factor")),
                                Active = reader.GetBoolean(reader.GetOrdinal("Activo"))
                            };

                            activities.Add(activity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving activities : " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return activities;
        }

        public List<ObjectiveModel> GetObjectives()
        {
            List<ObjectiveModel> objectives = new List<ObjectiveModel>();

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand(SpGetObjectives, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ObjectiveModel objective = new ObjectiveModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("idObjetivo")),
                                Code = reader.GetString(reader.GetOrdinal("Codigo")),
                                Name = reader.GetString(reader.GetOrdinal("Nombre")),
                                Description = reader.GetString(reader.GetOrdinal("Descripcion")),
                                Calories = reader.GetInt32(reader.GetOrdinal("Calorias")),
                                Active = reader.GetBoolean(reader.GetOrdinal("Activo"))
                            };

                            objectives.Add(objective);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving objectives : " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return objectives;
        }

        public List<MacronutrientModel> GetMacronutrients()
        {
            List<MacronutrientModel> macronutrients = new List<MacronutrientModel>();

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (SqlCommand cmd = new SqlCommand(SpGetMacronutrients, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MacronutrientModel macronutrient = new MacronutrientModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("idMacronutrientes")),
                                Code = reader.GetString(reader.GetOrdinal("Codigo")),
                                Name = reader.GetString(reader.GetOrdinal("Nombre")),
                                Description = reader.GetString(reader.GetOrdinal("Descripcion")),
                                Hco = reader.GetDecimal(reader.GetOrdinal("Hco")),
                                Lipids = reader.GetDecimal(reader.GetOrdinal("Lipidos")),
                                Protein = reader.GetDecimal(reader.GetOrdinal("Proteina")),
                                Active = reader.GetBoolean(reader.GetOrdinal("Activo"))
                            };

                            macronutrients.Add(macronutrient);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving macronutrients : " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return macronutrients;
        }

        #endregion
    }

}

