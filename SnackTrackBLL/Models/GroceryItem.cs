using System;
using System.Collections.Generic;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL.Models
{
    public class GroceryItem : BusinessObject
    {
        #region public properties

        [MatchingDatabaseField("Id")]
        public int Id { get; set; }

        private Product _Product = null;
        public Product Product
        {
            get
            {
                return null;
            }
        }

        private Store _Store = null;
        public Store Store
        {
            get
            {
                return null;
            }
        }

        private Unit _Unit = null;
        public Unit Unit
        {
            get
            {
                return null;
            }
        }

        private Brand _Brand = null;
        public Brand Brand
        {
            get
            {
                return null;
            }
        }

        [MatchingDatabaseField("Date")]
        public DateTime Date { get; set; }

        [MatchingDatabaseField("PricePaid")]
        public decimal PricePaid { get; set; }

        [MatchingDatabaseField("PricePerUnit")]
        public decimal PricePerUnit { get; set; }

        [MatchingDatabaseField("IsPurchased")]
        public bool IsPurchased { get; set; }

        [MatchingDatabaseField("IsOrganic")]
        public bool IsOrganic { get; set; }

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

        [MatchingDatabaseField("ProductId")]
        private int ProductId { get; set; }

        [MatchingDatabaseField("StoreId")]
        private int StoreId { get; set; }

        [MatchingDatabaseField("UnitId")]
        private int UnitId { get; set; }

        [MatchingDatabaseField("BrandId")]
        private int BrandId { get; set; }

        [MatchingDatabaseField("DeactivatedDate")]
        private DateTime? DeactivatedDate { get; set; }

        #endregion


        public GroceryItem(int Id, DateTime Date, decimal PricePaid, decimal PricePerUnit, bool IsPurchased, bool IsOrganic, DateTime? DeactivatedDate)
        {
            this.Id = Id;
            this.Date = Date;
            this.PricePaid = PricePaid;
            this.PricePerUnit = PricePerUnit;
            this.IsPurchased = IsPurchased;
            this.IsOrganic = IsOrganic;
            this.DeactivatedDate = DeactivatedDate;
        }

        protected override void AddStoredProcedureNames()
        {
            Dictionary<StoredProcedureType, string> names = new Dictionary<StoredProcedureType, string>()
            {
                { StoredProcedureType.Get, "GetGroceryItem" },
                { StoredProcedureType.GetAll, "GetGroceryItems" },
                { StoredProcedureType.Insert, "NewGroceryItem" },
                { StoredProcedureType.Update, "UpdateGroceryItem" },
                { StoredProcedureType.Deactivate, "DeactivateGroceryItem" }
                //{ StoredProcedureType.Calculation, "" },
                //{ StoredProcedureType.Other, "" },
            };
            SetStoredProcedureNames(names);
        }
    }
}
