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

            try
            {
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
                                Cellphone = reader.GetString(reader.GetOrdinal("Celular"))
                            };

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
    }

}

