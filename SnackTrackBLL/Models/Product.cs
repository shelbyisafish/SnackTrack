using System;
using System.Collections.Generic;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL.Models
{
    public class Product : BusinessObject
    {
        #region public properties

        [MatchingDatabaseField("Id")]
        public int Id { get; set; }

        public List<Product> SubProducts { get; set; }      // TO DO

        [MatchingDatabaseField("Name")]
        public string ProductName { get; set; }

        private Unit _PreferredUnit = null;
        public Unit PreferredUnit 
        {
            get
            {
                if(_PreferredUnit == null)
                {
                    Unit u = new Unit(Unit_Id, Unit_Name, Unit_DeactivatedDate);
                    _PreferredUnit = u;
                    return u;
                }
                else
                {
                    return _PreferredUnit;
                }
            }
            set
            {
                _PreferredUnit = value;
            }
        }

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

        #endregion



        #region private properties

        [MatchingDatabaseField("PreferredUnitId")]
        private int PreferredUnitId { get; set; }

        [MatchingDatabaseField("DeactivatedDate")]
        private DateTime? DeactivatedDate { get; set; }

        // Unit
        [MatchingDatabaseField("Unit_Id")]
        private int Unit_Id { get; set; }

        [MatchingDatabaseField("Unit_Name")]
        private string Unit_Name { get; set; }

        [MatchingDatabaseField("Unit_DeactivatedDate")]
        private DateTime? Unit_DeactivatedDate { get; set; }

        #endregion





        public Product(int Id, string ProductName, int PreferredUnitId, DateTime? DeactivatedDate) 
        {
            this.Id = Id;
            this.ProductName = ProductName;
            this.PreferredUnitId = PreferredUnitId;
            this.DeactivatedDate = DeactivatedDate;
        }

        protected override void AddStoredProcedureNames()
        {
            Dictionary<StoredProcedureType, string> names = new Dictionary<StoredProcedureType, string>()
            {
                { StoredProcedureType.Get, "GetProduct" },
                { StoredProcedureType.GetAll, "GetProduct" },
                { StoredProcedureType.Insert, "NewProduct" },
                { StoredProcedureType.Update, "UpdateProduct" },
                { StoredProcedureType.Deactivate, "DeactivateProduct" }
                //{ StoredProcedureType.Calculation, "" },
                //{ StoredProcedureType.Other, "" },
            };
            SetStoredProcedureNames(names);
        }
    }
}
