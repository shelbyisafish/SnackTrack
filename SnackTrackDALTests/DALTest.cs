using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnackTrackDataAccessLayer;
using SnackTrackBLL.Models;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace SnackTrackDALTests
{
    [TestClass]
    public class DALTest
    {
        DAL dAL;

        [TestInitialize]
        public void TestInitialize()
        {
            string x = Convert.ToBase64String(Encoding.ASCII.GetBytes("SnackTrackConnection"));       //(WebConfigurationManager.AppSettings["x"]));
            string y = Convert.ToBase64String(Encoding.ASCII.GetBytes("rUtcwhys3q!Jahicm]f4-Tt+"));       //(WebConfigurationManager.AppSettings["y"]));
            dAL = new DAL(ConnectionStringType.SnackTrack_ConnectionString, x, y);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void DAL_ShouldThrowException_WhenIncorrectCredentials()
        {
            string x = Convert.ToBase64String(Encoding.ASCII.GetBytes("SnackTrackConnection"));
            string y = Convert.ToBase64String(Encoding.ASCII.GetBytes("password!"));
            DAL incorrectDAL = new DAL(ConnectionStringType.SnackTrack_ConnectionString, x, y);

            DBResult result = incorrectDAL.ExecuteStoredProcedure("TestSP_Get_NoParam", null);
        }

        [TestMethod]
        public void DAL_ShouldGetSPResults_NoParameters()
        {
            DBResult result = dAL.ExecuteStoredProcedure("TestSP_Get_NoParam", null);

            DALMap<Store> storeMapper = new DALMap<Store>();
            List<Store> stores = storeMapper.Map(result.DataTable);

            Assert.IsTrue(stores.Any());
        }

        [TestMethod]
        public void DAL_ShouldGetSPResults_OneParameter()
        {
            // SP TestSP_Get_OneParam requires 1 parameter.
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("InputParam", SqlDbType.Int) { Value = 2 }
            };

            DBResult result = dAL.ExecuteStoredProcedure("TestSP_Get_OneParam", sqlParameters);

            DALMap<Store> storeMapper = new DALMap<Store>();
            List<Store> stores = storeMapper.Map(result.DataTable);

            Assert.IsTrue(stores.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void DAL_ShouldThrowException_WhenNoParameterGiven()
        {
            // SP TestSP_Get_OneParam requires 1 parameter.
            DBResult result = dAL.ExecuteStoredProcedure("TestSP_Get_OneParam", null);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void DAL_ShouldThrowException_WhenParameterGiven()
        {
            // SP GetStores does not accept parameters.
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("InputParam", SqlDbType.Int) { Value = 2 }
            };

            DBResult result = dAL.ExecuteStoredProcedure("TestSP_Get_NoParam", sqlParameters);
        }

        [TestMethod]
        public void DAL_ShouldGetSPOutput()
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("OutputParam", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output }
            };

            DBResult result = dAL.ExecuteStoredProcedure("TestSP_Get_OutputParam", sqlParameters);
            string outputValue = result.FindOutputParameter("OutputParam").Value;

            Assert.IsNotNull(outputValue);
        }
    }
}
