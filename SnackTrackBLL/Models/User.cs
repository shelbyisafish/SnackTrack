using System;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL.Models
{
    public class User
    {
        [MatchingDatabaseField("PublicId")]
        public Guid PublicId { get; set; }

        [MatchingDatabaseField("UserName")]
        public string UserName { get; set; }



        public User() { }


        //public string GenerateOneTimeToken()
        //{
        //    // Call SPROC to generate guid and set expiry: GenerateToken

        //    //return Convert.ToBase64String(.ToByteArray());
        //}

        //public TokenInfo UseOneTimeToken(string token)
        //{
        //    TokenInfo TokenInfo = new TokenInfo();
        //    // Unencode token string to guid
        //    byte[] data = Convert.FromBase64String(token);
        //    Guid guidToken = new Guid(data);

        //    // Call SPROC to return guid and expiry to compare to: GetToken

        //    // Success: Call SPROC to clear token and expiry: RemoveToken
        //    // Failure, wrong guid: Do not clear token
        //    // Failure, expiry: Clear token and expiry: RemoveToken
        //}
    }
}
