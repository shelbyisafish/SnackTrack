using System;
using System.Collections.Generic;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL.Models
{
    public class Brand : BusinessObject
    {
        [MatchingDatabaseField("Id")]
        public int Id { get; set; }

        [MatchingDatabaseField("Name")]
        public string BrandName { get; set; }

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

        [MatchingDatabaseField("DeletedDate")]
        private DateTime? DeletedDate { get; set; }

        public bool IsDeleted
        {
            get
            {
                return (DeletedDate != null);
            }
        }



        public Brand() { }

        protected override void AddStoredProcedureNames()
        {
            Dictionary<StoredProcedureType, string> names = new Dictionary<StoredProcedureType, string>()
            {
                { StoredProcedureType.Get, "GetBrand" },
                { StoredProcedureType.GetAll, "GetBrand" },
                { StoredProcedureType.Insert, "NewBrand" },
                { StoredProcedureType.Update, "UpdateBrand" },
                { StoredProcedureType.Deactivate, "DeactivateBrand" }
                //{ StoredProcedureType.Calculation, "" },
                //{ StoredProcedureType.Other, "" },
            };
            SetStoredProcedureNames(names);
        }
    }
}
