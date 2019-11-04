using System;
using System.Collections.Generic;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL.Models
{
    public class TokenInfo
    {
        public bool Validated
        {
            get { return Errors.Count == 0; }
        }
        public readonly List<TokenValidationStatus> Errors = new List<TokenValidationStatus>();


        public TokenInfo() { }
    }

    public enum TokenValidationStatus
    {
        Expired,
        WrongUser,
        WrongPurpose,
        WrongGuid
    }
}
