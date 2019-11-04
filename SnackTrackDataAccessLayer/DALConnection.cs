using System;
using System.Collections.Generic;
using System.Text;

namespace SnackTrackDataAccessLayer
{
    public class DALConnection
    {
        private string Server;
        private string Database;
        private string id;
        private string pwd;
        internal string ConnectionString
        {
            get
            {
                string credentials = string.Format("Integrated Security=false; user id={0}; password={1}", id, pwd);
                string connection = string.Format("data source={0}; initial catalog={1}; {2};", Server, Database, credentials);
                return Convert.ToBase64String(Encoding.ASCII.GetBytes(connection));
            }
        }

        internal DALConnection(ConnectionStringType connectionStringType, string id, string pwd)
        {
            this.id = Encoding.ASCII.GetString(Convert.FromBase64String(id));
            this.pwd = Encoding.ASCII.GetString(Convert.FromBase64String(pwd));

            switch (connectionStringType)
            {
                case ConnectionStringType.SnackTrack_ConnectionString:
                    Server = "DESKTOP-QPHPCFQ";
                    Database = "SnackTrack";
                    break;
                default:
                    Server = "";
                    Database = "";
                    break;
            }

        }
    }
}
