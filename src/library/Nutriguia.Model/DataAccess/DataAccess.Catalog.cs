using Microsoft.Data.SqlClient;
using Nutriguia.Model.Models;
using System.Data;

namespace Nutriguia.Model.DataAccess
{
    public partial class DataAccess
    {
        private partial class StoredProcedures
        {
            internal const string SpGetActivity = "[Catalogo].[GetActividad]";
            internal const string SpGetObjectives = "[Catalogo].[GetObjetivo]";
            internal const string SpGetMacronutrients = "[Catalogo].[GetMacronutrientes]";
        }

        public List<ActivityModel> GetActivities()
        {
            var returnValue = this.Query<ActivityModel>(StoredProcedures.SpGetActivity, null);
            return returnValue;
        }

        public List<ObjectiveModel> GetObjectives()
        {
            var returnValue = this.Query<ObjectiveModel>(StoredProcedures.SpGetObjectives, null);
            return returnValue;
        }

        public List<MacronutrientModel> GetMacronutrients()
        {
            var returnValue = this.Query<MacronutrientModel>(StoredProcedures.SpGetMacronutrients, null);
            return returnValue;
        }
    }
}
