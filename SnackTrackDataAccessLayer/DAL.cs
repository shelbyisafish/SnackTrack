using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SnackTrackDataAccessLayer
{
    public class DAL
    {
        private DALConnection Connection;
        public int TimeoutSecs { get; set; }


        /// <summary>
        /// Sensitive information should be encrypted when passed to DAL.
        /// </summary>
        /// <param name="connectionStringType"></param>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <param name="TimeoutSecs"></param>
        public DAL(ConnectionStringType connectionStringType, string id, string pwd, int TimeoutSecs = 300)
        {
            this.TimeoutSecs = TimeoutSecs;
            Connection = new DALConnection(connectionStringType, id, pwd);
        }


        public DBResult ExecuteStoredProcedure(string storedProcedureName, SqlParameter[] parameters = null)
        {
            // by defining these variables OUTSIDE the using statements, we can evaluate them in 
            // the debugger even when the using's go out of scope.
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            DBResult result = null;

            //string connection = Encoding.ASCII.GetString(Convert.FromBase64String(this.ConnectionString));
            using (conn = new SqlConnection(Encoding.ASCII.GetString(Convert.FromBase64String(Connection.ConnectionString))))
            {
                conn.Open();
                using (cmd = new SqlCommand(storedProcedureName, conn) { CommandTimeout = this.TimeoutSecs, CommandType = CommandType.StoredProcedure })
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    // Get records
                    DataTable data = new DataTable();
                    using (reader = cmd.ExecuteReader())
                    {
                        data.Load(reader);
                    }

                    // Get output parameters
                    List<SqlParameter> outputParameters = cmd.Parameters.Cast<SqlParameter>().Where(x => x.Direction == ParameterDirection.Output).ToList();

                    result = new DBResult(data, outputParameters);
                }
            }
            return result;
        }


    }

    public enum ConnectionStringType
    {
        SnackTrack_ConnectionString
    }
}
