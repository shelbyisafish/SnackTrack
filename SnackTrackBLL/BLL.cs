using System;
using System.Collections.Generic;
using System.Text;
using SnackTrackDataAccessLayer;

namespace SnackTrackBLL
{
    class BLL
    {
        DAL dal;


        public BLL(string id, string pwd)
        {
            string x = Convert.ToBase64String(Encoding.ASCII.GetBytes(id));       //(WebConfigurationManager.AppSettings["x"]));
            string y = Convert.ToBase64String(Encoding.ASCII.GetBytes(pwd));       //(WebConfigurationManager.AppSettings["y"]));
            dal = new DAL(ConnectionStringType.SnackTrack_ConnectionString, x, y);
        }

        public T Get<T>() where T : BusinessObject, new()
        {
            //string paramName = T.StoredProcedureNames[StoredProcedureType.Get];
            //DBResult result = dal.ExecuteStoredProcedure(paramName, null);
            

            return null;
        }
    }
}
