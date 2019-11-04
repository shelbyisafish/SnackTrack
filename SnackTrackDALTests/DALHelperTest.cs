using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnackTrackDataAccessLayer;

namespace SnackTrackDALTests
{
    [TestClass]
    public class DALHelperTest
    {
        [TestMethod]
        public void TypeIsNullable_ShouldReturnFalse_WhenTypeIsNotNullable()
        {
            bool result = DALHelper.TypeIsNullable(typeof(int));

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TypeIsNullable_ShouldThrowException_WhenGivenNullType()
        {
            DALHelper.TypeIsNullable(null);
        }

        [TestMethod]
        public void TypeIsNullable_ShouldReturnTrue_WhenPropertyIsNullable()
        {
            bool result = DALHelper.TypeIsNullable(typeof(int?));

            Assert.IsTrue(result);
        }
    }
}
