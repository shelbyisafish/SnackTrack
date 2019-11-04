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

        public bool IsActive
        {
            get
            {
                return (DeactivatedDate == null);
            }
        }

        new static Dictionary<StoredProcedureType, string> StoredProcedureNames
        {
            get
            {
                return new Dictionary<StoredProcedureType, string>()
                {
                    { StoredProcedureType.Get, "GetStore" },
                    { StoredProcedureType.GetAll, "GetStore" },
                    { StoredProcedureType.Insert, "NewStore" },
                    { StoredProcedureType.Update, "UpdateStore" },
                    { StoredProcedureType.Deactivate, "DeactivateStore" }
                    //{ StoredProcedureType.Calculation, "" },
                    //{ StoredProcedureType.Other, "" },
                };
            }
        }



        public Store() { }

    }
}
