﻿using Microsoft.Data.SqlClient;
using System.Data;
using UiNutriguia.Models;
using System.Configuration;
using Dapper;
using static Dapper.SqlMapper;

namespace Nutriguia.Model.DataAccess;

public class DataAccess
{
    #region Stored Procedures
    private const string SpGetFood = "[Food].[GetFood]";
    private const string SpGetFoodTypes = "[Food].[GetFoodTypes]";
    private const string SpGetFoodUnits = "[Food].[GetFoodUnits]";
    private const string SpGetDishTypes = "[Food].[GetDishTypes]";
    private const string SpGetDishes = "[Food].[GetDishes]";
    private const string SpGetDishFoods = "[Food].[GetDishFoods]";
    private const string SpSetDishes = "[Food].[SetDish]";
    private const string SpdDelDish = "[Food].[DeleteDish]";
    private const string SpGetPatients = "[Patient].[GetPatients]";
    private const string SpSetPatient = "[Patient].[SetPatient]";
    private const string SpSetNutritionalProfile = "[Patient].[SetNutritionalProfile]";
    private const string SpSetPatientMeasurement = "[Patient].[SetPatientMeasurement]";
    private const string SpGetPatientMeasurements = "[Patient].[GetPatientMeasurements]";
    private const string SpGetPatientAppointments = "[Patient].[GetPatientAppointments]";
    private const string SpGetPatientDislikedFoods = "[Patient].[GetPatientDislikedFood]";
    private const string SpSetPatientDislikedFoods = "[Patient].[SetPatientDislikedFood]";
    private const string SpGetActivities = "[Catalog].[GetActivities]";
    private const string SpGetObjectives = "[Catalog].[GetObjectives]";
    private const string SpGetMacronutrients = "[Catalog].[GetMacronutrients]";
    private const string SpGetAppointmentsStatuses = "[Catalog].[GetAppointmentStatuses]";
    private const string SpGetAppointments = "[dbo].[GetAppointments]";
    private const string SpGetAppointmentHistory = "[dbo].[GetAppointmentHistory]";
    private const string SpSetAppointment = "[dbo].[SetAppointment]";
    private const string SpGetNextAppointments = "[dbo].[GetNextAppointments]";
    private const string SpGetPlanPatient = "[NutritionalPlan].[GetPlanPatient]";
    private const string SpsetPlan = "[NutritionalPlan].[SetPlan]";

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

    #region Dapper

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

    private int ExecuteNonQuery(string sp, object parms, CommandType commandType = CommandType.StoredProcedure)
    {
        var returnValue = -1;
        try
        {
            var config = ConfigurationManager.ConnectionStrings["DbConnection"];
            using (var connection = new SqlConnection(config.ConnectionString))
            {
                connection.Open();
                returnValue = connection.ExecuteScalar<int>(sp, parms, commandType: commandType);

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
        return returnValue;
    }

    #endregion

    #region Patient

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

                    var macronutrient = macronutrients.FirstOrDefault(m => m.IdMacronutrients == profile.IdMacronutrients);
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

    public List<PatientMeasurementModel> GetPatientMeasurements(int idPatient)
    {
        var returnValue = this.Query<PatientMeasurementModel>(SpGetPatientMeasurements, new { @IdPatient = idPatient}).ToList();
        return returnValue;
    }

    public List<AppointmentModel> GetPatientAppointments(int idPatient)
    {
        List<AppointmentModel> returnValue;
        using (var connection = this.GetSqlConnection())
        {
            using (var multiple = this.QueryMultiple(connection, SpGetPatientAppointments, new { @IdPatient = idPatient }))
            {
                returnValue = multiple.Reader.Read<AppointmentModel>().ToList();
                var statuses = multiple.Reader.Read<AppointmentStatusModel>().ToList();
                foreach (var appointment in returnValue)
                {
                    var status = statuses.FirstOrDefault(u => u.IdAppointmentStatus == appointment.IdAppointmentStatus);
                    if (status != null)
                        appointment.AppointmentStatus = status;
                }
            }
        }
        return returnValue.ToList();
    }

    public List<FoodModel> GetPatientDislikedFoods(int idPatient)
    {
        var returnValue = this.Query<FoodModel>(SpGetPatientDislikedFoods, new { @IdPatient = idPatient }).ToList();
        return returnValue;
    }

    public int SetPatient(PatientModel model)
    {
        var returnValue = this.ExecuteNonQuery(SpSetPatient, new
        {
            @IdPatient  = model.IdPatient,
            @Name       = model.Name,
            @SecondName = model.SecondName,
            @LastNameP  = model.LastNameP,
            @LastNameM  = model.LastNameM,
            @Email      = model.Email,
            @Cellphone  = model.Cellphone,
            @BirthDate  = model.BirthDate,
        });
        return returnValue;
    }

    public int SetNutritionalProfile(PatientModel model)
    {
        var returnValue = this.ExecuteNonQuery(SpSetNutritionalProfile, new
        {
            @IdPatient = model.IdPatient,
            @Height = model.NutritionalProfile.Height,
            @Sex = model.NutritionalProfile.Sex,
            @idActivity = model.NutritionalProfile.Activity.IdActivity,
            @idObjective = model.NutritionalProfile.Objective.IdObjective,
            @idMacronutrients = model.NutritionalProfile.Macronutrient.IdMacronutrients,
        });
        return returnValue;
    }

    public int SetPatientMeasurement(PatientModel model)
    {
        var returnValue = this.ExecuteNonQuery(SpSetPatientMeasurement, new
        {
            @IdNutritionalProfile = model.NutritionalProfile.IdNutritionalProfile,
            @Weight = model.NutritionalProfile.PatientMeasurement.Weight,
            @BodyFat = model.NutritionalProfile.PatientMeasurement.BodyFat,
            @BMR = model.NutritionalProfile.PatientMeasurement.BMR,
            @TDEE = model.NutritionalProfile.PatientMeasurement.TDEE,
        });
        return returnValue;
    }

    public int SetPatientDislikedFood(int idPatient, int idFood)
    {
        var returnValue = this.ExecuteNonQuery(SpSetPatientDislikedFoods, new
        {
            @IdPatient = idPatient,
            @IdFood = idFood
        });
        return returnValue;
    }
    #endregion

    #region Food

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

    public List<DishModel> GetDishes()
    {
        var returnValue = this.Query<DishModel>(SpGetDishes, null).ToList();
        return returnValue;
    }

    public List<DishFoodModel> GetDishFoods(int idDish)
    {
        List<DishFoodModel> returnValue;
        using (var connection = this.GetSqlConnection())
        {
            using (var multiple = this.QueryMultiple(connection, SpGetDishFoods, new { @IdDish = idDish }))
            {
                returnValue = multiple.Reader.Read<DishFoodModel>().ToList();
                var foods = multiple.Reader.Read<FoodModel>().ToList();
                var types = multiple.Reader.Read<FoodTypeModel>().ToList();
                var units = multiple.Reader.Read<FoodEguModel>().ToList();

                foreach (var food in foods)
                {
                    var type = types.FirstOrDefault(t => t.IdFoodType == food.IdFoodType);
                    if (type != null)
                        food.Type = type;

                    var unit = units.FirstOrDefault(u => u.IdUnit == food.IdUnit);
                    if (unit != null)
                        food.Unit = unit;
                }

                foreach (var dishFood in returnValue)
                {
                    var food = foods.FirstOrDefault(f => f.IdFood == dishFood.IdFood);
                    if (food != null)
                        dishFood.Food = food;
                }
            }
        }

        return returnValue.ToList();
    }

    public int SetDish(DishModel model)
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("FoodId", typeof(int));
        dataTable.Columns.Add("Equivalent", typeof(decimal));
        dataTable.Columns.Add("Quantity", typeof(decimal));
        dataTable.Columns.Add("Kcal", typeof(int));
        dataTable.Columns.Add("Protein", typeof(decimal));
        dataTable.Columns.Add("Lipids", typeof(decimal));
        dataTable.Columns.Add("Hco", typeof(decimal));

        foreach (var dishFood in model.DishFoodModel)
        {
            dataTable.Rows.Add(dishFood.Food.IdFood, dishFood.Equivalent, dishFood.Quantity, dishFood.Kcal, dishFood.Lipids, dishFood.Protein, dishFood.Hco);
        }

        var returnValue = this.ExecuteNonQuery(SpSetDishes, new
        {
            @IdDish = model.IdDish,
            @Code = model.Code,
            @Name = model.Name,
            @Description = model.Description,
            @Kcal = model.Kcal,
            @Protein = model.Protein,
            @Lipids = model.Lipids,
            @Hco = model.Hco,
            @DishFoods = dataTable
        });

        return returnValue;
    }

    public int DeleteDish(int idDish)
    {
        var returnValue = this.ExecuteNonQuery(SpdDelDish, new
        {
            @IdDish = idDish
        });
        return returnValue;
    }
    #endregion

    #region Appointment
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

    public int SetAppointment(AppointmentModel model)
    {
        var returnValue = this.ExecuteNonQuery(SpSetAppointment, new
        {
            @IdAppointment = model.IdAppointment,
            @IdPatient = model.IdPatient,
            @IdAppointmentStatus = model.IdAppointmentStatus,
            @StartDateTime = model.StartDateTime,
            @EndDateTime = model.EndDateTime,
            @Notes = model.Notes,
        });

        return returnValue;
    }

    public List<AppointmentModel> GetNextAppointments()
    {
        var returnValue = this.Query<AppointmentModel>(SpGetNextAppointments, null).ToList();
        return returnValue;
    }

    public List<AppointmentModel> GetAppointmentHistory(DateTime startDate, DateTime endDate)
    {
        List<AppointmentModel> returnValue;
        using (var connection = this.GetSqlConnection())
        {
            using (var multiple = this.QueryMultiple(connection, SpGetAppointmentHistory, new { @BeginDateTime = startDate, @EndDateTime = endDate }))
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
    #endregion

    #region Nutritional Plan
    public List<PlanPatientModel> GetPlanPatient(int idPatient)
    {
        List<PlanPatientModel> returnValue;
        using (var connection = this.GetSqlConnection())
        {
            using (var multiple = this.QueryMultiple(connection, SpGetPlanPatient, new { @IdPatient = idPatient }))
            {
                returnValue = multiple.Reader.Read<PlanPatientModel>().ToList();
                var plans = multiple.Reader.Read<PlanModel>().ToList();
                var planOptions = multiple.Reader.Read<PlanOptionModel>().ToList();
                var planDishes = multiple.Reader.Read<PlanDishModel>().ToList();
                var dishes = GetDishes();

                foreach (var dish in dishes)
                {
                    var dishFoods = GetDishFoods(dish.IdDish);
                    if (dishFoods != null)
                    {
                        foreach (var dishFood in dishFoods)
                        {
                            dish.DishFoodModel.Add(dishFood);
                        }
                    }
                }

                foreach (var planDish in planDishes)
                {
                    var dish = dishes.FirstOrDefault(d => d.IdDish == planDish.IdDish);
                    if (dish != null)
                        planDish.Dish = dish;
                }
                
                foreach (var planOption in planOptions)
                {
                    var breakfast = planDishes.FirstOrDefault(d => d.IdPlanDish == planOption.Breakfast);
                    if (breakfast != null)
                        planOption.BreakfastModel = breakfast;

                    var col1 = planDishes.FirstOrDefault(d => d.IdPlanDish == planOption.Collation1);
                    if (col1 != null)
                        planOption.Collation1Model = col1;

                    var meal = planDishes.FirstOrDefault(d => d.IdPlanDish == planOption.Meal);
                    if (meal != null)
                        planOption.MealModel = meal;

                    var col2 = planDishes.FirstOrDefault(d => d.IdPlanDish == planOption.Collation2);
                    if (col2 != null)
                        planOption.Collation2Model = col2;

                    var dinner = planDishes.FirstOrDefault(d => d.IdPlanDish == planOption.Dinner);
                    if (dinner != null)
                        planOption.DinnerModel = dinner;
                }

                foreach (var plan in plans)
                {
                    var opt1 = planOptions.FirstOrDefault(p => p.IdPlanOption == plan.IdPlanOption_1);
                    if (opt1 != null)
                        plan.PlanOptionModel1 = opt1;

                    var opt2 = planOptions.FirstOrDefault(p => p.IdPlanOption == plan.IdPlanOption_2);
                    if (opt2 != null)
                        plan.PlanOptionModel2 = opt2;

                    var opt3 = planOptions.FirstOrDefault(p => p.IdPlanOption == plan.IdPlanOption_3);
                    if (opt3 != null)
                        plan.PlanOptionModel3 = opt3;
                }

                foreach (var planPatient in returnValue)
                {
                    var plan = plans.FirstOrDefault(p => p.IdPlan == planPatient.IdPlan);
                    if (plan != null)
                        planPatient.PlanModel = plan;
                }
            }
        }
        return returnValue.ToList();
    }

    public int SetPlanOption1(PlanOptionModel model, int idPatient, int idPlan)
    {
        var returnValue = this.ExecuteNonQuery(SpsetPlan, new
        {
            @IdPlan             = idPlan,
            @IdPatient          = idPatient,
            @IdDishBreakfast    = model.BreakfastModel.Dish.IdDish,
            @IdDishCollation1   = model.Collation1Model.Dish.IdDish,
            @IdDishMeal         = model.MealModel.Dish.IdDish,
            @IdDishCollation2   = model.Collation2Model.Dish.IdDish,
            @IdDishDinner       = model.DinnerModel.Dish.IdDish,
            @Kcal               = model.Kcal,
            @Protein            = model.Protein,
            @Lipids             = model.Lipids,
            @Hco                = model.Hco
        });
        return returnValue;
    }


    #endregion

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

    public List<DishTypeModel> GetDishTypes()
    {
        var returnValue = this.Query<DishTypeModel>(SpGetDishTypes, null).ToList();
        return returnValue;
    }
    #endregion
}