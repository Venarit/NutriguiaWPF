using Microsoft.Data.SqlClient;
using Nutriguia.Model.Models;
using System.Data;
using System.Configuration;
using Dapper;
using static Dapper.SqlMapper;

namespace Nutriguia.Model.DataAccess
{
    public partial class DataAccess
    {
        private static partial class StoredProcedures { }

        private static volatile DataAccess instance;
        private static readonly object lockObject = new object();

        private DataAccess() { }

        public static DataAccess Instance 
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new DataAccess();
                        }
                    }
                }
                return instance;
            }
        }

        private SqlConnection GetSqlConnection()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            connection.Open();
            return connection;
        }

        private List<T> Query<T>(string sp, object parms, CommandType commandType = CommandType.StoredProcedure)
        {
            List<T> result;
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
                Console.WriteLine(ex.ToString());
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
                throw ex;
            }
            return returnValue;
        }

        private CustomGridReader QueryMultiple(SqlConnection connection, string sp, object parms, CommandType commandType = CommandType.StoredProcedure)
        {
            CustomGridReader result;
            try
            {
                result = new CustomGridReader(connection, connection.QueryMultiple(sp, parms, commandType: commandType));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return result;
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

    }

}

