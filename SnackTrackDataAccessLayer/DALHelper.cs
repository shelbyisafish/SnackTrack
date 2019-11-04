using System;
using System.Collections.Generic;
using System.Text;

namespace SnackTrackDataAccessLayer
{
    public class DALHelper
    {
        public static bool TypeIsNullable(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("Cannot check nullable for a null type.", "type");

            return (Nullable.GetUnderlyingType(type) != null);
        }
    }
}
