using Microsoft.Data.SqlClient;

namespace Nutriguia.Model.DataAccess
{
    public class DataAccess
    {
        #region Store Procedures
        
        private const string SpGetPatients = "[Patient].[GetPatients]";

        #endregion

        protected string ConnectionString { get; set; }

        private SqlConnection db;

        public DataAccess(string connectionString) 
        {
            try
            {
                this.db = new SqlConnection(connectionString);
                this.db.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
