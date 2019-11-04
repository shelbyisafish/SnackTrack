using System;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL.Models
{
    public class Unit
    {
        [MatchingDatabaseField("Id")]
        public int Id { get; set; }

        [MatchingDatabaseField("Name")]
        public string UnitName { get; set; }

        [MatchingDatabaseField("DeactivatedDate")]
        private DateTime? DeactivatedDate { get; set; }

        public bool IsActive
        {
            get
            {
                return (DeactivatedDate == null);
            }
        }



        public Unit() { }


    }
}
