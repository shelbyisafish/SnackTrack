using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnackTrackDataAccessLayer;
using System.Collections.Generic;

namespace SnackTrackDALTests
{
    [TestClass]
    public class DBResultTest
    {
        DBResult ExampleDBResult1;

        [TestInitialize]
        public void TestInitialize()
        {
            try
            {
                List<OutputParameter> ParameterList = new List<OutputParameter>
                {
                    new OutputParameter("param1", 5),
                    new OutputParameter("param2", "a"),
                    new OutputParameter("param3", true),
                    new OutputParameter("nullParam", DBNull.Value),
                    new OutputParameter("duplicateParam", 1.2),
                    new OutputParameter("duplicateParam", 1.5)
                };
                ExampleDBResult1 = new DBResult(new System.Data.DataTable(), ParameterList);


                //int value1 = return1.GetOutputValue<int>();
            }
            catch (NullReferenceException)
            {
                //"Parameter not found.";
            }
            catch (System.FormatException)
            {
                //"Incorrect type for output value.";
            }
        }

        [TestMethod]
        public void DBResult_ShouldFindOutputParameter_ByName()
        {
            OutputParameter outputParameter = ExampleDBResult1.FindOutputParameter("param3");

            Assert.AreEqual(true, outputParameter.Value);
        }

        [TestMethod]
        public void DBResult_ShouldReturnNull_WhenParameterNotFound()
        {
            OutputParameter outputParameter = ExampleDBResult1.FindOutputParameter("Param3");

            Assert.IsNull(outputParameter);
        }

        [TestMethod]
        public void OutputParameter_ShouldGetOutputValues()
        {
            OutputParameter outputParameter1 = ExampleDBResult1.FindOutputParameter("param1");
            int outputValue1 = outputParameter1.Value;

            OutputParameter outputParameter2 = ExampleDBResult1.FindOutputParameter("param2");
            string outputValue2 = outputParameter2.Value;

            OutputParameter outputParameter3 = ExampleDBResult1.FindOutputParameter("param3");
            bool outputValue3 = outputParameter3.Value;

            Assert.IsTrue(outputValue1 == 5 && outputValue2 == "a" && outputValue3 == true);
        }

        [TestMethod]
        public void OutputParameter_ShouldGetOutputValue_WhenNullAndNullable()
        {
            OutputParameter outputParameter = ExampleDBResult1.FindOutputParameter("nullParam");
            int? outputValue = outputParameter.Value;

            Assert.IsNull(outputValue);
        }

        [TestMethod]
        [ExpectedException(typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException))]
        public void OutputParameter_ShouldThrowException_WhenNullAndNotNullable()
        {
            OutputParameter outputParameter = ExampleDBResult1.FindOutputParameter("nullParam");
            int outputValue = outputParameter.Value;
        }

        [TestMethod]
        [ExpectedException(typeof(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException))]
        public void OutputParameter_ShouldThrowException_WhenTypesDontMatch()
        {
            OutputParameter outputParameter = ExampleDBResult1.FindOutputParameter("param2");
            char outputValue = outputParameter.Value;
        }

        [TestMethod]
        public void OutputParameter_ShouldGetOutputValue_WhenImplicitConversion()
        {
            OutputParameter outputParameter = ExampleDBResult1.FindOutputParameter("param1");
            double outputValue = outputParameter.Value;

            Assert.AreEqual(5, outputValue);
        }

        [TestMethod]
        public void OutputParameter_ShouldGetFirstOutputValue_WhenDuplicateNames()
        {
            OutputParameter outputParameter = ExampleDBResult1.FindOutputParameter("duplicateParam");
            double outputValue = outputParameter.Value;

            Assert.AreEqual(1.2, outputValue);
        }

    }
}
