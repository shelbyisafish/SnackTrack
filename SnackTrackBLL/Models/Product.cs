using System;
using System.Collections.Generic;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL.Models
{
    public class Product
    {
        [MatchingDatabaseField("Id")]
        public int Id { get; set; }

        public List<Product> SubProducts { get; set; }

        [MatchingDatabaseField("Name")]
        public string ProductName { get; set; }

        [MatchingDatabaseField("PreferredUnitId")]
        private int PreferredUnitId { get; set; }

        public Unit PreferredUnit { get; set; }

        [MatchingDatabaseField("DeactivatedDate")]
        private DateTime? DeactivatedDate { get; set; }

        public bool IsActive
        {
            get
            {
                return (DeactivatedDate == null);
            }
        }



        
        public Product() { }

    }
}
