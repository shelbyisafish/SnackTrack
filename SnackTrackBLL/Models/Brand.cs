using System;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL.Models
{
    public class Brand
    {
        [MatchingDatabaseField("Id")]
        public int Id { get; set; }

        [MatchingDatabaseField("Name")]
        public string BrandName { get; set; }

        [MatchingDatabaseField("DeactivatedDate")]
        private DateTime? DeactivatedDate { get; set; }

        public bool IsActive
        {
            get
            {
                return (DeactivatedDate == null);
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
    }
}
