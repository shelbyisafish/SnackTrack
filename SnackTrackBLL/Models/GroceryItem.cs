using System;
using System.Collections.Generic;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL.Models
{
    public class GroceryItem : IBusinessObject
    {
        [MatchingDatabaseField("Id")]
        public int Id { get; set; }

        [MatchingDatabaseField("ProductId")]
        private int ProductId { get; set; }

        //public Product Product
        //{
        //    get
        //    {

        //    }
        //}

        [MatchingDatabaseField("StoreId")]
        private int StoreId { get; set; }

        [MatchingDatabaseField("UnitId")]
        private int UnitId { get; set; }

        [MatchingDatabaseField("BrandId")]
        private int BrandId { get; set; }

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

        [MatchingDatabaseField("DeactivatedDate")]
        private DateTime? DeactivatedDate { get; set; }

        public bool IsActive
        {
            get
            {
                return (DeactivatedDate == null);
            }
        }

        Dictionary<StoredProcedureType, string> StoredProcedureNames = new Dictionary<StoredProcedureType, string>()
        {
            { StoredProcedureType.Get, "GetGroceryItem" },
            { StoredProcedureType.GetAll, "GetGroceryItems" },
            { StoredProcedureType.Insert, "NewGroceryItem" },
            { StoredProcedureType.Update, "UpdateGroceryItem" },
            { StoredProcedureType.Deactivate, "DeactivateGroceryItem" }
            //{ StoredProcedureType.Calculation, "" },
            //{ StoredProcedureType.Other, "" },
        };

        public GroceryItem() { }
    }
}
