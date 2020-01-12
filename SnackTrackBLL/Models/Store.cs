using System;
using System.Collections.Generic;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL.Models
{
    public class Store : BusinessObject
    {
        [MatchingDatabaseField("Id")]
        public int Id { get; set; }

        [MatchingDatabaseField("Name")]
        public string StoreName { get; set; }

        [MatchingDatabaseField("DeactivatedDate")]
        private DateTime? DeactivatedDate { get; set; }

        private bool? _IsActive = null;
        public bool IsActive
        {
            get
            {
                if (_IsActive == null)
                {
                    _IsActive = (DeactivatedDate == null);
                    return (bool)_IsActive;
                }
                else
                {
                    return (bool)_IsActive;
                }
            }
            set
            {
                if (value.GetType() == typeof(bool))
                    _IsActive = value;
            }
        }




        public Store() 
        {

        }

        protected override void AddStoredProcedureNames()
        {
            Dictionary<StoredProcedureType, string> names = new Dictionary<StoredProcedureType, string>()
            {
                { StoredProcedureType.Get, "GetStore" },
                { StoredProcedureType.GetAll, "GetStore" },
                { StoredProcedureType.Insert, "NewStore" },
                { StoredProcedureType.Update, "UpdateStore" },
                { StoredProcedureType.Deactivate, "DeactivateStore" }
                //{ StoredProcedureType.Calculation, "" },
                //{ StoredProcedureType.Other, "" },
            };
            SetStoredProcedureNames(names);
        }
    }
}
