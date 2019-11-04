using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;

namespace SnackTrackDataAccessLayer
{
    public class DBResult
    {
        public DataTable DataTable { get; set; }
        public List<OutputParameter> OutputParameters { get; set; }

        public DBResult(DataTable DataTable, List<OutputParameter> OutputParameters)
        {
            this.DataTable = DataTable;
            this.OutputParameters = OutputParameters;
        }

        public DBResult(DataTable DataTable, List<SqlParameter> sqlParameters)
        {
            this.DataTable = DataTable;
            List<OutputParameter> outputParameters = sqlParameters.Select(x => new OutputParameter(x.ParameterName, x.Value)).ToList();
            this.OutputParameters = outputParameters;
        }

        public OutputParameter FindOutputParameter(string parameterName)
        {
            return OutputParameters.Where(x => x.ParameterName == parameterName).FirstOrDefault();
        }
    }


    public class OutputParameter
    {
        public string ParameterName { get; set; }
        public dynamic Value { get; set; }

        public OutputParameter(string ParameterName, dynamic Value)
        {
            this.ParameterName = ParameterName;
            this.Value = (Value.Equals(DBNull.Value)) ? null : Value;
        }
    }
}