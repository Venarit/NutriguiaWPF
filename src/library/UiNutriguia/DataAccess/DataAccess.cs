using Microsoft.Data.SqlClient;
using System;
using System.Data;
using UiNutriguia.Models;
using System.Configuration;
using Dapper;
using System.Windows.Media;
using static Dapper.SqlMapper;

namespace Nutriguia.Model.DataAccess
{
    public class DataAccess
    {
        #region Stored Procedures
        private const string SpGetFood = "[Food].[GetFood]";
        private const string SpGetFoodTypes = "[Food].[GetFoodTypes]";
        private const string SpGetFoodUnits = "[Food].[GetFoodUnits]";
        private const string SpGetPatients = "[Patient].[GetPatients]";
        private const string SpGetActivities = "[Catalog].[GetActivities]";
        private const string SpGetObjectives = "[Catalog].[GetObjectives]";
        private const string SpGetMacronutrients = "[Catalog].[GetMacronutrients]";
        private const string SpGetAppointmentsStatuses = "[Catalog].[GetAppointmentStatuses]";
        private const string SpGetAppointments = "[dbo].[GetAppointments]";
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
        private SqlConnection GetSqlConnection()
        {
            var config = ConfigurationManager.ConnectionStrings["DbConnection"];
            var returnValue = new SqlConnection(config.ConnectionString);
            returnValue.Open();
            return returnValue;
        }

        public sealed class CustomGridReader : IDisposable
        {
            private readonly SqlConnection connection;

            internal CustomGridReader(SqlConnection connection, GridReader reader)
            {
                this.connection = connection;
                this.Reader = reader;
            }

            internal GridReader Reader
            {
                get;
                private set;
            }

            public void Dispose()
            {
            }
        }

        private List<T> Query<T>(string sp, object parms, CommandType commandType = CommandType.StoredProcedure)
        {
            List<T> result = null;

            try
            {
                var config = ConfigurationManager.ConnectionStrings["DbConnection"];
                using (var connection = new SqlConnection(config.ConnectionString))
                {
                    connection.Open();
                    result = connection.Query<T>(sp, parms, commandType: commandType).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return result;
        }

        private CustomGridReader QueryMultiple(SqlConnection connection, string sp, object parms, CommandType commandType = CommandType.StoredProcedure)
        {
            CustomGridReader result = null;

            try
            {
                result = new CustomGridReader(connection, connection.QueryMultiple(sp, parms, commandType: commandType));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return result;
        }

        public List<PatientModel> GetPatients()
        {
            List<PatientModel> returnValue;
            using (var connection = this.GetSqlConnection())
            {
                using (var multiple = this.QueryMultiple(connection, SpGetPatients, null))
                {
                    returnValue = multiple.Reader.Read<PatientModel>().ToList();
                    var profiles = multiple.Reader.Read<NutritionalProfileModel>().ToList();
                    var measurements = multiple.Reader.Read<PatientMeasurementModel>().ToList();

                    var activities = GetActivities();
                    var objectives = GetObjectives();
                    var macronutrients = GetMacronutrients();
                    
                    foreach (var profile in profiles)
                    {
                        var measurement = measurements.FirstOrDefault(m => m.IdNutritionalProfile == profile.IdNutritionalProfile);
                        if (measurement != null)
                            profile.PatientMeasurement = measurement;

                        var activity = activities.FirstOrDefault(a => a.IdActivity == profile.IdActivity);
                        if (activity != null)
                            profile.Activity = activity;

                        var objective = objectives.FirstOrDefault(o => o.IdObjective == profile.IdObjective);
                        if (objective != null)
                            profile.Objective = objective;

                        var macronutrient = macronutrients.FirstOrDefault(m => m.IdMacronutrient == profile.IdMacronutrients);
                        if (macronutrient != null)
                            profile.Macronutrient = macronutrient;
                    }

                    foreach (var patient in returnValue)
                    {
                        var profile = profiles.FirstOrDefault(t => t.IdPatient == patient.IdPatient);
                        if (profile != null)
                            patient.NutritionalProfile = profile;
                    }
                }
            }

            return returnValue.ToList();
        }

        public List<FoodModel> GetFoods(int? idFoodType, int? idUnit)
        {
            List<FoodModel> returnValue;
            using (var connection = this.GetSqlConnection())
            {
                using (var multiple = this.QueryMultiple(connection, SpGetFood, new { @IdFoodType = idFoodType, @IdUnit = idUnit }))
                {
                    returnValue = multiple.Reader.Read<FoodModel>().ToList();
                    var types = multiple.Reader.Read<FoodTypeModel>().ToList();
                    var units = multiple.Reader.Read<FoodEguModel>().ToList();
                    foreach (var food in returnValue)
                    {
                        var type = types.FirstOrDefault(t => t.IdFoodType == food.IdFoodType);
                        if (type != null)
                            food.Type = type;

                        var unit = units.FirstOrDefault(u => u.IdUnit == food.IdUnit);
                        if (unit != null)
                            food.Unit = unit;
                    }
                }
            }

            return returnValue.ToList();
        }

        public List<FoodTypeModel> GetFoodTypes()
        {
            var returnValue = this.Query<FoodTypeModel>(SpGetFoodTypes, null).ToList();
            return returnValue;
        }

        public List<FoodEguModel> GetFoodUnits()
        {
            var returnValue = this.Query<FoodEguModel>(SpGetFoodUnits, null).ToList();
            return returnValue;
        }

        public List<AppointmentModel> GetAppointments(DateTime date)
        {
            List<AppointmentModel> returnValue;
            using (var connection = this.GetSqlConnection())
            {
                using (var multiple = this.QueryMultiple(connection, SpGetAppointments, new { @Date = date }))
                {
                    returnValue = multiple.Reader.Read<AppointmentModel>().ToList();
                    var statuses = multiple.Reader.Read<AppointmentStatusModel>().ToList();
                    var patients = multiple.Reader.Read<PatientModel>().ToList();
                    foreach (var appointment in returnValue)
                    {
                        var status = statuses.FirstOrDefault(s => s.IdAppointmentStatus == appointment.IdAppointmentStatus);
                        if (status != null)
                            appointment.AppointmentStatus = status;

                        var patient = patients.FirstOrDefault(p => p.IdPatient == appointment.IdPatient);
                        if (patient != null)
                            appointment.Patient = patient;
                    }
                }
            }

            return returnValue.ToList();
        }

        #region Catalogs
        public List<ActivityModel> GetActivities()
        {
            var returnValue = this.Query<ActivityModel>(SpGetActivities, null).ToList();
            return returnValue;
        }

        public List<ObjectiveModel> GetObjectives()
        {
            var returnValue = this.Query<ObjectiveModel>(SpGetObjectives, null).ToList();
            return returnValue;
        }

        public List<MacronutrientModel> GetMacronutrients()
        {
            var returnValue = this.Query<MacronutrientModel>(SpGetMacronutrients, null).ToList();
            return returnValue;
        }

        public List<AppointmentStatusModel> GetAppointmentStatuses()
        {
            var returnValue = this.Query<AppointmentStatusModel>(SpGetAppointmentsStatuses, null).ToList();
            return returnValue;
        }
        #endregion
    }
}