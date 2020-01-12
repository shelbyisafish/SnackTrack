using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SnackTrackBLL
{
    public abstract class BusinessObject
    {
        private Dictionary<StoredProcedureType, string> storedProcedureNames;

        protected void SetStoredProcedureNames(Dictionary<StoredProcedureType, string> names)
        {
            storedProcedureNames = names;
        }

        public string GetStoredProcedureName(StoredProcedureType spType)
        {
            try
            {
                return storedProcedureNames[spType];
            }
            catch   // Dictionary not defined, key not found.
            {
                return null;
            }
        }

        /// <summary>
        /// You are required to define a method that sets the value of the storedProcedureNames dictionary using the SetStoredProcedureNames(Dictionary<>) method.
        /// </summary>
        protected abstract void AddStoredProcedureNames();
    }
}
