using System;
using System.Collections.Generic;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL.Models
{
    public class Unit : BusinessObject
    {
        #region public properties

        [MatchingDatabaseField("Id")]
        public int Id { get; set; }

        [MatchingDatabaseField("Name")]
        public string UnitName { get; set; }

        private bool? _IsActive = null;
        public bool IsActive
        {
            get
            {
                if(_IsActive == null)
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
                if(value.GetType() == typeof(bool))
                    _IsActive = value;
            }
        }

        #endregion


        #region private properties

        [MatchingDatabaseField("DeactivatedDate")]
        private DateTime? DeactivatedDate { get; set; }

        #endregion



        public Unit(int Id, string Name, DateTime? DeactivatedDate)
        {
            this.Id = Id;
            UnitName = Name;
            this.DeactivatedDate = DeactivatedDate;
        }

        protected override void AddStoredProcedureNames()
        {
            Dictionary<StoredProcedureType, string> names = new Dictionary<StoredProcedureType, string>()
            {
                { StoredProcedureType.Get, "GetUnit" },
                { StoredProcedureType.GetAll, "GetUnit" },
                { StoredProcedureType.Insert, "NewUnit" },
                { StoredProcedureType.Update, "UpdateUnit" },
                { StoredProcedureType.Deactivate, "DeactivateUnit" }
                //{ StoredProcedureType.Calculation, "" },
                //{ StoredProcedureType.Other, "" },
            };
            SetStoredProcedureNames(names);
        }
    }
}
