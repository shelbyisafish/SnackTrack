using System;
using System.Collections.Generic;
using System.Text;

namespace SnackTrackBLL
{
    interface IBusinessObject
    {
        Dictionary<StoredProcedureType, string> StoredProcedureNames { get; }
    }


    public enum StoredProcedureType
    {
        Get, GetAll, Insert, Update, Deactivate, Calculation, Other
    }
}
